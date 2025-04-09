using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverWindow : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;

    private void Start()
    {
        Bird.instance.OnDeath += OnDeath;
        Hide();
    }

    private void OnDestroy()
    {
        Bird.instance.OnDeath -= OnDeath;
    }

    private void OnDeath()
    {
        Show();
        scoreText.text = Level.instance.GetAchievedPipes().ToString();
    }

    public void Retry()
    {
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
        Loader.LoadScene(Loader.Scene.MainMenu);
    }
}
