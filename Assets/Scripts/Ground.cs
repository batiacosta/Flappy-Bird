using System;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] private Transform groundTransform;
    private const float SPEED = 30f;
    private Vector3 _groundStartPosition;

    private void Start()
    {
        _groundStartPosition = groundTransform.position;
    }

    private void Update()
    {
        if(GameManager.GameState != GameManager.State.Playing) return;
        HandleGroundMovement();
    }

    private void HandleGroundMovement()
    {
        groundTransform.Translate(Vector2.left * SPEED * Time.deltaTime);
        if (groundTransform.position.x < 0)
        {
            groundTransform.position = _groundStartPosition;
        }
    }
}
