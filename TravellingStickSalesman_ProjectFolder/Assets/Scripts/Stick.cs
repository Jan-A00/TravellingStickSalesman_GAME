using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Character
{
     None = 0, //important to start from 0
     Genevieve = 1,
     Baz = 2, 
     Kaede = 3, 
     Quercus = 4, 
     Beardfacé = 6,
     BasilAndSybil = 7
}

[CreateAssetMenu(fileName = "New Stick", menuName = "Stick")]
public class Stick : ScriptableObject
{
     public string name;
     public string description;
     public Sprite stickArt;
     public Sprite selectedStickArt;
     public Sprite highlightedStickArt;
     public Character character;
}
