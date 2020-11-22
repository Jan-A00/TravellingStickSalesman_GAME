using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class InventoryOpenButton : MonoBehaviour
    {
        
        public void OpenInventoryUserInterface()
        {
            if (!gameObject.activeSelf)
            {
                Debug.Log("Re-Enabling the \"Open Inventory\" button.");
                gameObject.SetActive(true);
            }
            else
            {
                Debug.Log("The \"Open Inventory\" button was already active, ignoring.");
            }
        }
    }
}