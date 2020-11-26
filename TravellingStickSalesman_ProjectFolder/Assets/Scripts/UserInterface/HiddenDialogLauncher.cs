using System;
using UnityEngine;

namespace UserInterface
{
    public class HiddenDialogLauncher : MonoBehaviour
    {
        public DialogSequencer dialogSequencer;
        public string lineText;
        public AudioClip lineAudio;
        public GameObject sceneInteraction;
        
        private void OnMouseDown()
        {
            Debug.Log("Clicked on some hidden dialog.");
            dialogSequencer.ShowDialogueBox(lineText, lineAudio);
            sceneInteraction.SetActive(false);
        }
    }
}