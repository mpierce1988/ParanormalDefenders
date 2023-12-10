using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjectArchitecture;
using UnityEngine;

public class PlayerUpgradesController : MonoBehaviour
{
    [SerializeField]
    private IntVariable _currentPlayerLevel;

    [SerializeField]
    private PlayerInventory _playerInventory;

    [SerializeField]
    private PlayerInventory _allAvailableWeapons;

    [SerializeField]
    private PlayerAvailableUpgradesSO _playerAvailableUpgrades;

    [SerializeField]
    private ObjectVariable _gameManager;

    private int _numberOfOptions = 4;

    private void Awake()
    {
        _playerAvailableUpgrades.ClearOptions();
    }

    // Start is called before the first frame update
    void Start()
    {
        _currentPlayerLevel.AddListener(HandlePlayerLevelUp);
    }

    private void OnDestroy()
    {
        _currentPlayerLevel.RemoveListener(HandlePlayerLevelUp);
    }

    void HandlePlayerLevelUp()
    {
        try
        {
            // create list of options for player to choose from
            List<UpgradeOption> upgradeOptions = new();

            foreach (WeaponDataSO wd in GetAvailableWeapons())
            {
                upgradeOptions.Add(new UpgradeOption()
                {
                    UpgradeType = UpgradeType.NewWeapon,
                    WeaponData = wd
                });
            }

            foreach (WeaponDataSO wd in GetAvailableUpgrades())
            {
                upgradeOptions.Add(new UpgradeOption()
                {
                    UpgradeType = UpgradeType.WeaponUpgrade,
                    WeaponData = wd
                });
            }

            // shuffle options
            upgradeOptions.Shuffle();

            // only use the maximum amount of options
            if (upgradeOptions.Count > _numberOfOptions)
            {
                upgradeOptions.RemoveRange(_numberOfOptions,
                    upgradeOptions.Count - _numberOfOptions);
            }

            // save values to SO list
            _playerAvailableUpgrades.SetUpgradeOptions(upgradeOptions);
            // signal to game manager to switch to upgrade state
            ((GameManager)_gameManager).ChangeToUpgradeState();
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
        }

    }

    List<WeaponDataSO> GetAvailableUpgrades()
    {
        List<WeaponDataSO> results = new();

        foreach (WeaponDataSO wd in _playerInventory.Weapons)
        {
            if (wd.GetNextUpgrade() != null)
            {
                results.Add(wd);
            }
        }

        return results;
    }

    List<WeaponDataSO> GetAvailableWeapons()
    {
        List<WeaponDataSO> results = new();

        foreach (WeaponDataSO wd in _allAvailableWeapons.Weapons)
        {
            if (!_playerInventory.Weapons.Contains(wd))
            {
                results.Add(wd);
            }
        }

        return results;
    }
}

public enum UpgradeType
{
    NewWeapon,
    WeaponUpgrade
}

[Serializable]
public struct UpgradeOption
{
    public UpgradeType UpgradeType;
    public WeaponDataSO WeaponData;
}
