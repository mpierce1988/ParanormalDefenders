using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField]
    private StartingWeaponSO _startingWeapon;

    [SerializeField]
    private PlayerInventory _playerInventory;

    private List<Weapon> _activeWeapons = new List<Weapon>();

    // Start is called before the first frame update
    void Start()
    {
        // clear anything remaining in player inventory weapons
        _playerInventory.ClearWeaponsList();
        // add starting weapon
        // reset starting weapon first
        _startingWeapon.StartingWeapon.ResetWeaponData();
        _playerInventory.AddWeapon(_startingWeapon.StartingWeapon);

        // spawn first weapon
        SpawnWeapon(_playerInventory.Weapons[0]);

        // listen for new weapons in inventory
        _playerInventory.OnWeaponsChange.AddListener(OnPlayerInventoryChange);

    }

    private void OnPlayerInventoryChange()
    {
        // find list of new weapons to add
        List<WeaponDataSO> _weaponsToAdd = new List<WeaponDataSO>();

        foreach (WeaponDataSO wd in _playerInventory.Weapons)
        {
            // if weapon isn't in the list of active weapons, add it
            if (!_activeWeapons.Any(aw => aw.WeaponData == wd))
            {
                // reset weapon data first
                wd.ResetWeaponData();
                _weaponsToAdd.Add(wd);
            }
        }

        foreach (WeaponDataSO wd in _weaponsToAdd)
        {
            SpawnWeapon(wd);
        }

        // find list of active weapons that need to be removed
        List<Weapon> _weaponsToRemove = new List<Weapon>();

        foreach (Weapon wp in _activeWeapons)
        {
            if (!_playerInventory.Weapons.Any(w => w == wp.WeaponData))
            {
                _weaponsToRemove.Add(wp);
            }
        }

        foreach (Weapon wp in _weaponsToRemove)
        {
            Destroy(wp.gameObject);
        }

    }

    private void SpawnWeapon(WeaponDataSO weaponDataSO)
    {
        // create Weapon gameobject
        GameObject weaponGO = new GameObject();
        weaponGO.transform.parent = this.transform;
        weaponGO.transform.localPosition = Vector3.zero;
        weaponGO.name = $"Weapon {_activeWeapons.Count + 1}";
        // create child object for active projectiles
        GameObject projectilesGO = new GameObject();
        projectilesGO.transform.parent = weaponGO.transform;
        projectilesGO.name = "Active Projectiles";
        // create child object for active projectiles
        GameObject inactiveGO = new GameObject();
        inactiveGO.transform.parent = weaponGO.transform;
        inactiveGO.name = "Inactive Projectiles";
        // add object pooler to weapon for projectiles
        ObjectPooler objectPooler = weaponGO.AddComponent<ObjectPooler>();
        objectPooler.Initialize(weaponDataSO.CurrentProjectilePrefab,
            weaponDataSO.DefaultPoolCapacity,
            weaponDataSO.MaxPoolCapacity, projectilesGO.transform,
            inactiveGO.transform);

        // add Weapon component
        Weapon weapon = weaponGO.AddComponent<Weapon>();
        weapon.SetWeaponData(weaponDataSO);
        weapon.SetProjectilesParent(projectilesGO.transform);
        weapon.SetObjectPool(objectPooler);
        // add weapon to list of active weapons
        _activeWeapons.Add(weapon);
        // start weapon
        weapon.StartFiring();
    }


}
