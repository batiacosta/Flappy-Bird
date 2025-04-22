using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverWindow : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text highScoreText;

    private void Start()
    {
        GameManager.OnStateChange += OnStateChange;
        Hide();
    }

    private void OnDestroy()
    {
        GameManager.OnStateChange -= OnStateChange;
    }

    private void OnStateChange(GameManager.State state)
    {
        if(state == GameManager.State.GameOver) OnDeath();
    }

    private void OnDeath()
    {
        Show();
        scoreText.text = "YOUR SCORE: " + PipesManager.instance.GetAchievedPipes().ToString();
        highScoreText.text = "HIGHEST SCORE: " + Score.GetHighScore().ToString();
    }

    public void Retry()
    {
        SoundManager.PlaySound(SoundManager.Sound.ButtonClick);
        Loader.LoadScene(Loader.Scene.GameScene);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    public void MainMenu()
    {
        SoundManager.PlaySound(SoundManager.Sound.ButtonClick);
        Loader.LoadScene(Loader.Scene.MainMenu);
    }

    public void ButtonOverPlaySound()
    {
        SoundManager.PlaySound(SoundManager.Sound.ButtonOver);
    }
}
