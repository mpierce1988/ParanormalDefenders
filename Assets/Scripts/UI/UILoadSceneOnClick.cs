using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILoadSceneOnClick : MonoBehaviour
{
    [SerializeField]
    private string _sceneName;

    public void LoadScene()
    {
        MasterSingleton.Instance.SceneLoader.LoadScene(_sceneName);
    }
}
