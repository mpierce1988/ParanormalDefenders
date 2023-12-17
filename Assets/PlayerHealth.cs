using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjectArchitecture;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, ITakeDamage
{
    [SerializeField]
    private IntVariable _playerHealth;
    [SerializeField]
    private IntVariable _maxPlayerHealth;
    [SerializeField]
    private GameEvent _onPlayerDied;

    void Start()
    {
        // reset player health to max health at start of level
        _playerHealth.BaseValue = _maxPlayerHealth.Value;
        _playerHealth.Raise();
    }


    public void TakeDamage(int damage)
    {
        float healthAfterDamage = _playerHealth.Value - damage;
        if (healthAfterDamage < 0) healthAfterDamage = 0;
        _playerHealth.BaseValue = healthAfterDamage;
        _playerHealth.Raise();

        if (_playerHealth == 0)
        {
            _onPlayerDied.Raise();
        }
    }
}
