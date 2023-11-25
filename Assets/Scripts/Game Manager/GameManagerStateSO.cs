using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static GameManager;

[CreateAssetMenu(fileName = "Custom/SO", menuName = "GameManagerState")]
public class GameManagerStateSO : ScriptableObject
{
    [SerializeField]
    private GameManagerState _gameManagerState;

    public UnityEvent<GameManagerState> OnGameManagerStateChange;

    public GameManagerState GameManagerState
    {
        get
        {
            return _gameManagerState;
        }
    }

    public void SetGameManagerState(GameManagerState gameManagerState)
    {
        _gameManagerState = gameManagerState;
        OnGameManagerStateChange?.Invoke(_gameManagerState);
    }
}
