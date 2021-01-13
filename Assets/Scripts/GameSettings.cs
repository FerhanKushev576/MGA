using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This component is only so we can have a singleton holder for our scriptable object instance
public class GameSettings : MonoBehaviour
{
    #region Singleton
    private static GameSettings _instance;
    public static GameSettings Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameSettings>();

                //Tell unity not to destroy this object when loading a new scene!
                DontDestroyOnLoad(_instance.gameObject);
            }

            return _instance;
        }
    }

    void Awake()
    { 
        if (_instance == null)
        {
            //If I am the first instance, make me the Singleton
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            //If a Singleton already exists and you find
            //another reference in scene, destroy it!
            if (this != _instance)
                Destroy(gameObject);
        }
        
    }
    #endregion

    public GameSettings_SO gameSettings;
}
