using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Notes
{
    C1 = 0, //important to start from 0
    D = 1,
    E = 2,
    F = 3,
    G = 4,
    A = 5,
    B = 6,
    C2 = 7
}



public class MusicalMushroom : MonoBehaviour
{
    public Notes note; //which note is this?
    public MusicalMushroomSettings settings;
    //public Sprite[] mushroomImage;
    //public AudioClip[] mushroomAudio;
    // These all come from the settings object now.
    public MusicalMushroomMatcher matcher;
    // Start is called before the first frame update
    private CheckResult checkResult;

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = settings.GetMushroomSprite(note);
        GetComponent<AudioSource>().clip = settings.GetMushroomAudio(note);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetSpriteBackToNormalAfterDelay()
    {
        GetComponent<SpriteRenderer>().sprite = settings.GetMushroomSprite(note);
    }

    void OnMouseDown()
    {
        //Debug.Log("Clicked " + iType);
        PlayAndGlow();
        matcher.AddNote(note);
        checkResult = matcher.SequentialChecker();
        switch (checkResult)
        {
            case CheckResult.IncompleteAndIncorrect:
            {
                Debug.Log("Failed before finishing sequence!");
                matcher.SoftReset();
                break;
            }
            case CheckResult.IncompleteButCorrectSoFar:
            {
                Debug.Log("Not yet complete but everything is ok so far.");
                matcher.UpdateDebugText();
                matcher.HighlightRune(note);
                break;
            }
            case CheckResult.CompleteButIncorrect:
            {
                Debug.Log("Failed the last note");
                matcher.SoftReset();
                break;
            }
            case CheckResult.CompleteAndCorrect:
            {
                Debug.Log("Success!");
                matcher.UpdateDebugText();
                matcher.HighlightRune(note);
                matcher.PuzzleComplete();
                break;
            }
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void PlayAndGlow()
    {
        GetComponent<AudioSource>().Play();
        GetComponent<SpriteRenderer>().sprite = settings.GetMushroomHighlightSprite(note);
        Invoke("SetSpriteBackToNormalAfterDelay", settings.highightOnClickDuration);
    }

    public void PlayAndGlowWithDelay(float delay)
    {
        Invoke("PlayAndGlow", delay);
    }
}
