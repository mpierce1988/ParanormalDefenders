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
        _startPanel.SetActive(true);
        _selectLevelPanel.SetActive(false);
    }

    public void OnPlayPressed()
    {
        _startPanel.SetActive(false);
        _selectLevelPanel.SetActive(true);
    }

    public void OnBackToStartPressed()
    {
        _startPanel.SetActive(true);
        _selectLevelPanel.SetActive(false);
    }

    public void OnQuitPressed()
    {
        Application.Quit();
    }
}
