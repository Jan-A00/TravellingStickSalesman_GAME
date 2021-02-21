using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    public GameObject[] credits;
    public int creditIndex;

    // Start is called before the first frame update
    public void NextCredit()
    {
        credits[creditIndex].SetActive(false);
        credits[creditIndex + 1].SetActive(true);
        creditIndex++;
        if(creditIndex == 4)
        {
            Application.Quit();
            Debug.Log("quitting");
        }
    }
}
