using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;
using UnityEngine.UI;
using TMPro;

public class UIPlayerInfo : MonoBehaviour
{
    [SerializeField]
    private IntVariable _currentHealth;
    [SerializeField]
    private IntVariable _maxHealth;
    [SerializeField]
    private IntVariable _coins;
    [SerializeField]
    private IntVariable _currentXP;
    [SerializeField]
    private IntVariable _nextXPThreshold;

    [SerializeField]
    private Slider _healthSlider;
    [SerializeField]
    private Slider _xpSlider;
    [SerializeField]
    private TextMeshProUGUI _coinsText;

    private int _previousXPThreshold = 0;
    private int _currentXPThreshold = 0;

    private void Start()
    {
        _currentXPThreshold = _nextXPThreshold;
    }

    private void OnEnable()
    {
        _currentHealth.AddListener(UpdateHealth);
        _maxHealth.AddListener(UpdateHealth);

        _currentXP.AddListener(UpdateXP);
        _nextXPThreshold.AddListener(UpdateXPThreshold);


        _coins.AddListener(UpdateCoins);

        UpdateHealth();
        UpdateXP();
        UpdateCoins();
    }

    private void OnDisable()
    {
        _currentHealth.RemoveListener(UpdateHealth);
        _maxHealth.RemoveListener(UpdateHealth);

        _currentXP.RemoveListener(UpdateXP);
        _nextXPThreshold.RemoveListener(UpdateXPThreshold);

        _coins.RemoveListener(UpdateCoins);
    }

    private void UpdateHealth()
    {
        _healthSlider.value = Mathf.Clamp01((float)_currentHealth.Value / (float)_maxHealth.Value);
    }

    private void UpdateXPThreshold()
    {
        if (_currentXPThreshold != _nextXPThreshold)
        {
            _previousXPThreshold = _currentXPThreshold;
            _currentXPThreshold = _nextXPThreshold;
            UpdateXP();
        }

    }

    private void UpdateXP()
    {
        _xpSlider.value = Mathf.Clamp01(((float)_currentXP.Value - _previousXPThreshold) / ((float)_nextXPThreshold.Value - _previousXPThreshold));
    }

    private void UpdateCoins()
    {
        _coinsText.text = _coins.Value.ToString();
    }
}
