using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryStick : MonoBehaviour
{
    Stick stick;
    InventoryManager manager;
    public Image stickArtImage;
    int index;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(int id, InventoryManager iManager, Stick stickData)
    {
        index = id;
        manager = iManager;
        stick = stickData;
        stickArtImage.sprite = stick.stickArt;
    }

    public void Click()
    {
        manager.SetActiveStick(this.index, stick);
        //perform sprite swap for active slot.
    }
}
