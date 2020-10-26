using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickGameManager : MonoBehaviour
{
    private static StickGameManager _instance;

    [SerializeField]
    Character currentTrader;

    void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            //Init this object, load inventory from disk, etc.
            DontDestroyOnLoad(gameObject); //Tell unity not to destroy when changing scenes
        }
    }

    public static StickGameManager Instance
    {
        get { return _instance;}
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTrader(Character c)
    {
        currentTrader = c;
    }
    public Character GetTrader()
    {
        return currentTrader;
    }
}
