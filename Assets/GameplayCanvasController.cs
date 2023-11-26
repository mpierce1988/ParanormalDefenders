using System.Collections;
using System.Collections.Generic;
using ScriptableObjectArchitecture;
using UnityEngine;

public class GameplayCanvasController : MonoBehaviour
{
    [SerializeField]
    private GameManagerStateSO _gameManagerState;
    [SerializeField]
    private GameObject _gameplayPanel;
    [SerializeField]
    private GameObject _pausePanel;

    private GameManager _gm;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        _gameManagerState.OnGameManagerStateChange.AddListener(HandleGameManagerStateChange);

        HandleGameManagerStateChange(_gameManagerState.GameManagerState);
    }

    private void HandleGameManagerStateChange(GameManagerState state)
    {
        if (state is PlayState)
        {
            _pausePanel.SetActive(false);
            _gameplayPanel.SetActive(true);
        }
        else if (state is PauseState)
        {
            _gameplayPanel.SetActive(false);
            _pausePanel.SetActive(true);
        }
    }

}
