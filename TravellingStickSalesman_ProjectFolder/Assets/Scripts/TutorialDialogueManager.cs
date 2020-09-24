using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialDialogueManager : MonoBehaviour
{
    public Text textDisplay;
    public string[] sentences;
    public AudioSource[] lines;
    private int textIndex;
    private int audioIndex;
    public float typingSpeed;
    public GameObject seaPuzzle;
    public GameObject continueButton;
    public GameObject dialogueBox;
    public bool hasPlayerDoneTutorial = false;

    void Start()
    {
        if(hasPlayerDoneTutorial == false)
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

        if(textDisplay.text == sentences[5])
        {
            //continueButton.SetActive(false);
        }

        if(textDisplay.text == sentences[15])
        {
            seaPuzzle.SetActive(true);
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
        yield return new WaitForSeconds(1);
        continueButton.SetActive(true);
    }

    public void EndDialogue()
    {
        if(textIndex == 20)
        {
            hasPlayerDoneTutorial = true;
            //Destroy(dialogueBox);
            dialogueBox.SetActive(false);
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

    public void GoToSeaPuzzle()
    {
        if(hasPlayerDoneTutorial == true)
        {
            SceneManager.LoadScene("SeaPuzzle");
        }
    }
}
