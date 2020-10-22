using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public Image image;
    public Ingredient_Type iType; //which ingredient is this?
    public Sprite[] ingredientImage;
    public PotionPuzzleController controller;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        image.sprite = ingredientImage[(int)iType];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddIngredientToRecipe()
    {
        controller.AddIngredient(iType);
    }

    
}
