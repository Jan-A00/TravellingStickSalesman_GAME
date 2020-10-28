using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

public enum StickValue
{
     TrueStick = 0,
     PlusStick = 1,
     MinusStick = 2
}

[CreateAssetMenu(fileName = "New Stick", menuName = "Stick")]
public class Stick : ScriptableObject
{
     public string name;
     public string description;
     public Character character;
     public StickValue stickValue;
     public Sprite stickArt;
     public SpriteState stickSpriteState;
     
}
