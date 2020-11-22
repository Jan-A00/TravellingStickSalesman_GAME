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
            // EventSystem.current.currentSelectedGameObject.
            // eventData.selectedObject.gameObject.unit
            // if (ev)
            // base.OnDeselect(eventData);
            ;
        }
    }
}
