using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using DataManagement;
using DataManagement.ConfigTypes;
using DataManagement.StateTypes;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

namespace Inventory
{
    public class InventoryUserInterfaceManager : MonoBehaviour
    {
        private Transform InventoryUIRoot => gameObject.transform.Find("Stick Inventory").GetComponent<Transform>();
        public bool Visible => gameObject.activeSelf;
        private string activeStick;
        private bool tradeButtonActive;
        private GameObject inventoryOpenButton;
        public GameObject buttonPrefab;

        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private List<InventoryButton> inventoryButtons = new List<InventoryButton>();
        // ReSharper disable once ConvertToAutoProperty
        public IEnumerable<InventoryButton> InventoryButtons => inventoryButtons;

        public void ShowInventoryUserInterface()
        {
            // Hold a reference to the button that opens the inventory,
            // otherwise it wont be available when we want to enable it again later.
            inventoryOpenButton = GameObject.FindWithTag("Inventory-OpenButton");
            // Disable the Open Inventory button.
            inventoryOpenButton.SetActive(false);
            // Enable the gameObject that this is attached to, the inventory/trading user interface.
            gameObject.SetActive(true);
            // Build the current Inventory Buttons.
            BuildInventoryIcons();
        }
        
        public void HideInventoryScreen()
        {
            // Enable the Open Inventory button.
            inventoryOpenButton.SetActive(true);
            // Purge the old inventory icons so they arent here next time.
            ClearInventoryIcons();
            // Disable the inventory trading interface.
            gameObject.SetActive(false);
        }

        private void ClearInventoryIcons()
        {
            List<GameObject> children = (from Transform child in InventoryUIRoot select child.gameObject).ToList();
            children.ForEach(Destroy);
        }
        
        private void BuildInventoryIcons()
        {
            // Clear out any buttons that we used last time...
            inventoryButtons.Clear();
            // Copy the stick configs to a new array each time we build the icons to prevent re-using old state...
            StickConfig[] stickConfigs = (StickConfig[]) GameStateManager.Instance.CurrentInventoryStickConfigs().ToArray().Clone();
            
            for (int index = 0; index < stickConfigs.Length; index++)
            {
                StickConfig stickConfig = stickConfigs[index]; 
                string stickName = stickConfig.name;
                
                // ReSharper disable once UseObjectOrCollectionInitializer
                SpriteState spriteState = new SpriteState();
                spriteState.pressedSprite = stickConfig.spriteForHighlighted;
                spriteState.highlightedSprite = stickConfig.spriteForHighlighted;
                spriteState.selectedSprite = stickConfig.spriteForHighlighted;
                
                GameObject newButton = Instantiate(buttonPrefab, InventoryUIRoot, true);
                newButton.GetComponentInChildren<Text>().text = stickName;
                newButton.transform.SetSiblingIndex(index);
                newButton.name = stickName;
                
                // RectTransform rectTransform = newButton.GetComponent<RectTransform>();
                newButton.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                Vector3 position = newButton.transform.localPosition;
                position = new Vector3(position.x, position.y, -1000.0f);
                newButton.transform.localPosition = position;
                
                InventoryButton newInventoryButton = newButton.GetComponent<InventoryButton>();
                newInventoryButton.image.sprite = stickConfig.spriteForNormal;
                newInventoryButton.inventoryUserInterfaceManager = this;
                newInventoryButton.spriteState = spriteState;
                newInventoryButton.stickName = stickName;
                newInventoryButton.onClick.AddListener( () => { UpdateActiveInventoryButton(stickName); } );
                inventoryButtons.Add(newInventoryButton);
            }
        }

        private void UpdateTradeButtonState(bool tradeButtonState)
        {
            Debug.Log($"Marking the trade button as {(tradeButtonState ? "enabled" : "disabled")}");
            gameObject.transform.Find("Trade Button").GetComponent<Button>().interactable = tradeButtonState;
        }
        
        private void UpdateActiveInventoryButton(string stickName)
        {
            PerformInventoryUserInterfaceButtonUpdate();
            Debug.Log($"Clicked on the \'{stickName}\' button...");
            activeStick = activeStick != stickName ? stickName : null;
            UpdateTradeButtonState(activeStick != null);
            ;
        }

        private void PerformInventoryUserInterfaceButtonUpdate()
        {
            foreach (InventoryButton inventoryButton in inventoryButtons)
            {
                Debug.Log(inventoryButton.Selected);
            }
        }

        public bool AllowTrading()
        {
            return !GameStateManager.Instance.AlreadyTradedWithThisTrader();
        }
        
        public void TradeSelectedStick()
        {
            ;
            if (inventoryButtons.Where(ib => ib.Selected).Select(ib => ib.stickName).Count() != 1) return;
            {
                Debug.Log("Getting the selected stick's name...");
                string selectedStickName = inventoryButtons.Where(ib => ib.Selected).Select(ib => ib.stickName).First();
                Debug.Log("Performing the trade...");
                GameStateManager.Instance.GiveStickToTrader(selectedStickName);
                GameStateManager.Instance.MarkCurrentTraderAsDone();
                HideInventoryScreen();
            }
        }
    }
}