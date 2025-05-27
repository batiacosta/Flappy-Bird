using System;
using UnityEngine;
using UnityEngine.UIElements;

public class InGameScoreUI : MonoBehaviour
{
    private VisualElement _instructionsContainer;
    private Label _scoreText;
    private Label _maxScoreText;
    private void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        _maxScoreText = root.Q<Label>("MaxScoreLabel");
        _scoreText = root.Q<Label>("ScoreLabel");
        _instructionsContainer = root.Q<VisualElement>("InstructionsContainer");
        
        OnGameStateChanged(GameManager.GameState);
        OnScoreChanged(Score.CurrentScore);
        _maxScoreText.text = "Highest score: " + Score.GetHighScore();
        Score.OnScoreChanged += OnScoreChanged;
        GameManager.OnStateChange += OnGameStateChanged;
    }

    private void OnGameStateChanged(GameManager.State gameState)
    {
        _instructionsContainer.style.display = gameState == GameManager.State.Begin ? DisplayStyle.Flex : DisplayStyle.None;
    }

    private void OnDestroy()
    {
        Score.OnScoreChanged += OnScoreChanged;
        GameManager.OnStateChange += OnGameStateChanged;
    }

    private void OnScoreChanged(int score)
    {
        _scoreText.text = Score.CurrentScore.ToString();
    }
}
