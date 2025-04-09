using UnityEngine;

public class MainMenuWindow : MonoBehaviour
{
    public void Play()
    {
        SoundManager.PlaySound(SoundManager.Sound.ButtonClick);
        Loader.LoadScene(Loader.Scene.GameScene);
    }

    public void Quit()
    {
        SoundManager.PlaySound(SoundManager.Sound.ButtonClick);
        Application.Quit();
    }
    public void ButtonOverPlaySound()
    {
        SoundManager.PlaySound(SoundManager.Sound.ButtonOver);
    }
}
