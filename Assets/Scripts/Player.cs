using System;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private const float JUMP_VALUE = 100f;

    private enum State
    { 
        Playing, Dead, WaitingToStart
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        if(_rigidbody == null) return;
        SetState(State.WaitingToStart);
        InputHandler.OnTap += OnTap;
    }
    private void OnDestroy()
    {
        InputHandler.OnTap -= OnTap;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        SetState(State.Dead);
    }

    private void OnTap()
    {
        if (GameManager.GameState is GameManager.State.Begin || GameManager.GameState is GameManager.State.Playing)
        {
            if (GameManager.GameState is GameManager.State.Begin)
            {
                GameManager.Instance.SetState(GameManager.State.Playing);
                SetState(State.Playing);
            }
            Jump();
        }
    }

    private void Jump()
    {
        _rigidbody.linearVelocityY = Vector2.up.y * JUMP_VALUE;
        SoundManager.PlaySound(SoundManager.Sound.BirdJump);

    }
    private void SetState(State state)
    {
        switch (state)
        {
            case State.WaitingToStart:
                SetRigidBodyType(RigidbodyType2D.Static);
                break;
            case State.Playing:
                SetRigidBodyType(RigidbodyType2D.Dynamic);
                break;
            case State.Dead:
                HandlePlayerDeath();
                break;
        }
    }
    private void HandlePlayerDeath()
    {
        SetRigidBodyType(RigidbodyType2D.Static);
        SoundManager.PlaySound(SoundManager.Sound.Lose);
        GameManager.Instance.SetState(GameManager.State.GameOver);
    }

    private void SetRigidBodyType(RigidbodyType2D rigidBodyType)
    {
        _rigidbody.bodyType = rigidBodyType;
    }
}
