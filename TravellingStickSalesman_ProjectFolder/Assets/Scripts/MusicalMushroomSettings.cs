using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class MusicalMushroomSettings : MonoBehaviour
{
    public float highightOnClickDuration;
    public AudioClip[] mushroomAudioClips;
    public Sprite[] mushroomSprites;
    public Sprite[] highlightedMushroomSprites;
    public Sprite[] wallRunes;
    public Sprite[] highlightedWallRunes;
    public MusicalWallRune[] runeSequence;
    public readonly Notes[] noteArray = new Notes[8]
    {
        Notes.C1,
        Notes.D,
        Notes.E,
        Notes.F,
        Notes.G,
        Notes.A,
        Notes.B,
        Notes.C2
    };
       
    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private int IndexOfNote(Notes mushroomNote)
    {
        return Array.IndexOf(noteArray, mushroomNote);
    }

    private AudioClip AudioClipRetriever(AudioClip[] targetArrayVariable, Notes mushroomNote)
    {
        return targetArrayVariable[IndexOfNote(mushroomNote)];
    }
    private Sprite SpriteRetriever(Sprite[] targetArrayVariable, Notes mushroomNote)
    {
        return targetArrayVariable[IndexOfNote(mushroomNote)];
    }

    public AudioClip GetMushroomAudio(Notes mushroomNote)
    {
        return AudioClipRetriever(mushroomAudioClips, mushroomNote);
    }

    public Sprite GetMushroomSprite(Notes mushroomNote)
    {
        return SpriteRetriever(mushroomSprites, mushroomNote);
    }

    public Sprite GetMushroomHighlightSprite(Notes mushroomNote)
    {
        return SpriteRetriever(highlightedMushroomSprites, mushroomNote);
    }

    public Sprite GetNormalWallRune(Notes mushroomNote)
    {
        return SpriteRetriever(wallRunes, mushroomNote);
    }

    public Sprite GetHighlightedWallRune(Notes mushroomNote)
    {
        return SpriteRetriever(highlightedWallRunes, mushroomNote);
    }   

}
