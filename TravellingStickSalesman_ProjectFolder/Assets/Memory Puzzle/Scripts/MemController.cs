﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemController : MonoBehaviour
{
    [SerializeField]
    private Sprite closedClamImage;

    [SerializeField]
    private Sprite emptyClamImage;

    public List<Sprite> pearls = new List<Sprite>();

    public List<Sprite> clamWithPearls = new List<Sprite>();
    
    public List<Button> closedClam = new List<Button>();

    private bool firstGuess, secondGuess;

    private int countGuesses;
    private int countCorrectGuesses;
    private int gameGuesses;

    private int firstGuessIndex, secondGuessIndex;

    private string firstGuessPuzzle, secondGuessPuzzle;

    void Start()
    {
        GetClosedClam();
        AddListeners();
        AddPearls();
        Shuffle(clamWithPearls);
        gameGuesses = clamWithPearls.Count / 2;
    }
    
    void GetClosedClam()
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag ("PuzzleClam");

            for(int i = 0; i < objects.Length; i++)
            {
                closedClam.Add(objects[i].GetComponent<Button>());
                closedClam[i].image.sprite = closedClamImage;
            }
        }

    void AddPearls()
    {
        int looper = closedClam.Count;
        int index = 0;

        for (int i = 0; i < looper; i++)
        {
            if (index == looper / 2)
            {
                index = 0;
            }

            clamWithPearls.Add(pearls[index]);
            index++;
        }
    }

    void AddListeners()
    {
        foreach (Button btn in closedClam)
        {
            btn.onClick.AddListener(() => OpenClam());
        }
    }
    
    public void OpenClam()
    {
        string name = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;

        if (!firstGuess)
        {
            firstGuess = true;
            firstGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            firstGuessPuzzle = clamWithPearls[firstGuessIndex].name;
            closedClam[firstGuessIndex].image.sprite = clamWithPearls[firstGuessIndex];
        }

        else if (!secondGuess)
        {
            secondGuess = true;
            secondGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            if (secondGuessIndex != firstGuessIndex)
            {
                secondGuessPuzzle = clamWithPearls[secondGuessIndex].name;
                closedClam[secondGuessIndex].image.sprite = clamWithPearls[secondGuessIndex];
                countGuesses++;
                StartCoroutine(CheckIfThePuzzlesMatch());
            }
            else
            {
                secondGuess = false;
                //Debug.Log("Illegal move.");
            }
        }
    }

    IEnumerator CheckIfThePuzzlesMatch()
    {
        yield return new WaitForSeconds (0.5f);

        if (firstGuessPuzzle == secondGuessPuzzle)
        {
            yield return new WaitForSeconds (0.5f);

            closedClam[firstGuessIndex].interactable = false;
            closedClam[secondGuessIndex].interactable = false;

            closedClam[firstGuessIndex].image.sprite = emptyClamImage;
            closedClam[secondGuessIndex].image.sprite = emptyClamImage;

            //Debug.Log("Correct Guess");

            CheckIfTheGameIsFinished();
        }

        else
        {
            yield return new WaitForSeconds (0.5f);

            closedClam[firstGuessIndex].image.sprite = closedClamImage;
            closedClam[secondGuessIndex].image.sprite = closedClamImage;
            //Debug.Log("Wrong Guess");
        }

        yield return new WaitForSeconds (0.2f);
        firstGuess = secondGuess = false;
    }

    void CheckIfTheGameIsFinished()
    {
        countCorrectGuesses++;

        if(countCorrectGuesses == gameGuesses)
        {
            Debug.Log("Game Finished in " + countGuesses + " moves.");
        }
    }

    void Shuffle(List<Sprite> list)
    {
        for(int i = 0; i < list.Count; i++)
        {
            Sprite temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
