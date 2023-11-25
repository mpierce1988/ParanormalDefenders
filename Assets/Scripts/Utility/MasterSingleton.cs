using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterSingleton : MonoBehaviour
{
    private static MasterSingleton _instance;
    public static MasterSingleton Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<MasterSingleton>();
                if (_instance == null)
                {
                    // create a Master Singleton game object
                    GameObject go = new GameObject("MasterSingleton");
                    _instance = go.AddComponent<MasterSingleton>();
                }
            }

            return _instance;
        }
    }

    [SerializeField]
    private SceneLoader _sceneLoader;

    public SceneLoader SceneLoader => _sceneLoader;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
}
