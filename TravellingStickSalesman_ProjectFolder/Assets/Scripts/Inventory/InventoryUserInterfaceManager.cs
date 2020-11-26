using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using DataManagement;
using DataManagement.ConfigTypes;
using Button = UnityEngine.UI.Button;

namespace Inventory
{
    public class InventoryUserInterfaceManager : MonoBehaviour
    {
        private Transform InventoryUIRoot => gameObject.transform.Find("Stick Inventory").GetComponent<Transform>();

        private Transform NormalUIRoot => GameObject.FindGameObjectWithTag("Normal-UserInterface").GetComponent<Transform>();
        private GameObject inventoryOpenButton;
        public GameObject buttonPrefab;
        public GameObject newStickPopup;

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
            inventoryOpenButton.GetComponent<Button>().interactable = false;
            // Enable the gameObject that this is attached to, the inventory/trading user interface.
            gameObject.SetActive(true);
            // Build the current Inventory Buttons.
            BuildInventoryIcons();
        }
        
        public void HideInventoryScreen()
        {
            // Enable the Open Inventory button.
            inventoryOpenButton.GetComponent<Button>().interactable = true;
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
                //spriteState.pressedSprite = stickConfig.spriteForHighlighted;
                //spriteState.highlightedSprite = stickConfig.spriteForHighlighted;
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
                // newInventoryButton.onClick.AddListener( () => { UpdateActiveInventoryButton(stickName); } );
                inventoryButtons.Add(newInventoryButton);
            }
        }

        private void UpdateTradeButtonState()
        {
            if (inventoryButtons.Any(button => button.Selected))
            {
                gameObject.transform.Find("Trade Button").GetComponent<TradeButton>().interactable = true;    
            }
            else
            {
                gameObject.transform.Find("Trade Button").GetComponent<TradeButton>().interactable = false;
            }
        }

        public bool AllowTrading()
        {
            if (gameObject.scene.name == "Tutorial") return false;
            return !GameStateManager.Instance.AlreadyTradedWithThisTrader();
        }

        private void ShowNewStickPopup(Sprite selectedStickIcon)
        {
            // GameObject newStickPopup = Instantiate(newStickPopupPrefab, NormalUIRoot, true);
            newStickPopup.transform.Find("Stick Icon").GetComponent<SpriteRenderer>().sprite = selectedStickIcon;
            newStickPopup.SetActive(true);
        }

        public void TradeStick(InventoryButton inventoryButton)
        {
            if (!AllowTrading()) { Debug.Log("Unable to trade sticks at this time."); return; }
            string selectedStickName = inventoryButton.stickName;
            Sprite selectedStickIcon = GameStateManager.Instance.GetSpriteForNewStickPopup(selectedStickName);
            GameStateManager.Instance.GiveStickToTrader(selectedStickName);
            GameStateManager.Instance.MarkCurrentTraderAsDone();
            HideInventoryScreen();
            ShowNewStickPopup(selectedStickIcon);
        }

        public void LateUpdate()
        {
            if (AllowTrading()) UpdateTradeButtonState();
        }
    }
}