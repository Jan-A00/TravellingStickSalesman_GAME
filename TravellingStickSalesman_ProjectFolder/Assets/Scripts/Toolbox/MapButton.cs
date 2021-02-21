using DataManagement;
using UnityEngine;
using UnityEngine.UI;

namespace Toolbox
{
    public class MapButton : MonoBehaviour
    {
        private Button openButton;

        private void Start()
        {
            openButton = gameObject.GetComponent<Button>();
        }
        
        private void Update()
        {
            openButton.interactable = GameStateManager.Instance.ReadyToTrade();
        }
    }
}