using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public bool isStickEquipped;
    public Button placeholderStick;
    public Button backButton;
    public GameObject equipText;

    void Start()
    {
        isStickEquipped = false;
    }

    public void EquipStick()
    {
        //backButton.interactable = true;
        
        if(isStickEquipped == false)
        {
            isStickEquipped = true;
            equipText.SetActive(true);
            //Debug.Log("Equipped Stick");
        }
        else
        {
            isStickEquipped = false;
            equipText.SetActive(false);
            //Debug.Log("Unequipped Stick");
        }
    }
}
