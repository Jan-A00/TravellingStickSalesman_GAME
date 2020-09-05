using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//redundant script, kept just in case

public class TutorialManager : MonoBehaviour
{
    public TutorialDialogueManager dialogueManager;
    public Button seaPuzzleButton;

    void Update()
    {
        if(dialogueManager.textDisplay.text == dialogueManager.sentences[9])
        {
            seaPuzzleButton.interactable = true;
        }
    }

    public void GoToSeaPuzzle()
    {
        if(dialogueManager.hasPlayerDoneTutorial == true)
        {
            SceneManager.LoadScene("SeaPuzzle");
        }
    }
}
