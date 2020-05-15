using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public TutorialDialogueManager dialogueManager;
    public Button seaPuzzleButton;

    void Update()
    {
        if(dialogueManager.hasPlayerDoneTutorial == true)
        {
            seaPuzzleButton.interactable = true;
        }
    }

    public void GoToSeaPuzzle()
    {
        SceneManager.LoadScene("SeaPuzzle");
    }
}
