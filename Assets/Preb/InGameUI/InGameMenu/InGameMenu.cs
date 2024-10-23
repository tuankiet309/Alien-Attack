using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameMenu : MonoBehaviour
{

    [SerializeField] UIManager manager;
    [SerializeField] LevelManager levelManager;
    private void Start()
    {

    }

    public void NextLevel()
    {
        levelManager.LoadNextLevel();
    }

    public void GoToMenu()
    {
        levelManager.GoToMainMenu();
    }

    public void RestartLevel()
    {
        levelManager.RestartCurrentLevel();
    }

    public void ResumeLevel()
    {
        manager.SwitchToGameplay();
    }
}
