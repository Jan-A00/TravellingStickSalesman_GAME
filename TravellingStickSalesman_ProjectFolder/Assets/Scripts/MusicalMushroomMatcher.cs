using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Object = System.Object;
using Random = UnityEngine.Random;
using static Utilities;
using System.Linq;
using Unity.UNetWeaver;

public enum CheckResult
{
    IncompleteButCorrectSoFar,
    IncompleteAndIncorrect,
    CompleteButIncorrect,
    CompleteAndCorrect
}

public class MusicalMushroomMatcher : MonoBehaviour
{
    public MusicalMushroomSettings settings;
    public Text txtCurrentItems;
    public Text txtCorrectItems;
    public Notes[] correctNotes;
    public GameObject[] mushroomsInScene;
    public List<Notes> currentMushrooms;
    public bool allowDuplicates = false;
    public bool randomNotes = true;
    public int randomMinNotes = 8;
    public int randomMaxNotes = 8;
    public bool playSequenceAtStart = true;
    public double initialPlaybackDelay = 2f;
    public double delayBetweenNotesDuringPlayback = 1.0f;

    private double ApproximateFinish => initialPlaybackDelay + (delayBetweenNotesDuringPlayback * (correctNotes.Length + 1));
    public bool NoIngredients => BooleanThatLogsIfTrue(currentMushrooms.Count == 0, "No Ingredients");
    public bool NotEnoughIngredients =>
        BooleanThatLogsIfTrue(currentMushrooms.Count < correctNotes.Length, "Not Enough");
    public bool EnoughIngredients => BooleanThatLogsIfTrue(currentMushrooms.Count == correctNotes.Length, "Enough Ingredients");
    public bool TooManyIngredients => BooleanThatLogsIfTrue(currentMushrooms.Count > correctNotes.Length, "Too Many Ingredients");
    
    // Start is called before the first frame update
    public void Start()
    {
        ResetPlayerNoteInput();
        try
        {
            GenerateNotesSequence();
        }
        catch (ApplicationException handleError)
        {
            Debug.LogError("You don't have enough mushroom types");
            return;
        }

        SetupRunes();
        PlayNoteSequence();
        StartCoroutine(PostNotePlaybackEvent((float) ApproximateFinish));
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    private void PlayNoteSequence()
    {
        if (playSequenceAtStart)
        {
            mushroomsInScene = GameObject.FindGameObjectsWithTag("Musical-Mushroom");
            int noteIndex = 0;
            foreach (Notes notes in correctNotes)
            {
                foreach (GameObject mushroom in mushroomsInScene)
                {
                    if (mushroom.GetComponent<MusicalMushroom>().note != notes) continue;
                    var startDelay = initialPlaybackDelay + (delayBetweenNotesDuringPlayback * noteIndex);
                    Debug.Log("Setting playback delay");
                    Debug.Log(startDelay);
                    mushroom.GetComponent<MusicalMushroom>().PlayAndGlowWithDelay((float) startDelay); 
                }
                noteIndex++;
            }
        }
    }

    private void GenerateNotesSequence()
    {
        if (!randomNotes) { Debug.Log("Not randomizing the notes."); return; }
        if (!allowDuplicates)
        {
            Debug.Log("Duplicates are not allowed...");
            correctNotes = new Notes[Random.Range(randomMinNotes, randomMaxNotes + 1)];
            if (settings.noteArray.Length < correctNotes.Length)
            {
                throw new ApplicationException();
            }
        }
        Debug.Log("Generating Note Sequence... ");
        List<int> blacklistedMushrooms = new List<int>();
        for (int i = 0; i < correctNotes.Length; i++)
        {
            bool added = false;
            while (!added)
            {
                int randnum = Random.Range(0, settings.noteArray.Length);
                if (allowDuplicates)
                {
                    correctNotes[i] = settings.noteArray[randnum];
                    added = true;
                }
                else if (!blacklistedMushrooms.Contains(randnum))
                {
                    correctNotes[i] = settings.noteArray[randnum];
                    blacklistedMushrooms.Add(randnum);
                    added = true;
                }
                Debug.Log("Randomized number = " + randnum + " selected note = " + settings.noteArray[randnum]);
            }
        }
        txtCorrectItems.text = MushroomsToString(correctNotes);
    }

    private void SetupRunes()
    {
        Debug.Log("Inside SetupRunes(), setting up runes...");
        for (int i = 0; i < settings.runeSequence.Length; i++)
        {
            MusicalWallRune rune = settings.runeSequence[i];
            if (i < correctNotes.Length)
            {
                Notes note = correctNotes[i];
                Debug.Log("Setting Rune " + rune + " to Note: " + note);
                rune.SetNote(note);
            }
            else
            {
                Debug.Log("Nothing to assign to Rune " + rune + ", hiding it.");
                rune.Hide();
            }
        }
    }

    public void ResetPlayerNoteInput()
    {
        currentMushrooms = new List<Notes>();
    }

    public void SoftReset()
    {
        // TODO: Need to work out how to reset the puzzle without reloading the scene and randomizing everything again.
        ResetPlayerNoteInput();
        txtCurrentItems.text = "Reset!";
        foreach (MusicalWallRune rune in settings.runeSequence)
        {
            rune.SetSpriteBackToNormal();
        }
    }

    public CheckResult SequentialChecker()
    {
        Debug.Log("currentMushrooms.Count = " + currentMushrooms.Count);
        Debug.Log("correctNotes.Length = " + correctNotes.Length);
        bool correctSoFar = true;
        for ( int i = 0; i < currentMushrooms.Count; i++)
        {
            Debug.Log("Checking item " + i + " in note sequence");
            if (currentMushrooms[i] == correctNotes[i])
            {
                Debug.Log("Last note was correct.");
            } else {
                Debug.Log("Last note was not correct.");
                correctSoFar = false;
            }
        }

        if (currentMushrooms.Count < correctNotes.Length)
        {
            Debug.Log("Not Enough Notes Yet");
            if (correctSoFar)
            {
                Debug.Log("Waiting for next note.");
                return CheckResult.IncompleteButCorrectSoFar;
            } else {
                Debug.Log("Incorrect note before reeaching end of sequence, reseting.");
                return CheckResult.IncompleteAndIncorrect;
            }
        } else {
            Debug.Log("Enough Notes.");
            if (correctSoFar)
            {
                Debug.Log("Complete and Correct.");
                return CheckResult.CompleteAndCorrect;
            } else {
                Debug.Log("Last note was incorrect.");
                return CheckResult.CompleteButIncorrect;
            }
        }
    }

    IEnumerator PostNotePlaybackEvent(float time)
    {
        yield return new WaitForSeconds(time);
        // TODO: What do we want to trigger after the notes have all played back.
    }
    
    public void AddNote(Notes mushroomtype)
    {
        currentMushrooms.Add(mushroomtype);
    }

    public void HighlightRune(Notes note)
    {
        MusicalWallRune matchedRune = settings.runeSequence.Single(rune => rune.note == note);
        matchedRune.Highlight();
    }
    
    public void UpdateDebugText()
    {
        txtCurrentItems.text = MushroomsToString( currentMushrooms.ToArray() );
    }

    public string MushroomsToString(Notes[] shrooms)
    {
        string mushroom_str = "";
        foreach (Notes shroom in shrooms)
        {
            mushroom_str += shroom.ToString() + ", ";
        }
        return mushroom_str;
    }

    public void PuzzleComplete()
    {
        Debug.Log("Puzzle Complete!");
        // TODO: Put what is supposed to happen when you get it right into this method. 
    }
    
}
