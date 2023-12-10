using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Player Inventory", menuName = "Custom/New Player Inventory")]
public class PlayerInventory : ScriptableObject
{
    [SerializeField]
    private List<WeaponDataSO> _weapons;

    public List<WeaponDataSO> Weapons => _weapons;

    public UnityEvent OnWeaponsChange;

    public void AddWeapon(WeaponDataSO weaponData)
    {
        _weapons.Add(weaponData);
        OnWeaponsChange.Invoke();
    }

    public void RemoveWeapon(WeaponDataSO weaponData)
    {
        if (_weapons.Contains(weaponData))
        {
            _weapons.Remove(weaponData);
            OnWeaponsChange.Invoke();
        }
    }

    public void ClearWeaponsList()
    {
        _weapons.Clear();
        OnWeaponsChange.Invoke();
    }

}