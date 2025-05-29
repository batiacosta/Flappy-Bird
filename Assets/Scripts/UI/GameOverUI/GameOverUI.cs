using System;
using UnityEngine;
using UnityEngine.UIElements;

public class GameOverUI : MonoBehaviour
{

    private Button _retryButton;
    private Button _mainMenuButton;
    private Label _highScoreLabel;
    private Label _scoreLabel;
    private VisualElement _root;
    private void Start()
    {
        _root = GetComponent<UIDocument>().rootVisualElement;
        _scoreLabel = _root.Q<Label>("ScoreLabel");
        _highScoreLabel = _root.Q<Label>("MaxScoreLabel");
        _retryButton = _root.Q<Button>("RetryButton");
        _mainMenuButton = _root.Q<Button>("MainMenuButton");
        

        _retryButton.clicked += RetryButtonClicked;
        _mainMenuButton.clicked += MainMenubuttonClicked;
        
        Hide();
        GameManager.OnStateChange += OnStateChange;
    }
    private void OnDestroy()
    {
        _retryButton.clicked -= RetryButtonClicked;
        _mainMenuButton.clicked -= MainMenubuttonClicked;
        
        GameManager.OnStateChange += OnStateChange;
    }

    private void RetryButtonClicked()
    {
        SoundManager.PlaySound(SoundManager.Sound.ButtonClick);
        Loader.LoadScene(Loader.Scene.GameScene);
    }

    private void MainMenubuttonClicked()
    {
        SoundManager.PlaySound(SoundManager.Sound.ButtonClick);
        Loader.LoadScene(Loader.Scene.MainMenu);
    }
    
    private void OnStateChange(GameManager.State state)
    {
        if(state == GameManager.State.GameOver) OnDeath();
    }

    private void OnDeath()
    {
        Show();
        _scoreLabel.text = "YOUR SCORE: " + Score.CurrentScore.ToString();
        _highScoreLabel.text = "HIGHEST SCORE: " + Score.GetHighScore().ToString();
    }

    private void Show()
    {
        _root.style.display = DisplayStyle.Flex;
        _retryButton.Focus();
    } 
    
    private void Hide() => _root.style.display = DisplayStyle.None;


}
