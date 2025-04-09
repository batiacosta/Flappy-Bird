using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    public enum Scene
    {
        GameScene, Loading, MainMenu
    }

    public static void LoadScene(Scene  scene)
    {
        SceneManager.LoadScene(Scene.Loading.ToString());
        
        var targetScene = scene.ToString();
        Await(()=> LoadTargetScene(targetScene));
    }

    private static void LoadTargetScene(String targetScene)
    {
        SceneManager.LoadScene(targetScene);
    }

    private static async Awaitable Await(Action action)
    {
        await Awaitable.WaitForSecondsAsync(0.5f);
        action?.Invoke();
    }
}