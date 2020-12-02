using System;
using DataManagement;
using UnityEngine;

namespace UserInterface
{
    public class HiddenDialogLauncher : MonoBehaviour
    {
        public DialogSequencer dialogSequencer;
        public string lineText;
        public AudioClip lineAudio;
        public GameObject sceneInteraction;
        private BoxCollider2D collider;

        private void Start()
        {
            collider = gameObject.GetComponent<BoxCollider2D>();
            Debug.Log(collider.enabled);
        }

        private void Update()
        {
            collider.enabled = GameStateManager.Instance.ReadyToTrade();
        }

        private void OnMouseDown()
        {
            Debug.Log("Clicked on some hidden dialog.");
            dialogSequencer.ShowDialogueBox(lineText, lineAudio);
            sceneInteraction.SetActive(false);
        }
    }
}