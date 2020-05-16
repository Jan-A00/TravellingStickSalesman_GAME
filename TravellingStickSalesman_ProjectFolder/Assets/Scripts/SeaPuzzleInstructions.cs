using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SeaPuzzleInstructions : MonoBehaviour
{
    public Text textDisplay;
    public string[] sentences;
    public int index;
    public float typingSpeed;
    public Button mapButton;
    public GameObject continueButton;
    public GameObject dialogueBox;
    public SeaPuzzleController controller;
    public bool hasPlayerSeenInstructions = false;
    public bool hasPlayerAccessedMap = false;
    public bool goNext = false;

    void Start()
    {
        if(hasPlayerSeenInstructions == false)
        {
            dialogueBox.SetActive(true);
            StartCoroutine(Type());
        }
    }
    
    void Update()
    {
        if(textDisplay.text == sentences[index])
        {
            continueButton.SetActive(true);
        }

        if(textDisplay.text == sentences[11])
        {
            EndInstructions();
        }

        if(textDisplay.text == sentences[11] && controller.winCon == true)
        {
            StartCoroutine(WinPuzzle());
            WinText();
            continueButton.SetActive(false);
        }

        if(textDisplay.text == sentences[17])
        {
            StartCoroutine(MapAppear());
            continueButton.SetActive(false);
        }

        if(textDisplay.text == sentences[21])
        {
            EndDialogue();
        }
    }

        IEnumerator Type()
    {
        foreach(char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    IEnumerator MapAppear()
    {
        yield return new WaitForSeconds(0.25f);
        mapButton.interactable = true;
    }

    IEnumerator WinPuzzle()
    {
        yield return new WaitForSeconds(0.04f);
        dialogueBox.SetActive(true);
    }

    public void EndInstructions()
    {
        dialogueBox.SetActive(false);
        hasPlayerSeenInstructions = true;
    }

    public void EndDialogue()
    {
        if(index == 21)
        {
            goNext = true;
            //Destroy(dialogueBox);
            dialogueBox.SetActive(false);
            StopAllCoroutines();
        }
    }

    public void WinText()
    {
        if(index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
    }

    public void NextSentence()
    {
        continueButton.SetActive(false);

        if(index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {
            textDisplay.text = "";
            continueButton.SetActive(false);
        }
    }
    public void NextSentenceAfterMap()
    {
        continueButton.SetActive(false);

        if(index < sentences.Length - 1 && hasPlayerAccessedMap == false)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
            hasPlayerAccessedMap = true;
        }
    }
}
