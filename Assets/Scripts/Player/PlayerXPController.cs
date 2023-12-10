using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjectArchitecture;
using UnityEngine;

public class PlayerXPController : MonoBehaviour
{
    [SerializeField]
    private IntCollection _playerXPThresholds;

    [SerializeField]
    private IntVariable _nextXPThreshold;

    [SerializeField]
    private IntVariable _currentPlayerXP;

    [SerializeField]
    private IntVariable _currentPlayerLevel;

    [SerializeField]
    private IntGameEvent _onXPPickup;

    private void Awake()
    {
        // reset current xp to zero
        _currentPlayerXP.BaseValue = 0;

        _currentPlayerLevel.BaseValue = 1;

        _nextXPThreshold.BaseValue = _playerXPThresholds[0];
    }

    // Start is called before the first frame update
    void Start()
    {
        _onXPPickup.AddListener(HandleXPPickup);

    }

    private void OnDestroy()
    {
        _onXPPickup.RemoveListener(HandleXPPickup);
    }

    private void HandleXPPickup(int amount)
    {
        _currentPlayerXP.BaseValue = _currentPlayerXP + amount;
        _currentPlayerXP.Raise();

        if (_currentPlayerXP > _nextXPThreshold)
        {
            HandleLevelUp();
            SetNextXPThreshold();
        }
    }

    private void HandleLevelUp()
    {
        _currentPlayerLevel.BaseValue = _currentPlayerLevel + 1;
        _currentPlayerLevel.Raise();
        Debug.Log("Player leveled up!!!");
    }

    private void SetNextXPThreshold()
    {
        int currentThresholdIndex = _currentPlayerLevel - 1;

        if (_nextXPThreshold != _playerXPThresholds[currentThresholdIndex])
        {
            _nextXPThreshold.BaseValue = _playerXPThresholds[currentThresholdIndex];
            _nextXPThreshold.Raise();
        }
    }
}
