using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverWindow : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private Button retyButton;

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
