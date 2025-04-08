
using System;
using System.Collections.Generic;
using UnityEngine;
public class Level : MonoBehaviour
{
    private const float PipeWidth = 7.8f;
    private const float PipeHeadHeight = 3.75f;
    private const float CameraOrthoSize = 50f;
    private const float pipeSpeed = 3f;
    private List<Pipe> _pipes = new List<Pipe>();
    private void Start()
    {
        CreateGap(50, 20, 20);
    }

    private void Update()
    {
        HandlePipeMovement();
    }

    private void HandlePipeMovement()
    {
        if (_pipes.Count == 0) return;
        foreach (var pipe in _pipes)
        {
            pipe.Move();
        }
    }

    private void CreateGap(float gapY, float gapSize, float posX)
    {
        CreatePipe(gapY - gapSize / 2, posX, true);
        CreatePipe(CameraOrthoSize*2 - gapY - gapSize/2, posX, false);
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

    private class Pipe
    {
        private Transform _pipeHead;
        private Transform _pipeBody;

        public Pipe(Transform pipeHead, Transform pipeBody)
        {
            _pipeHead = pipeHead;
            _pipeBody = pipeBody;
        }

        public void Move()
        {
            _pipeHead.Translate(Vector3.left * pipeSpeed * Time.deltaTime);
            _pipeBody.Translate(Vector3.left * pipeSpeed * Time.deltaTime);
        }
    }
}