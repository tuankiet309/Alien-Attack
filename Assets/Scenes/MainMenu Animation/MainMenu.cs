using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Button startBTN;
    [SerializeField] Button controlBTN;
    [SerializeField] Button quitBTN;
    [SerializeField] Button backBTN;
    [SerializeField] CanvasGroup MainMenuArea;
    [SerializeField] CanvasGroup ControlArea;
    [SerializeField] LevelManager levelManager;
    void Start()
    {
        startBTN.onClick.AddListener(StartGame);
        quitBTN.onClick.AddListener(QuitGame);
        controlBTN.onClick.AddListener(SwitchToControl);
        backBTN.onClick.AddListener(SwitchToMainMenu);
        SwitchToMainMenu();
    }

    private void SwitchToControl()
    {
        ControlArea.blocksRaycasts = true;
        ControlArea.alpha = 1;
        ControlArea.interactable = true;

        MainMenuArea.blocksRaycasts = false;
        MainMenuArea.alpha = 0;
        MainMenuArea.interactable = false;
    }

    private void SwitchToMainMenu()
    {
        ControlArea.blocksRaycasts = false;
        ControlArea.alpha = 0;
        ControlArea.interactable = false;

        MainMenuArea.blocksRaycasts = true;
        MainMenuArea.alpha = 1;
        MainMenuArea.interactable = true;
    }



    private void QuitGame()
    {
        throw new NotImplementedException();
    }

    private void StartGame()
    {
        levelManager.LoadFirstLevel();
    }
}
