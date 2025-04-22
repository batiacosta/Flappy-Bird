using System;
using UnityEngine;

public class CloudManager : MonoBehaviour
{
    [SerializeField] private Transform[] clouds;
    [SerializeField] private float cloudSpeed;
    private float _leftBound;
    private const float INICIAL_CLOUD_SPEED = 0.65f;
    
    private void Start()
    {
        if (Level.Instance == null) return;
        _leftBound = Level.Instance.LeftBound;
    }

    private void Update()
    {
        HandleCloudsMovement();
    }

    private void HandleCloudsMovement()
    {
        var speed = 0f;
        if (GameManager.GameState is GameManager.State.Begin or GameManager.State.GameOver)
        {
            speed = INICIAL_CLOUD_SPEED;
        }
        else
        {
            speed = cloudSpeed;
        }
        foreach (var cloud in clouds)
        {
            cloud.Translate(Vector3.left * speed * Time.deltaTime);// 0.95 to create parallaxs
            if (cloud.position.x < _leftBound - cloud.GetComponent<SpriteRenderer>().bounds.size.x)
            {
                cloud.position = new Vector2(-_leftBound + cloud.GetComponent<SpriteRenderer>().bounds.size.x, cloud.position.y);
            }
        }
    }
    
}
