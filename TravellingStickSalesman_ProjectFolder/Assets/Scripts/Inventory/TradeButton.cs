using System;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Inventory
{
    public class TradeButton : Button
    {
        private bool InventoryButtonSelected()
        {
            return gameObject.transform.parent.GetComponent<InventoryUserInterfaceManager>().InventoryButtons.Any(iB => iB.Selected);
        }

        public void TradeStick()
        {
            Debug.Log("Trading Stick...");
            gameObject.transform.parent.GetComponent<InventoryUserInterfaceManager>().TradeSelectedStick();
        }

        private void Start()
        {
            if (gameObject.scene.name == "Tutorial") return;
            onClick.AddListener(TradeStick);
        }
        
        private void LateUpdate()
        {
            if (gameObject.transform.parent.GetComponent<InventoryUserInterfaceManager>().AllowTrading())
            {
                gameObject.GetComponent<Button>().interactable = InventoryButtonSelected();
            }
        }
    }
}