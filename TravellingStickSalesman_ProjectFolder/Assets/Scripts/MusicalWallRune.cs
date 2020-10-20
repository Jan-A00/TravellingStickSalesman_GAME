using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MusicalWallRune : MonoBehaviour
{
    public MusicalMushroomMatcher matcher;
    public MusicalMushroomSettings settings;
    public Notes note; //which note is this?
    private Sprite normalSprite;
    private Sprite highlightSprite;

    void Awake()
    {
        
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetNote(Notes newNote)
    {
        note = newNote;
        normalSprite = settings.GetNormalWallRune(newNote);
        highlightSprite = settings.GetHighlightedWallRune(newNote);
        GetComponent<SpriteRenderer>().sprite = normalSprite;
        Debug.Log("Setting sprite for rune: " + this + " to " + normalSprite);
    }

    public void Hide()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }
    
    public void SetSpriteBackToNormal()
    {
        GetComponent<SpriteRenderer>().sprite = normalSprite;
    }

    public void Highlight()
    {
        Debug.Log("Highlighting Wall Rune: "+ note);
        GetComponent<SpriteRenderer>().sprite = highlightSprite;
    }
   
}
