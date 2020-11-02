using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SeaPuzzleInstructions : MonoBehaviour
{
    public GameObject shrinePuzzle;
    public SeaPuzzleController controller;
    public bool hasPlayerSeenInstructions = false;
    public bool goNext = false;

    [Header("Dialogue")]
    public Text textDisplay;
    public string[] sentences;
    public AudioSource[] lines;
    private int audioIndex; // set to 8 to skip dialogue
    private int textIndex; //set to 8 to skip dialogue
    public float typingSpeed;
    public GameObject continueButton;
    public GameObject dialogueBox;

    void Start()
    {
        StickGameManager.Instance.SetTrader(Character.Baz);
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

        if(textDisplay.text == sentences[9])
        {
            EndInstructions();
        }

        if(textDisplay.text == sentences[9] && controller.winCon == true)
        {
            WinText();
            continueButton.SetActive(false);
        }

        if(textDisplay.text == sentences[15])
        {
            shrinePuzzle.SetActive(true);
        }

        if(textDisplay.text == sentences[21])
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
        yield return new WaitForSeconds(1);
        continueButton.SetActive(true);
    }

    public void EndInstructions()
    {
        dialogueBox.SetActive(false);
        hasPlayerSeenInstructions = true;
    }

    public void EndDialogue()
    {
        if(textIndex == 21)
        {
            goNext = true;
            //Destroy(dialogueBox);
            dialogueBox.SetActive(false);
            StopAllCoroutines();
        }
    }

    public void NextSentence()
    {
        continueButton.SetActive(false);
        StopAllCoroutines();

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
            audioIndex++;
            textDisplay.text = "";
            dialogueBox.SetActive(true);
            StartCoroutine(Type());
            StartCoroutine(Speak());
        }
    }
}
