using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShrinePuzzleInstructions : MonoBehaviour
{
    public Text textDisplay;
    public string[] sentences;
    public AudioSource[] lines;
    private int audioIndex;
    private int textIndex;
    public float typingSpeed;
    public Button mapButton;
    public GameObject continueButton;
    public GameObject dialogueBox;
    public GameObject merchantCapital;
    public GameObject kaede;
    public ShrinePuzzleController controller;
    public bool hasPlayerSeenInstructions = false;
    public bool goNext = false;

    void Start()
    {
        if(hasPlayerSeenInstructions == false)
        {
            dialogueBox.SetActive(true);
            StartCoroutine(Type());
            StartCoroutine(Speak());
        }
    }

    void Update()
    {
        if(textDisplay.text == sentences[textIndex])
        {
            continueButton.SetActive(true);
        }

        if(textDisplay.text == sentences[13])
        {
            EndInstructions();
        }

        if(textDisplay.text == sentences[13] && controller.winCon == true)
        {
            StartCoroutine(WinPuzzle());
            WinText();
            continueButton.SetActive(false);
        }

        if(textDisplay.text == sentences[20])
        {
            EndDialogue();
        }

    }

    IEnumerator Type()
    {
        foreach(char letter in sentences[textIndex].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

        IEnumerator Speak()
    {
        yield return new WaitForSeconds(0.05f);
        lines[audioIndex].Play();
        //yield return new WaitForSeconds(2);
        continueButton.SetActive(true);
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
        if(textIndex == 20)
        {
            goNext = true;
            //Destroy(dialogueBox);
            dialogueBox.SetActive(false);
            merchantCapital.SetActive(true);
            kaede.SetActive(false);
            StopAllCoroutines();
        }
    }    

    public void NextSentence()
    {
        continueButton.SetActive(false);

        if(textIndex < sentences.Length - 1)
        {
            textIndex++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }

        if(audioIndex < lines.Length - 1)
        {
            lines[audioIndex].Stop();
            audioIndex++;
            StartCoroutine(Speak());
        }

        else
        {
            textDisplay.text = "";
            continueButton.SetActive(false);
        }
    }
    
    public void WinText()
    {
        if(textIndex < sentences.Length - 1)
        {
            textIndex++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
    }
}
