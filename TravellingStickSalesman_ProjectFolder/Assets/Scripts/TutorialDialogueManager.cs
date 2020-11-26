using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialDialogueManager : MonoBehaviour
{
    public GameObject popUp;
    public GameObject invArrow;
    // public InventoryManager invMngr;
    // public Button invBtn;
    public Button mapBtn;
    public Button backBtn;
    public bool hasPlayerDoneTutorial = false;
    Animator popUpAnim;

    [Header("Dialogue")]
    public Text textDisplay;
    public string[] sentences;
    public AudioSource[] lines;
    private int textIndex;
    private int audioIndex;
    public float typingSpeed;
    public GameObject continueButton;
    public GameObject dialogueBox;

    void Start()
    {
        // TODO: Hook up to GameStateManager and re-enable
        // invMngr.hasTraded = true;
        mapBtn.interactable = false;
        // invBtn.interactable = false;
        popUpAnim = popUp.GetComponent<Animator>();
        StickGameManager.Instance.SetTrader(Character.Genevieve);
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

        if(textDisplay.text == sentences[4])
        {
            continueButton.SetActive(false);
            // invBtn.interactable = true;
            invArrow.SetActive(true);
            backBtn.onClick.AddListener(() => AfterInventory());
            // invBtn.onClick.AddListener(() => HideDialogueBox());
        }

        if(textDisplay.text == sentences[5])
        {
            continueButton.SetActive(true);
            // invBtn.interactable = false;
        }

        if(textDisplay.text == sentences[14])
        {
            dialogueBox.SetActive(false);
            popUp.SetActive(true);
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
        if(audioIndex == 4)
        {
            continueButton.SetActive(false);
        }
    }

    public void EndDialogue()
    {
        if(textIndex == 18)
        {
            hasPlayerDoneTutorial = true;
            //Destroy(dialogueBox);
            dialogueBox.SetActive(false);
            mapBtn.interactable = true;
            // invBtn.interactable = true;
            // invMngr.hasTraded = false;
            textIndex++;
            audioIndex++;
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

    public void HideDialogueBox()
    {
        dialogueBox.SetActive(false);
    }

    public void AfterInventory()
    {
        Character c = StickGameManager.Instance.GetTrader();
        if(textDisplay.text == sentences[4] && c == Character.Genevieve)
        {
            dialogueBox.SetActive(true);

            textDisplay.text = "";
            textIndex++;
            StartCoroutine(Type());

            lines[audioIndex].Stop();
            audioIndex++;
            StartCoroutine(Speak());

            invArrow.SetActive(false);
        }
    }

    public void GoToSeaPuzzle()
    {
        // invBtn.interactable = false;
        SceneManager.LoadScene("SeaPuzzle");
    }
}
