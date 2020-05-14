using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddClams : MonoBehaviour
{
    [SerializeField]
    private Transform puzzleField;

    [SerializeField]
    private GameObject clam;

    void Awake()
    {
        for(int i = 0; i < 8; i++)
        {
            GameObject c = Instantiate(clam);
            c.name = "" + i;
            c.transform.SetParent(puzzleField, false);
        }
    }
}
