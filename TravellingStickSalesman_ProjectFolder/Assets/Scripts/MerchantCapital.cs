﻿using System.Collections;
using System.Collections.Generic;
using DataManagement;
using DataManagement.StateTypes;
using UnityEngine;
using UnityEngine.UI;
using DataManagement;
using DataManagement.StateTypes;

public class MerchantCapital : MonoBehaviour
{
    //if there are other things for this scene put them here
    public GameObject invBtn;
    public EndingType endingType;

    [Header("Dialogue")]
    public Text textDisplay;
    public string[] sentences;
    public AudioSource[] lines;
    private int textIndex;
    private int audioIndex;
    public float typingSpeed;
    public GameObject continueButton;
    public GameObject dialogueBox;
    // Start is called before the first frame update

    void Start()
    {
        EndingType endingType = GameStateManager.Instance.CurrentGameEndingType();
        Debug.Log(endingType);
        invBtn = GameObject.FindGameObjectWithTag("Inventory-OpenButton");
        invBtn.SetActive(false);
        dialogueBox.SetActive(true);
        StartCoroutine(Type());
        StartCoroutine(Speak());
    }

    // Update is called once per frame
    void Update()
    {
        if(textDisplay.text == sentences[textIndex])
        {
            continueButton.SetActive(true);
        }

        if (endingType == EndingType.FirstEnding)
        {
            Debug.Log("one");
        }

        if (endingType == EndingType.SecondEnding)
        {
            Debug.Log("two");
        }

        if (endingType == EndingType.ThirdEnding)
        {
            Debug.Log("three");
        }

        if(textDisplay.text == sentences[24])
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
        if(textIndex == 24)
        {
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
}
