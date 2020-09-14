using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddClams : MonoBehaviour
{
    [SerializeField]
    private Transform puzzleField;

    [SerializeField]
    private GameObject clam;

    public SeaPuzzleInstructions instructions;

    private bool startPuzzle;

    void Update()
    {
        if(/*instructions.hasPlayerSeenInstructions == true &&*/ !startPuzzle)
        {
            StartPuzzle();
            startPuzzle = true;
        }
    }

    void StartPuzzle()
    {
        for(int i = 0; i < 8; i++)
        {
            GameObject c = Instantiate(clam);
            c.name = "" + i;
            c.transform.SetParent(puzzleField, false);
        }
    }
}
