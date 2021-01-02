using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class gameLoader
{
    public static scene targetScene;
    public enum scene
    {
        gameScene,
        loading,
    }
    public static void loadScene(scene s)
    {
        SceneManager.LoadScene(scene.loading.ToString());
        targetScene = s;
    }
    public static void loadTargetScene()
    {
        SceneManager.LoadScene(targetScene.ToString());
    }
}
