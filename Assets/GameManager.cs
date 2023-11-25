using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameManagerStateSO _gameManagerState;

    [SerializeField]
    private ObjectVariable _gameManagerObject;


    private PlayState _playState;
    private PauseState _pauseState;

    public GameManagerState GameManagerState => _gameManagerState.GameManagerState;

    private void Awake()
    {
        _gameManagerObject.BaseValue = this;
        _gameManagerObject.Raise();
    }

    // Start is called before the first frame update
    void Start()
    {
        _playState = new PlayState(this);
        _pauseState = new PauseState(this);

        // default state is play state
        ChangeState(_playState);
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameManagerState.GameManagerState != null)
        {
            _gameManagerState.GameManagerState.Update();
        }
    }

    public void ChangeToPlayState()
    {
        if (_gameManagerState.GameManagerState != null
            && _gameManagerState.GameManagerState == _playState)
        {
            // state is already play state, so do nothing
            return;
        }

        ChangeState(_playState);
    }

    public void ChangeToPauseState()
    {
        if (_gameManagerState.GameManagerState != null
            && _gameManagerState.GameManagerState == _pauseState)
        {
            // state is already pause state, so do nothing
            return;
        }

        ChangeState(_pauseState);
    }

    private void ChangeState(GameManagerState newState)
    {
        if (_gameManagerState.GameManagerState != null)
        {
            _gameManagerState.GameManagerState.Exit();
        }

        _gameManagerState.SetGameManagerState(newState);

        if (_gameManagerState.GameManagerState != null)
        {
            _gameManagerState.GameManagerState.Enter();
        }
    }
}
