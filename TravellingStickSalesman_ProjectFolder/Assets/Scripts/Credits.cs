using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    public GameObject[] credits;
    public int creditIndex;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextCredit();
        }
    }

    // Start is called before the first frame update
    public void NextCredit()
    {
        credits[creditIndex].SetActive(false);
        credits[creditIndex + 1].SetActive(true);
        creditIndex++;
        if(creditIndex == 4)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
