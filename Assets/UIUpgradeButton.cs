using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ScriptableObjectArchitecture;

[RequireComponent(typeof(Button))]
public class UIUpgradeButton : MonoBehaviour
{
    [SerializeField]
    private UpgradeOption _upgradeOption;

    [SerializeField]
    private UpgradeGameEventSO _upgradeGameEvent;

    [SerializeField]
    private PlayerInventory _playerInventory;

    [SerializeField]
    private TextMeshProUGUI _upgradeTypeText;

    [SerializeField]
    private TextMeshProUGUI _weaponNameText;

    [SerializeField]
    private TextMeshProUGUI _numProjectilesText;

    [SerializeField]
    private TextMeshProUGUI _fireRateText;

    [SerializeField]
    private TextMeshProUGUI _fireTimeText;

    [SerializeField]
    private TextMeshProUGUI _cooldownText;

    [SerializeField]
    private TextMeshProUGUI _damageText;

    [SerializeField]
    private ObjectVariable _gameManager;


    private Button _button;
    private bool _isSet = false;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }


    public void SetUpgradeOption(UpgradeOption option)
    {
        _upgradeOption = option;

        // set text values for display
        if (_upgradeOption.UpgradeType == UpgradeType.NewWeapon)
        {
            _upgradeTypeText.text = "New Weapon";
        }
        else if (_upgradeOption.UpgradeType == UpgradeType.WeaponUpgrade)
        {
            _upgradeTypeText.text = "Weapon Upgrade";
        }

        _weaponNameText.text = _upgradeOption.WeaponData.WeaponName == ""
            ? "Unknown"
            : _upgradeOption.WeaponData.WeaponName;

        _numProjectilesText.text = _upgradeOption.WeaponData.CurrentNumProjectiles.ToString();

        _fireRateText.text = _upgradeOption.WeaponData.CurrentTimeBetweenProjectiles.ToString();

        _fireTimeText.text = _upgradeOption.WeaponData.CurrentWeaponFireTime.ToString();

        _cooldownText.text = _upgradeOption.WeaponData.CurrentCooldown.ToString();

        _damageText.text = _upgradeOption.WeaponData.CurrentProjectileDamage.ToString();

        _isSet = true;
    }

    public void HandleOnClick()
    {
        if (!_isSet)
        {
            Debug.Log("Upgrade Option not set for Upgrade Button");
            return;
        }

        if (_upgradeOption.UpgradeType == UpgradeType.WeaponUpgrade)
        {
            _upgradeOption.WeaponData.ApplyNextUpgrade();
        }
        else if (_upgradeOption.UpgradeType == UpgradeType.NewWeapon)
        {
            _playerInventory.AddWeapon(_upgradeOption.WeaponData);
        }

        _upgradeGameEvent.Invoke(_upgradeOption);

        // change back to gameplay state
        ((GameManager)_gameManager).ChangeToPlayState();
    }


}
