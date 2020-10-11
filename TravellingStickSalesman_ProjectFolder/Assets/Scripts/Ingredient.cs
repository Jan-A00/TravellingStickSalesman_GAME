using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Ingredient_Type
{
    GREENGEL = 0, //important to start from 0
    HERB = 1, 
    MUSHROOM = 2, 
    PINKGEL = 3, 
    ROSEMARY = 4
}

public class Ingredient : MonoBehaviour
{
    public Ingredient_Type iType; //which ingredient is this?
    public Sprite[] ingredientImage;
    public PotionPuzzleController controller;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = ingredientImage[(int)iType];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        //Debug.Log("Clicked " + iType);
        AddIngredientToRecipe();

    }

    void AddIngredientToRecipe()
    {
        controller.AddIngredient(iType);
    }

    
}
