using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public Transform inventoryUIRoot;
    public GameObject invUI;
    public Stick[] allSticks;
    public InventoryStick stickPrefab;
    [SerializeField]
    private InventoryStick[] inventory;
    private int activeStickIndex = 0;
    public Stick activeStick;
    public int[] startingSticks;
    public Button tradeButton;

    void Start()
    {
        CreateInventory();
    }

    void Update()
    {
        
    }

    void CreateInventory()
    {
        int index = 0;
        inventory = new InventoryStick[startingSticks.Length];
        foreach (int id in startingSticks)
        {
            InventoryStick newStick = Instantiate(stickPrefab);
            inventory[index] = newStick;
            newStick.Init(index, this, allSticks[id]);
            newStick.transform.SetParent(inventoryUIRoot);
            newStick.transform.SetSiblingIndex(index);
            index++;
        }
    }

    public void SetActiveStick(int index, Stick stick)
    {
        //Note: unity handles highlighting thanks to Button component (thanks button!)
        //unhilight old stick
        
        //highlight new stick
        Debug.Log("Stick in slot " + index + " is now active (" + stick.name + ": " + stick.description + ")");
        //set active index
        activeStickIndex = index;
        activeStick = stick;
        ManipulateTradeButton();
    }

    public void ManipulateTradeButton()
    {
        Character c = StickGameManager.Instance.GetTrader();
        if (activeStick == null)
        {    
            tradeButton.interactable = false;
        }
        else if (c == Character.None)
        {    
            tradeButton.interactable = false;
        }
        else if (activeStick.character == c)
        {
            if (activeStick.stickValue == StickValue.PlusStick ||
                activeStick.stickValue == StickValue.MinusStick)
                {
                    //you cant trade with them.
                    tradeButton.interactable = false;
                }
        }
        else
            tradeButton.interactable = true;
    }

    public void SellStick()
    {
        if (activeStick != null)
        {
            Debug.Log("You have sold " + activeStick + " at index " + activeStickIndex);
            //invUI.SetActive(false); //the idea is to get the ui to disappear and for the dialogue to start
            //Get ID of Plus or Minus stick for this character.
            
            int tradeStickID = GetIDOfResultantTradedStick(StickGameManager.Instance.GetTrader(), activeStick);
            InventoryStick newStick = Instantiate(stickPrefab);
            //Destroy old stick
            Debug.Log("Destroying " + inventory[activeStickIndex]);
            Destroy(inventory[activeStickIndex].gameObject);
            newStick.Init(activeStickIndex, this, allSticks[tradeStickID]);
            newStick.transform.SetParent(inventoryUIRoot);
            newStick.transform.SetSiblingIndex(activeStickIndex);
            
        }
        else
        {
            Debug.LogError("The active stick was null");
        }
    }

    public int GetIDOfResultantTradedStick(Character c, Stick s)
    {
        Debug.Log("Looking for a trade of " + s.name + " with " + c);
        Stick plusStick;
        Stick minusStick;
        Stick trueStick;
        //Step 1: Go through all sticks in allSticks:
        //  if stick has character == c,
        //      if stick.stickValue == plusstick, set plusStick to that stick
        //      if stick.stickValue == minusstick, set minusStick to that stick
        //      if stick.stickValue == truestick, set trueStick to that stick
        //Step 2:
        //  if stick is trueStick,
        //      return the id of plusStick;
        //  else
        //      return the id of minusStick;
        
         
        
        return 3;
    }

    public Stick GetActiveStick()
    {
        if (activeStick == null)
        {
            Debug.LogError("The active stick was null");
        }
        return activeStick;
    }

    public void OnEnable()
    {
        ManipulateTradeButton();
    }
}
