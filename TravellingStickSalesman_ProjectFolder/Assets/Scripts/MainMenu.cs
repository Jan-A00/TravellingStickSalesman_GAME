using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void NewGame ()
    {
        Debug.Log("Welcome to the game.");
    }

    public void QuitGame ()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }
}
