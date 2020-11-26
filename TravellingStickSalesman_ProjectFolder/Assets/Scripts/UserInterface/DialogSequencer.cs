using System.ComponentModel.Design.Serialization;
using DataManagement.StateTypes;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UIElements;

namespace UserInterface
{
    public class DialogSequencer : MonoBehaviour
    {
        public GameObject dialogBoxPrefab;
        private Transform NormalUIRoot => GameObject.FindGameObjectWithTag("Normal-UserInterface").GetComponent<Transform>();
        
        public void ShowDialogueBox(string lineText, AudioClip lineAudio)
        {
            float boxWidth = 1071f;
            float boxHeight = 166.5f;
            float bottomPadding = 23.75f;
            float boxCenterY = -Screen.height / 2 + boxHeight + bottomPadding;
            GameObject dialogBox = Instantiate(dialogBoxPrefab, NormalUIRoot, true);
            Vector3 position = new Vector3(0, boxCenterY, -1000.0f);
            dialogBox.transform.localPosition = position;
            dialogBox.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            ((RectTransform) dialogBox.transform).SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, boxWidth);
            ((RectTransform) dialogBox.transform).SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, boxHeight);
            ((RectTransform) dialogBox.transform).ForceUpdateRectTransforms();
            DisplayDialogue component = dialogBox.GetComponent<DisplayDialogue>();
            component.lineText = lineText;
            component.lineAudio = lineAudio;
        } 
    }
}