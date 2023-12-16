using System.Collections;
using System.Collections.Generic;
using ScriptableObjectArchitecture;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIPausePanelController : MonoBehaviour
{
    [SerializeField]
    private ObjectVariable _gameManager;



    public void ReloadCurrentScene()
    {
        // get current scene name
        var sceneName = SceneManager.GetActiveScene().name;

        // load via scene name
        MasterSingleton.Instance.SceneLoader.LoadScene(sceneName);
    }

    public void LoadMenu()
    {
        MasterSingleton.Instance.SceneLoader.LoadScene("Menu");
    }

    public void Continue()
    {
        GameManager gm = _gameManager.Value as GameManager;

        gm.ChangeToPlayState();
    }
}
