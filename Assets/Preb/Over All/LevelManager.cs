using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[CreateAssetMenu(menuName = "LevelManager")]
public class LevelManager : ScriptableObject
{
    [SerializeField] int MainMenuBuildIndex = 0;
    [SerializeField] int FirstLevelBuildIndex = 1;

    public delegate void OnLevelFinish();
    public static event OnLevelFinish onLevelFinish;
    internal static void LevelFinishes()
    {
        onLevelFinish?.Invoke();
    }

    public void GoToMainMenu()
    {
        LoadSceneByIndex(MainMenuBuildIndex);
    }
    public void LoadNextLevel()
    {
        LoadSceneByIndex(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void LoadFirstLevel()
    {
        LoadSceneByIndex(FirstLevelBuildIndex);
    }
    public void RestartCurrentLevel()
    {
        LoadSceneByIndex(SceneManager.GetActiveScene().buildIndex);

    }
    private void LoadSceneByIndex(int index)
    {
        SceneManager.LoadScene(index);
        GameStatic.SetGamePause(false); 
    }
}
