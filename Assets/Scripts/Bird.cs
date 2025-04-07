using System;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private const float JUMP_VALUE = 100f;
    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        if(_rigidbody == null) return;
        InputHandler.OnTap += OnTap;
    }

    private void OnDestroy()
    {
        InputHandler.OnTap -= OnTap;
    }

    private void OnTap()
    {
        Jump();
    }

    private void Jump()
    {
        _rigidbody.linearVelocityY = Vector2.up.y * JUMP_VALUE;
    }
}
