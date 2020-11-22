using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class InventoryOverlay : MonoBehaviour
    {
        private bool uiDisplayed;
        
        public void BuildInventoryUserInterface()
        {
            if (uiDisplayed) return;
            GameObject newCanvas = new GameObject("Dynamic UI Canvas");
            Canvas c = newCanvas.AddComponent<Canvas>();
            c.renderMode = RenderMode.ScreenSpaceOverlay;
            newCanvas.AddComponent<CanvasScaler>();
            newCanvas.AddComponent<GraphicRaycaster>();
            GameObject panel = new GameObject("Dynamic UI Panel");
            panel.AddComponent<CanvasRenderer>();
            Image image = panel.AddComponent<Image>();
            Sprite[] uiBackgrounds = Resources.LoadAll<Sprite>("Assets/Sprites/UI_Sticks/OVERWORLD_InventoryBackground_02.png");
            Debug.Log(uiBackgrounds);
            image.sprite = uiBackgrounds[0];
            panel.transform.SetParent(newCanvas.transform, false);
            uiDisplayed = true;
        }
    }
}