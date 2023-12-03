using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Starting Weapon SO", menuName = "Custom/New Starting Weapon Placeholder SO")]
public class StartingWeaponSO : ScriptableObject
{
    [SerializeField]
    private WeaponDataSO _startingWeapon;

    public WeaponDataSO StartingWeapon => _startingWeapon;

    public UnityEvent OnStartingWeaponChange;

    public void ChangeStartingWeapon(WeaponDataSO weaponData)
    {
        _startingWeapon = weaponData;
        OnStartingWeaponChange.Invoke();
    }
}
