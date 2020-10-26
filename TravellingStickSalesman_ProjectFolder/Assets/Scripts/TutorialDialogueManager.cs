using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialDialogueManager : MonoBehaviour
{
    public GameObject seaPuzzle;
    public GameObject popUp;
    public Button invBtn;
    public Button mapBtn;
    public bool hasPlayerDoneTutorial = false;
    Animator popUpAnim;

    [Header("Dialogue")]
    public Text textDisplay;
    public string[] sentences;
    public AudioSource[] lines;
    private int textIndex = 5;
    private int audioIndex = 5;
    public float typingSpeed;
    public GameObject continueButton;
    public GameObject dialogueBox;

    void Start()
    {
        StickGameManager.Instance.SetTrader(Character.Genevieve);
        popUpAnim = popUp.GetComponent<Animator>();
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

        if(textDisplay.text == sentences[6])
        {
            continueButton.SetActive(false);
        }

        if(textDisplay.text == sentences[16])
        {
            dialogueBox.SetActive(false);
            popUp.SetActive(true);
            if(popUpAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                //Debug.Log("animation finish");
                NextSentence();
                seaPuzzle.SetActive(true);
                dialogueBox.SetActive(true);
            }
            else
            {
                //Debug.Log("playing");
            }
        }

        if(textDisplay.text == sentences[21])
        {
            EndDialogue();
            mapBtn.interactable = true;
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
        if(audioIndex == 6)
        {
            continueButton.SetActive(false);
        }
    }

    public void EndDialogue()
    {
        if(textIndex == 21)
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

    public void GoToSeaPuzzle()
    {
        if(hasPlayerDoneTutorial == true)
        {
            SceneManager.LoadScene("SeaPuzzle");
        }
    }
}
