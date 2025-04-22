using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Action<State> OnStateChange;
    public enum State
    {
        Paused, GameOver, Playing, Begin
    }

    public static State GameState
    {
        get;
        private set;
    }

    public static GameManager Instance;

    private void Awake()
    {
        if(Instance != null) Destroy(gameObject);
        Instance = this;
    }

    private void Start()
    {
        SetState(State.Begin);
    }

    public void SetState(State state)
    {
        GameState = state;
        OnStateChange?.Invoke(state);
    }
}
