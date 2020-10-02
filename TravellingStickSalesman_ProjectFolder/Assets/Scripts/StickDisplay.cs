using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StickDisplay : MonoBehaviour
{
    public Stick stick;
    public Image stickArtImage;

    void Start()
    {
        stickArtImage.sprite = stick.stickArt;
    }
}
