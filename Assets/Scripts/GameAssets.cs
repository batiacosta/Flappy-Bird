using System;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    [SerializeField] private Sprite pipe;
    private static GameAssets _instance;

    public static GameAssets GetInstance()
    {
        return _instance;
    }

    private void Awake()
    {
        _instance = this;
    }
}
