using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace UserInterface
{
    public class DisplayDialogue : MonoBehaviour
    {
        public string lineText;
        public AudioClip lineAudio;
        public AudioSource audioSource;
        public Text textArea;
        public GameObject continueButton;
        private float typingSpeed;
        private bool textComplete;
        private bool audioComplete;


        private void Start()
        {
            typingSpeed = 0.01f;
            audioSource.clip = lineAudio;
            StartCoroutine(Type());
            StartCoroutine(Speak());
        }

        private void Update()
        {
            if (CanContinue()) continueButton.SetActive(true);
        }
        
        private bool CanContinue()
        {
            return textComplete && audioComplete;
        } 

        private IEnumerator Type()
        {
            foreach (char letter in lineText)
            {
                textArea.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }
            textComplete = true;
        }

        private IEnumerator Speak()
        {
            yield return new WaitForSeconds(0.05f);
            audioSource.Play();
            yield return new WaitForSeconds(1);
            audioComplete = true;
        }

        public void RemoveDialogueBox()
        {
            Destroy(gameObject);
        }
    }
}