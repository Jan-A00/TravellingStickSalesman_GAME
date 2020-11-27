using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MushroomPuzzleInstructions : MonoBehaviour
{
    public MusicalMushroomSettings settings;
    public MusicalMushroomMatcher matcher;
    public bool hasPlayerSeenInstructions = false;
    public GameObject popUp;
    public Button invBtn;
    public Button mapBtn;
    Animator popUpAnim;

    [Header("Dialogue")]
    public Text textDisplay;
    public string[] sentences;
    public AudioSource[] lines;
    private int audioIndex;
    private int textIndex;
    public float typingSpeed;
    public GameObject continueButton;
    public GameObject dialogueBox;

    void Start()
    {
        invBtn = GameObject.FindGameObjectWithTag("Inventory-OpenButton").GetComponent<Button>();
        mapBtn.interactable = false;
        invBtn.interactable = false;
        popUpAnim = popUp.GetComponent<Animator>();
        StickGameManager.Instance.SetTrader(Character.Beardfacé);
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

        if(textDisplay.text == sentences[10])
        {
            EndInstructions();
            settings.puzzleEnabled = true;
        }

        if(textDisplay.text == sentences[10] && matcher.winCon == true)
        {
            WinText();
            continueButton.SetActive(false);
        }

        if(textDisplay.text == sentences[15])
        {
            popUp.SetActive(true);
            dialogueBox.SetActive(false);
            if(popUpAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                //Debug.Log("animation finish");
                NextSentence();
                dialogueBox.SetActive(true);
            }
        }

        if(textDisplay.text == sentences[18])
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
        if(textIndex == 18)
        {
            //Destroy(dialogueBox);
            dialogueBox.SetActive(false);
            textIndex++;
            audioIndex++;
            StopAllCoroutines();
        }
        if (textIndex == 19)
        {
            mapBtn.interactable = true;
            invBtn.interactable = true;
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
            settings.puzzleEnabled = false;
            StartCoroutine(Type());
            StartCoroutine(Speak());
        }
    }

    public void GoToPotionPuzzle()
    {
        invBtn.interactable = false;
        SceneManager.LoadScene("PotionPuzzle");
    }
}
