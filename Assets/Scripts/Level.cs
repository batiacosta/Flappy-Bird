
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Level : MonoBehaviour
{
    public static Level instance { get; private set; }

    private const float PipeWidth = 7.8f;
    private const float PipeHeadHeight = 3.75f;
    private const float CameraOrthoSize = 50f;
    private const float PipeSpeed = 30f;
    private float _leftBound = 0;
    private List<Pipe> _pipes = new List<Pipe>();
    private float _pipeSpawnTimer = 0;
    private float _pipeSpawnTimerMax = 0;
    private float _gapSize = 0;
    private static float _achievedPipes = 0;

    private enum Difficulty
    {
        Easy, Medium, Hard, Impossible
    }

    private enum State
    {
        WaitingToStart, Playing, Death
    }
    private Difficulty _difficulty = Difficulty.Easy;
    private State _state = State.WaitingToStart;
    private int _spawnedPipes;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        _state = State.WaitingToStart;
        _pipeSpawnTimerMax = 1f;
        SetDifficulty(Difficulty.Easy);
        SetLeftBound();
        Bird.instance.OnStartedPlaying += OnStartedPlaying;
        Bird.instance.OnDeath += OnDeath;
    }

    private void OnStartedPlaying()
    {
        _state = State.Playing;
    }

    private void OnDestroy()
    {
        Bird.instance.OnStartedPlaying -= OnStartedPlaying;
        Bird.instance.OnDeath -= OnDeath;
    }

    private void OnDeath()
    {
        _state = State.Death;
        //RestartAfter(3);
    }

    private async Awaitable RestartAfter(float seconds)
    {
        await Awaitable.WaitForSecondsAsync(seconds);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void SetLeftBound() // Can be public to be call when the screen resizes
    {
        var cameraOrtho = Camera.main.orthographicSize * 2;
        var cameraWith = cameraOrtho * Camera.main.aspect;
        var cameraPositionX = Camera.main.transform.position.x;
        _leftBound = cameraPositionX - (cameraWith / 2);
    }

    private void Update()
    {
        if (_state is State.Death or State.WaitingToStart) return;
        HandlePipeMovement();
        HandlePipeSpawning();
    }

    private void HandlePipeSpawning()
    {
        _pipeSpawnTimer -= Time.deltaTime;
        if (_pipeSpawnTimer < 0)
        {
            _pipeSpawnTimer += _pipeSpawnTimerMax;
            var heightEdgeLimit = 10f;
            var minHeight = _gapSize / 2 + heightEdgeLimit;
            var totalHeight = CameraOrthoSize * 2;
            var maxHeight = totalHeight - _gapSize / 2 - heightEdgeLimit;
            var height = Random.Range(minHeight, maxHeight);
            CreateGapPipes(50, height, -_leftBound + PipeWidth*2);
        }
    }

    private void HandlePipeMovement()
    {
        if (_pipes.Count == 0) return;
        for (int i = 0; i < _pipes.Count; i++)
        {
            var pipePositionX = _pipes[i].GetPositionX();
            _pipes[i].Move();
            if (pipePositionX <= _leftBound)
            {
                _pipes[i].DestroySelf();
                _pipes.RemoveAt(i);
                i--;
            }
        }
    }

    private void CreateGapPipes(float gapY, float gapSize, float posX)
    {
        CreatePipe(gapY - gapSize / 2, posX, true);
        CreatePipe(CameraOrthoSize*2 - gapY - gapSize/2, posX, false);
        _spawnedPipes++;
        SetDifficulty(GetDifficulty());
    }
    private void CreatePipe(float height, float xPosition, bool createBottom)
    {
        var pipeHeadPositionY = 0f;
        var pipeBodyPositionY = 0f;
        var pipeHead = Instantiate(GameAssets.GetInstance().GetPipeHead());
        var pipeBody = Instantiate(GameAssets.GetInstance().GetPipeBody());
        if (createBottom)
        {
            pipeHeadPositionY = height - (PipeHeadHeight / 2) - CameraOrthoSize;
            pipeBodyPositionY = -CameraOrthoSize;
        }
        else
        {
            pipeHeadPositionY = CameraOrthoSize - height + PipeHeadHeight / 2;
            pipeBodyPositionY = CameraOrthoSize;
            pipeBody.localScale = new Vector3(1, -1, 1); // So it grows inverted
        }
        
        pipeHead.SetPositionAndRotation(new Vector2(xPosition, pipeHeadPositionY), Quaternion.identity);
        pipeBody.SetPositionAndRotation(new Vector2(xPosition, pipeBodyPositionY), Quaternion.identity);
        
        var pipeBodyRenderer = pipeBody.gameObject.GetComponent<SpriteRenderer>(); ;
        pipeBodyRenderer.drawMode = SpriteDrawMode.Sliced; // Making sure Sliced is selected to be able to set size
        pipeBodyRenderer.size = new Vector2(PipeWidth, height);
        var pipeBodyBoxCollider = pipeBody.gameObject.GetComponent<BoxCollider2D>();
        pipeBodyBoxCollider.size = new Vector2(PipeWidth, height);
        pipeBodyBoxCollider.offset = new Vector2(0f, height/2);
        
        var pipe = new Pipe(pipeHead, pipeBody);
        _pipes.Add(pipe);
    }

    private Difficulty GetDifficulty()
    {
        if (_spawnedPipes >= 30) return Difficulty.Impossible;
        if (_spawnedPipes >= 20) return Difficulty.Hard;
        if (_spawnedPipes >= 10) return Difficulty.Medium;
        return Difficulty.Easy;
    }

    private void SetDifficulty(Difficulty difficulty)
    {
        switch (difficulty)
        {
            case Difficulty.Impossible:
                _gapSize = 24;
                _pipeSpawnTimerMax = 0.8f;
                break;
            case Difficulty.Hard:
                _gapSize = 33;
                _pipeSpawnTimerMax = 1.0f;
                break;
            case Difficulty.Medium:
                _gapSize = 40;
                _pipeSpawnTimerMax = 1.1f;
                break;
            case Difficulty.Easy:
                _gapSize = 50;
                _pipeSpawnTimerMax = 1.2f;
                break;
        }
    }

    public int GetSpawnedPipesNumber() => _spawnedPipes;

    private class Pipe
    {
        private Transform _pipeHead;
        private Transform _pipeBody;
        private bool _hasPassedBird = false;

        public Pipe(Transform pipeHead, Transform pipeBody)
        {
            _pipeHead = pipeHead;
            _pipeBody = pipeBody;
        }

        public void Move()
        {
            _pipeHead.Translate(Vector3.left * PipeSpeed * Time.deltaTime);
            _pipeBody.Translate(Vector3.left * PipeSpeed * Time.deltaTime);
            if (!_hasPassedBird)
            {
                if (_pipeHead.position.x < 0)
                {
                    _achievedPipes = _achievedPipes + 0.5f;
                    _hasPassedBird = true;
                }
            }
        }
        public float GetPositionX() => _pipeHead.position.x;

        public void DestroySelf()
        {
            Destroy(_pipeHead.gameObject);
            Destroy(_pipeBody.gameObject);
        }
    }
    
    public float GetAchievedPipes() => _achievedPipes;
    
}