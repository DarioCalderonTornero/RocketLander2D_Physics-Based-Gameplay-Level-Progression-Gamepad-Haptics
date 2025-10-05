using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{

    public enum Scene
    {
        GameScene,
        MainMenuScene,
        GameFinishScene
    }
    public static void LoadScene(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }
}
