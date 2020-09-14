using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Ingredient_Type
{
    ORANGE = 0, //important to start from 0
    PEACH = 1, 
    WATERMELON = 2, 
    CHERRY = 3, 
    PEAR = 4
}

public class Ingredient : MonoBehaviour
{
    public Ingredient_Type iType; //which ingredient is this?
    public Sprite[] ingredientImage;
    public Recipe recipe;
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
        recipe.AddIngredient(iType);
    }

    
}
