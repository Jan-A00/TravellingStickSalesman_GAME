using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinePuzzleClicker : MonoBehaviour
{
    public ShrinePuzzleController controller;

    void Start()
    {
        controller.winCon = false;
        transform.Rotate(0.0f, 0.0f, Random.Range(0, 2) * 90);
    }
    
    private void OnMouseDown()
    {
        if (controller.winCon == false)
        {
            transform.Rotate(0f, 0f, 90f);
        }
    }
}
