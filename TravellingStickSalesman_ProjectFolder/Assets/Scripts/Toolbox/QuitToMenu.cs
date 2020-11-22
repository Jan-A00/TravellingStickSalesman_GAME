using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Toolbox
{
    public class QuitToMenu : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
    }
}