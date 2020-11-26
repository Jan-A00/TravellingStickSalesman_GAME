using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Inventory
{
    public class InventoryButton : Button
    {
        public string stickName;
        public InventoryUserInterfaceManager inventoryUserInterfaceManager;
        public bool Disabled => currentSelectionState == SelectionState.Disabled;
        public bool Pressed => currentSelectionState == SelectionState.Pressed;
        public bool Selected => currentSelectionState == SelectionState.Selected;
        public bool Highlighted => currentSelectionState == SelectionState.Highlighted;
        public bool Normal => currentSelectionState == SelectionState.Normal;
        
        public override void OnDeselect(BaseEventData eventData)
        {
            base.OnDeselect(eventData);
            if (eventData.GetType() != typeof(PointerEventData)) return;
            GameObject selectedObject = eventData.selectedObject;
            GameObject clickedObject = ((PointerEventData) eventData).pointerCurrentRaycast.gameObject;
            TradeButton tradeButton = clickedObject.transform.parent.GetComponent<TradeButton>();
            if (!tradeButton) return;
            InventoryUserInterfaceManager inventoryManager = selectedObject.transform.parent.parent
                .GetComponent<InventoryUserInterfaceManager>();
            if (!inventoryManager) return;
            inventoryManager.TradeStick(selectedObject.GetComponent<InventoryButton>());
        }
    }
}
