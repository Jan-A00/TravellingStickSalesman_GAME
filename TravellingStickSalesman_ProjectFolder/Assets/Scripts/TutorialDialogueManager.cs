using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//redundant script, kept just in case

public class TutorialDialogueManager : MonoBehaviour
{
    public Text textDisplay;
    public string[] sentences;
    private int index;
    public float typingSpeed;
    public GameObject continueButton;
    public GameObject dialogueBox;
    public Button mapButton;
    public Button inventoryButton;
    public bool hasPlayerAccessedMap = false;
    public bool hasPlayerAccessedInventory = false;
    public bool hasPlayerDoneTutorial = false;

    void Start()
    {
        if(hasPlayerDoneTutorial == false)
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

        if(textDisplay.text == sentences[5])
        {
            StartCoroutine(InventoryAppear());
            continueButton.SetActive(false);
        }

        if(textDisplay.text == sentences[9])
        {
            StartCoroutine(MapAppear());
            continueButton.SetActive(false);
        }

        if(textDisplay.text == sentences[13])
        {
            EndDialogue();
        }
    }

    IEnumerator MapAppear()
    {
        yield return new WaitForSeconds(0.25f);
        mapButton.interactable = true;
    }

    IEnumerator InventoryAppear()
    {
        yield return new WaitForSeconds(0.25f);
        inventoryButton.interactable = true;
    }

    IEnumerator Type()
    {
        foreach(char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void EndDialogue()
    {
        dialogueBox.SetActive(false);
        hasPlayerDoneTutorial = true;
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

    public void NextSentenceAfterInventory()
    {
        continueButton.SetActive(false);

        if(index < sentences.Length - 1 && hasPlayerAccessedInventory == false)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
            hasPlayerAccessedInventory = true;
        }
    }
}
