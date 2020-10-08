using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionPuzzleRecipe : MonoBehaviour
{
    public Text txtCurrentIngredients;
    public Text txtCorrectIngredients;
    public Text feedbackTextDisplay;
    public string incorrectText;
    public string emptyText;
    public float typingSpeed;
    public GameObject feedbackContinueButton;
    public GameObject dialogueBox;
    public BoxCollider2D cauldronCol;
    public Ingredient_Type[] allPossibleIngredientTypes;
    public Ingredient_Type[] correctIngredients;
    public List<Ingredient_Type> currentIngredients;
    public bool orderMatters = false;
    public bool allowDuplicates = false;
    public bool randomRecipe = false;
    public int randomRecipeMinIngredients = 1;
    public int randomRecipeMaxIngredients = 5;
    // Start is called before the first frame update
    void Awake()
    {
        currentIngredients = new List<Ingredient_Type>();
    }
    void Start()
    {
        if (randomRecipe)
        {
            if (!allowDuplicates)
            { correctIngredients = new Ingredient_Type[Random.Range(randomRecipeMinIngredients, randomRecipeMaxIngredients+1)];
                if (allPossibleIngredientTypes.Length < correctIngredients.Length)
                {
                    Debug.LogError("You don't have enough ingredient types");
                    return;
                }
            }

            List<int> blacklistedIngredients = new List<int>();
            for (int i = 0; i < correctIngredients.Length; i++)
            {
                bool added = false;
                while (!added)
                {
                    int randnum = Random.Range(0, 5); //between 'orange and pear'
                    if (allowDuplicates)
                    {
                        correctIngredients[i] = allPossibleIngredientTypes[randnum];    
                        added = true;
                    }
                    else if (!blacklistedIngredients.Contains( randnum ) )
                    {
                        correctIngredients[i] = allPossibleIngredientTypes[randnum];
                        blacklistedIngredients.Add(randnum);
                        added = true;
                    }
                }
            }
        }
        txtCorrectIngredients.text = IngredientsToString(correctIngredients);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        CheckRecipe();
        currentIngredients.Clear();
        txtCurrentIngredients.text = IngredientsToString( currentIngredients.ToArray() );
    }

    public void AddIngredient(Ingredient_Type ingredienttype)
    {
        currentIngredients.Add(ingredienttype);
        txtCurrentIngredients.text = IngredientsToString( currentIngredients.ToArray() );
    }

    //For each thing in the current ingredients, check if it matches in the
    //correct ingredients. Once you get n matches, where n is the length of the
    //correct ingredients list, you have a correct recipe, as long as you 
    //have no more ingredients left (i.e. extra ingredients are bad)
    
    IEnumerator WrongIngredients()
    {
        foreach(char letter in incorrectText.ToCharArray())
        {
            feedbackTextDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
    
    public void Dismiss()
    {
        feedbackTextDisplay.text = "";
        cauldronCol.enabled = true;
        dialogueBox.SetActive(false);
    }
    
    public bool CheckRecipe()
    {
        int matches = 0;
        List<int> alreadyMatchedPlaces = new List<int>();

        if (correctIngredients.Length != currentIngredients.Count && currentIngredients.Count != 0)
        {
            //haha try again m8
            cauldronCol.enabled = false;
            dialogueBox.SetActive(true);
            Debug.Log("meh");
            StartCoroutine(WrongIngredients());
        }

        if (correctIngredients.Length == currentIngredients.Count )
        {
            //win condition basically. main dialogue should resume after this
            Debug.Log("yay");
        }

        if (currentIngredients.Count == 0)
        {
            //cauldron is empty!
            Debug.Log("empty");
        }

        for (int place = 0; place < currentIngredients.Count; place++)
        {
            Ingredient_Type iplace = currentIngredients[place];
            if (orderMatters)
            {
                if (correctIngredients.Length > place && correctIngredients[place] == iplace)
                    matches += 1;
            }
            else
            {
                for (int correctPlace = 0; correctPlace < correctIngredients.Length; correctPlace++)
                {
                    if ( iplace == correctIngredients[correctPlace] 
                        && !alreadyMatchedPlaces.Contains(correctPlace) )
                    {
                        //Add this slot to the already matched slots
                        alreadyMatchedPlaces.Add(correctPlace);
                        matches += 1;
                    }
                }
            }
        }
        return (matches == correctIngredients.Length);
    }

    public string IngredientsToString(Ingredient_Type[] ings)
    {
        string ingredient_str = "";
        foreach (Ingredient_Type ing in ings)
        {
            ingredient_str += ing.ToString() + ", ";
        }
        return ingredient_str;
    }
}
