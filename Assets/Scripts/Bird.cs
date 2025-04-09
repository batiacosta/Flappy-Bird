using System;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public static Bird instance;
    public Action OnDeath;
    public Action OnStartedPlaying;
    
    private Rigidbody2D _rigidbody;
    private const float JUMP_VALUE = 100f;

    private enum State
    {
        WaitingToStart, Playing, Dead
    }

    private State _state = State.WaitingToStart;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        if(_rigidbody == null) return;
        _rigidbody.bodyType = RigidbodyType2D.Static;
        InputHandler.OnTap += OnTap;
    }
    private void OnDestroy()
    {
        InputHandler.OnTap -= OnTap;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        SetState(State.Dead);
        SoundManager.PlaySound(SoundManager.Sound.Lose);
    }

    private void OnTap()
    {
        Jump();
        if(_state == State.WaitingToStart) SetState(State.Playing);
    }

    private void Jump()
    {
        _rigidbody.linearVelocityY = Vector2.up.y * JUMP_VALUE;
        SoundManager.PlaySound(SoundManager.Sound.BirdJump);
    }

    private void SetState(State state)
    {
        _state = state;
        switch (state)
        {
            case State.WaitingToStart:
                _rigidbody.bodyType = RigidbodyType2D.Static;
                break;
            case State.Playing:
                _rigidbody.bodyType = RigidbodyType2D.Dynamic;
                OnStartedPlaying?.Invoke();
                break;
            case State.Dead:
                _rigidbody.bodyType = RigidbodyType2D.Static;
                OnDeath?.Invoke();
                break;
        }
    }
}
