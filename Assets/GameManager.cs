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
    private UpgradeState _upgradeState;

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
        _upgradeState = new UpgradeState(this);

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

        ChangeState(_playState);
    }

    public void ChangeToPauseState()
    {

        ChangeState(_pauseState);
    }

    public void ChangeToUpgradeState()
    {
        ChangeState(_upgradeState);
    }


    private void ChangeState(GameManagerState newState)
    {
        // check if we are already in this state
        if (_gameManagerState.GameManagerState != null
            && _gameManagerState.GameManagerState == newState)
        {
            // we are already in this state, do nothing
            return;
        }

        if (_gameManagerState.GameManagerState != null)
        {
            _gameManagerState.GameManagerState.Exit();
        }

        _gameManagerState.SetGameManagerState(newState);

        if (_gameManagerState.GameManagerState != null)
        {
            _gameManagerState.GameManagerState.Enter();
        }

        _gameManagerState.OnGameManagerStateChange?.Invoke(_gameManagerState.GameManagerState);
    }
}
