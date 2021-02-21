using System;
using System.Collections;
using System.Collections.Generic;
using DataManagement;
using DataManagement.StateTypes;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public void Start()
    {
        if (!GameStateManager.StateExists) return;
        GameObject.FindGameObjectWithTag("MainMenu-ContinueButton").GetComponent<Button>().interactable = true;
    }

    public void NewGame()
    {
        GameStateManager.Instance.PurgeState();
        GameStateManager.Instance.InitializeGameState();
        SceneManager.LoadScene("Tutorial");
    }

    public void Continue()
    {
        GameStateManager.Instance.InitializeGameState();
        GameStateManager.Instance.ReturnToLevel();
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }
    
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }
}
