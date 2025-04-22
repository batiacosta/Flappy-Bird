using System;
using UnityEngine;

public class Level : MonoBehaviour
{
    public static Level Instance;
    public float LeftBound { get; private set;}
    public float RightBound { get; private set;}
    public float TopBound { get; private set;}
    public float BottomBound { get; private set;}
    

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SetBoundaries();
    }

    private void SetBoundaries()
    {
        SetLeftBound();
    }

    private void SetLeftBound() // Can be public to be call when the screen resizes
    {
        var cameraOrtho = Camera.main.orthographicSize * 2;
        var cameraWith = cameraOrtho * Camera.main.aspect;
        var cameraPositionX = Camera.main.transform.position.x;
        LeftBound = cameraPositionX - (cameraWith / 2);
    }
}
