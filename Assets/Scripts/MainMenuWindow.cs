using UnityEngine;

public class MainMenuWindow : MonoBehaviour
{
    public void Play()
    {
        Loader.LoadScene(Loader.Scene.GameScene);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
