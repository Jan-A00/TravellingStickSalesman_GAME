using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public Transform inventoryUIRoot;
    public Stick[] allSticks;
    public InventoryStick stickPrefab;
    [SerializeField]
    private InventoryStick[] inventory;
    private int activeStickIndex = 0;
    public Stick activeStick;
    public int[] startingSticks;

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
        foreach (int id in startingSticks)
        {
            InventoryStick newStick = Instantiate(stickPrefab);
            newStick.Init(index, this, allSticks[id]);
            newStick.transform.SetParent(inventoryUIRoot);
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
    }

    public void SellStick()
    {
        if (activeStick != null)
        {
            Debug.Log("You have sold " + activeStick);
        }
        else
        {
            Debug.LogError("The active stick was null");
        }
    }

    /*public Stick GetActiveStick()
    {
        if (activeStick == null)
        {
            Debug.LogError("The active stick was null");
        }
        return activeStick;
    }*/
}
