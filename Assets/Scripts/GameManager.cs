using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameAssetsSO selectedAssets;

    public GameAssetsSO SelectedAssets
    {
        get => selectedAssets;
    }
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
        Score.ResetScore();
        SetState(State.Begin);
    }

    public void SetState(State state)
    {
        GameState = state;
        OnStateChange?.Invoke(state);
    }
}
