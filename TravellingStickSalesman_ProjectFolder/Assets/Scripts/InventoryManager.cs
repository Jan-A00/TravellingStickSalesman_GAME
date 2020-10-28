using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public Transform inventoryUIRoot;
    public GameObject invUI;
    public Stick[] allSticks;
    //public int[] allSticksID;
    public InventoryStick stickPrefab;
    [SerializeField]
    private InventoryStick[] inventory;
    private int activeStickIndex = 0;
    public Stick activeStick;
    public int[] startingSticks;
    public Button tradeButton;
    public bool hasTraded = false;

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
        if (activeStick.stickValue == StickValue.PlusStick || activeStick.stickValue == StickValue.MinusStick || hasTraded == true)
        {
            //you cant trade with them.
            tradeButton.interactable = false;
        }
        else
        {
            tradeButton.interactable = true;
        }
    }

    public void SellStick()
    {
        if (activeStick != null)
        {
            Debug.Log("You have sold " + activeStick + " at index " + activeStickIndex);
            //Get ID of Plus or Minus stick for this character.
            
            int tradeStickID = GetIDOfResultantTradedStick(StickGameManager.Instance.GetTrader(), activeStick);
            InventoryStick newStick = Instantiate(stickPrefab);
            //Destroy old stick
            Debug.Log("Destroying " + inventory[activeStickIndex]);
            Destroy(inventory[activeStickIndex].gameObject);
            newStick.Init(activeStickIndex, this, allSticks[tradeStickID]);
            newStick.transform.SetParent(inventoryUIRoot);
            newStick.transform.SetSiblingIndex(activeStickIndex);
            hasTraded = true;
            tradeButton.interactable = false;

            //invUI.SetActive(false); //the idea is to get the ui to disappear and for the dialogue to start
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
        int plusStickID;
        int minusStickID;
        // what am i doing pls help 

        foreach (Stick ss in allSticks)
        {
            if(s.character == c)
            {
                if (s.stickValue == StickValue.TrueStick)
                {
                    Debug.Log("true");
                }
                else if (s.stickValue == StickValue.PlusStick)
                {
                    Debug.Log("plus");
                }
                else if (s.stickValue == StickValue.MinusStick)
                {
                    Debug.Log("minus");
                }
            }
        }

        //Step 1: Go through all sticks in allSticks: ✔✔
        //  if stick has character == c,
        //      if stick.stickValue == plusstick, set plusStick to that stick
        //      if stick.stickValue == minusstick, set minusStick to that stick
        //      if stick.stickValue == truestick, set trueStick to that stick
        //Step 2:
        //  if stick is trueStick,
        //      return the id of plusStick;
        //  else
        //      return the id of minusStick;
        
        return 9;
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
