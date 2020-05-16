using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialAudioManager : MonoBehaviour
{
    public AudioSource[] lines;
    public int index;
    public GameObject continueButton;
    public Button mapButton;
    public Button inventoryButton;
    public bool hasPlayerAccessedMap = false;
    public bool hasPlayerAccessedInventory = false;
    public bool hasPlayerDoneTutorial = false;
    
    void Start()
    {
        StartCoroutine(Speak());
    }

    void Update()
    {
        
    }

    IEnumerator Speak()
    {
        yield return new WaitForSeconds(0.05f);
        lines[index].Play();
        yield return new WaitForSeconds(2);
        continueButton.SetActive(true);
    }

    public void NextLine()
    {
        continueButton.SetActive(false);

        if(index < lines.Length - 1)
        {
            lines[index].Stop();
            index++;
            StartCoroutine(Speak());
        }
        else
        {
            continueButton.SetActive(false);
        }
    }
}
