using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUICanvasController : MonoBehaviour
{
    [SerializeField]
    private GameObject _startPanel;

    [SerializeField]
    private GameObject _selectLevelPanel;

    private void Start()
    {
        _startPanel.active = true;
        _selectLevelPanel.active = false;
    }

    public void OnPlayPressed()
    {
        _startPanel.active = false;
        _selectLevelPanel.active = true;
    }

    public void OnBackToStartPressed()
    {
        _startPanel.active = true;
        _selectLevelPanel.active = false;
    }

    public void OnQuitPressed()
    {
        Application.Quit();
    }
}
