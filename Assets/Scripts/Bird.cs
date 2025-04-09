using System;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public static Bird instance;
    public Action OnDeath;
    
    private Rigidbody2D _rigidbody;
    private const float JUMP_VALUE = 100f;

    private void Awake()
    {
        instance = this;
    }

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        _rigidbody.bodyType = RigidbodyType2D.Static;
        OnDeath?.Invoke();
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
