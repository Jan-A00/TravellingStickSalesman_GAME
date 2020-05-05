using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotPuzzleController : MonoBehaviour
{
    public Transform[] images;
    public GameObject winText;
    public bool winCon;

    void Start()
    {
        winCon = false;
        winText.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (images[0].rotation.z == 0 &&
            images[1].rotation.z == 0 &&
            images[2].rotation.z == 0 &&
            images[3].rotation.z == 0 &&
            images[4].rotation.z == 0 &&
            images[5].rotation.z == 0 &&
            images[6].rotation.z == 0 &&
            images[7].rotation.z == 0 &&
            images[8].rotation.z == 0)
        {
            winCon = true;
            Debug.Log("You Win!");
        }
        
    }
}
