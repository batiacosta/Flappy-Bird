using System;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuUI : MonoBehaviour
{
    private Button _playButton;
    private Button _quitButton;
    private void Start()
    {
        // Look for the buttons and add listeners
        var root = GetComponent<UIDocument>().rootVisualElement;
        _playButton = root.Q<Button>("PlayButton");
        _quitButton = root.Q<Button>("QuitButton");
        // Set play button as default
        _playButton.Focus();
        _playButton.clicked += PlayButtonClicked;
        _quitButton.clicked += QuitButtonClicked;
    }

    private void QuitButtonClicked()
    {
        SoundManager.PlaySound(SoundManager.Sound.ButtonClick);
        Application.Quit();
    }

    private void PlayButtonClicked()
    {
        SoundManager.PlaySound(SoundManager.Sound.ButtonClick);
        Loader.LoadScene(Loader.Scene.GameScene);
    }

    private void OnDestroy()
    {
        // All required unsubscriptions
        _playButton.clicked -= PlayButtonClicked;
        _quitButton.clicked -= QuitButtonClicked;
    }
}
