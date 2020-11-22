using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionPuzzleController : MonoBehaviour
{
    public Text txtCurrentIngredients;
    public Text txtCorrectIngredients;
    public BoxCollider2D cauldronCol;
    public Button[] button;
    public Ingredient_Type[] allPossibleIngredientTypes;
    public Ingredient_Type[] correctIngredients;
    public List<Ingredient_Type> currentIngredients;
    public bool orderMatters = false;
    public bool allowDuplicates = false;
    public bool randomRecipe = false;
    public int randomRecipeMinIngredients = 1;
    public int randomRecipeMaxIngredients = 5;
    public bool winCon = true;
    
    [Header("Feedback")]
    public Text feedbackTextDisplay;
    public AudioSource[] feedbackLines;
    public string emptyText;
    public string incorrectText;
    public string notEnoughText;
    public string tooManyText;
    public float typingSpeed;
    public GameObject feedbackContinueButton;
    public GameObject feedbackDialogueBox;

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
        CheckAndDoStuff();
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
        yield return new WaitForSeconds(0.01f);
        feedbackContinueButton.SetActive(true);
        foreach(char letter in incorrectText.ToCharArray())
        {
            feedbackTextDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
    
    IEnumerator EmptyCauldron()
    {
        yield return new WaitForSeconds(0.01f);
        feedbackContinueButton.SetActive(true);
        foreach(char letter in emptyText.ToCharArray())
        {
            feedbackTextDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    IEnumerator NotEnough()
    {
        yield return new WaitForSeconds(0.01f);
        feedbackContinueButton.SetActive(true);
        foreach(char letter in notEnoughText.ToCharArray())
        {
            feedbackTextDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    IEnumerator TooMany()
    {
        yield return new WaitForSeconds(0.01f);
        feedbackContinueButton.SetActive(true);
        foreach(char letter in tooManyText.ToCharArray())
        {
            feedbackTextDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void Dismiss()
    {
        cauldronCol.enabled = true;
        feedbackTextDisplay.text = "";
        feedbackDialogueBox.SetActive(false);
        feedbackContinueButton.SetActive(false);
        for (int i = 0; i < 5; i++)
        {
            button[i].enabled = true;
        }
        feedbackLines[0].Stop();
        feedbackLines[1].Stop();
        feedbackLines[2].Stop();
        feedbackLines[3].Stop();
        StopAllCoroutines();
    }
    
    public bool NoIngredients() { if ( currentIngredients.Count == 0 ) { Debug.Log("No Ingredients"); return true; } else { return false; } }
    public bool NotEnoughIngredients() { if ( currentIngredients.Count < correctIngredients.Length ) { Debug.Log("Not Enough"); return true; } else { return false; } }
    public bool EnoughIngredients() { if (currentIngredients.Count == correctIngredients.Length) { Debug.Log("Enough Ingredients"); return true; } else { return false; } }
    public bool TooManyIngredients() { if (currentIngredients.Count > correctIngredients.Length) { Debug.Log("Too Many Ingredients"); return true; } else { return false; } }

    public bool CheckAndDoStuff()
    {
        // ▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽
        // Check for incorrect conditions first.
        // ▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽
        if (NoIngredients()) 
        {
            /* Calls and Coroutines can go here. */
            feedbackLines[2].Play();
            cauldronCol.enabled = false;
            feedbackDialogueBox.SetActive(true);
            for (int i = 0; i < 5; i++)
            {
                button[i].enabled = false;
            }
            StartCoroutine(EmptyCauldron());
            return false;
        }
        if (NotEnoughIngredients()) 
        {
            /* Calls and Coroutines can go here. */
            feedbackLines[3].Play();
            cauldronCol.enabled = false;
            feedbackDialogueBox.SetActive(true);
            for (int i = 0; i < 5; i++)
            {
                button[i].enabled = false;
            }
            StartCoroutine(NotEnough());
            return false;
        }
        if (TooManyIngredients()) 
        {
            /* Calls and Coroutines can go here. */
            feedbackLines[0].Play();
            cauldronCol.enabled = false;
            feedbackDialogueBox.SetActive(true);
            for (int i = 0; i < 5; i++)
            {
                button[i].enabled = false;
            }
            StartCoroutine(TooMany());
            return false;
        }
        // △△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△

        // ▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽
        // Now check for correct conditions.
        // ▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽▽
        if (EnoughIngredients()) 
        {
            /* Calls and Coroutines can go here. */

            if (CheckRecipe()) {
                // We have the correct ingedients!
                
                /* Calls and Coroutines can go here. */
                //cauldronCol.enabled = false;
                winCon = true;
                Debug.Log("WIN!!");
                GameObject.FindGameObjectWithTag("Inventory-OpenButton").GetComponent<Button>().interactable = true;
                return true;
            }
            else {
                /* We have the correct number of ingredients, 
                but they are the wrong kind. */

                /* Calls and Coroutines can go here. */
                feedbackLines[1].Play();
                cauldronCol.enabled = false;
                feedbackDialogueBox.SetActive(true);
                for (int i = 0; i < 5; i++)
                {
                    button[i].enabled = false;
                }
                StartCoroutine(WrongIngredients());      
                return false;
            }
        }  
        // △△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△
        // If all else false, we just return false.
        return false;
        
    }

    //For each thing in the current ingredients, check if it matches in the
    //correct ingredients. Once you get n matches, where n is the length of the
    //correct ingredients list, you have a correct recipe, as long as you 
    //have no more ingredients left (i.e. extra ingredients are bad)

    public bool CheckRecipe()
    {
        /*
        This function, when called, returns one of the following:
            true  <- if they have matched the ingredients.
            false <- if they have not yet matched the correct ingredients.
        */
        int matches = 0;
        List<int> alreadyMatchedPlaces = new List<int>();

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
        /* This performs the final comparison between the 
        number of correct matches and the total number of 
        ingredients they must match and returns true or false. */
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
