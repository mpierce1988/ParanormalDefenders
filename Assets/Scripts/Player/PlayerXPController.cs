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
    private IntGameEvent _onXPPickup;


    // Start is called before the first frame update
    void Start()
    {
        // reset current xp to zero
        _currentPlayerXP.BaseValue = 0;
        _currentPlayerXP.Raise();

        _onXPPickup.AddListener(HandleXPPickup);
        SetNextXPThreshold();

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
        Debug.Log("Player leveled up!!!");
    }

    private void SetNextXPThreshold()
    {
        int currentThresholdIndex = 0;

        for (int i = 0; i < _playerXPThresholds.Count; i++)
        {
            if (_currentPlayerXP < _playerXPThresholds[i])
            {
                currentThresholdIndex = i;
                break;
            }
        }

        if (_nextXPThreshold != _playerXPThresholds[currentThresholdIndex])
        {
            _nextXPThreshold.BaseValue = _playerXPThresholds[currentThresholdIndex];
            _nextXPThreshold.Raise();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
