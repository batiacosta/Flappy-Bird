using System;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    [SerializeField] private Sprite pipe;
    [SerializeField] private Transform pipeBody;
    [SerializeField] private Transform pipeHead;
    private static GameAssets _instance;

    public static GameAssets GetInstance()
    {
        return _instance;
    }

    private void Awake()
    {
        _instance = this;
    }

    public Transform GetPipeHead() => pipeHead;
    public Transform GetPipeBody() => pipeBody;
    public Sprite GetPipe() => pipe;
}
