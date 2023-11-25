using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;
using System;

public class PauseGame : MonoBehaviour
{
    [SerializeField]
    private ObjectVariable _gameManagerObject;

    [SerializeField]
    private BoolVariable _pauseButtonBool;

    private GameManager _gameManager;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = (GameManager)_gameManagerObject;

        _pauseButtonBool.AddListener(HandlePauseButtonPress);
    }

    private void HandlePauseButtonPress(bool arg0)
    {
        if (!arg0)
        {
            // button was released, do nothing
            return;
        }

        // if current state is play, then pause the game
        if (_gameManager.GameManagerState is PlayState)
        {
            _gameManager.ChangeToPauseState();
        }
        else if (_gameManager.GameManagerState is PauseState)
        {
            _gameManager.ChangeToPlayState();
        }
    }
}
