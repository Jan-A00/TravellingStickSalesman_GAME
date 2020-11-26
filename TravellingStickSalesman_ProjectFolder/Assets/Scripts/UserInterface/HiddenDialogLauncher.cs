using System;
using UnityEngine;

namespace UserInterface
{
    public class HiddenDialogLauncher : MonoBehaviour
    {
        public DialogSequencer dialogSequencer;
        public string lineText;
        public AudioClip lineAudio;
        
        private void OnMouseDown()
        {
            Debug.Log("Clicked on some hidden dialog.");
            dialogSequencer.ShowDialogueBox(lineText, lineAudio);
        }
    }
}