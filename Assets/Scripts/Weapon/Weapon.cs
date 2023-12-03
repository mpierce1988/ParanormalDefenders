using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public delegate GameObject SpawnFunction(Vector2 position, Quaternion rotation, Transform parentTransform);

    [SerializeField]
    private WeaponDataSO _weaponData;

    [SerializeField]
    private Transform _projectilesParent;

    public WeaponDataSO WeaponData => _weaponData;

    private bool _isFiring = false;

    private void OnEnable()
    {
        // Initialize initial WeaponData values
        _weaponData.ResetWeaponData();
        // DEBUG remove later
        //StartFiring();
    }

    public void SetWeaponData(WeaponDataSO weaponData)
    {
        _weaponData = weaponData;
    }

    public void SetProjectilesParent(Transform projectilesParent)
    {
        _projectilesParent = projectilesParent;
    }

    IEnumerator FireWeaponFixedTime()
    {
        float timeBetweenShots = _weaponData.CurrentWeaponFireTime / _weaponData.CurrentNumProjectiles;

        float timeProgress = 0f;

        while (timeProgress < _weaponData.CurrentWeaponFireTime)
        {
            _weaponData.SpawnProjectiles(SpawnProjectile, this.transform, _projectilesParent);

            yield return new WaitForSeconds(timeBetweenShots);

            timeProgress += timeBetweenShots;
        }

        if (_isFiring)
        {
            StartCoroutine(Cooldown());
        }
    }

    IEnumerator FireWeaponFixedAmount()
    {
        int totalShotsFired = 0;

        while (totalShotsFired < _weaponData.CurrentNumProjectiles)
        {
            if (!_isFiring)
            {
                // stop firing
                yield break;
            }

            totalShotsFired += _weaponData.SpawnProjectiles(SpawnProjectile, this.transform, _projectilesParent);

            yield return new WaitForSeconds(_weaponData.CurrentTimeBetweenProjectiles);
        }

        // start cooldown
        if (_isFiring)
        {
            StartCoroutine(Cooldown());
        }
    }

    private GameObject SpawnProjectile(Vector2 position, Quaternion rotation, Transform parentTransform)
    {
        GameObject projectile = Instantiate(_weaponData.CurrentProjectilePrefab, position, rotation, parentTransform);
        projectile.SetActive(true);
        return projectile;
    }

    public void StartFiring()
    {
        Debug.Log("Weapon start firing...");
        _isFiring = true;
        if (_weaponData.WeaponType == WeaponType.FixedAmount)
        {
            StartCoroutine(FireWeaponFixedAmount());
        }
        else if (_weaponData.WeaponType == WeaponType.FixedTime)
        {
            StartCoroutine(FireWeaponFixedTime());
        }

    }

    public void StopFiring()
    {
        _isFiring = false;
    }

    IEnumerator Cooldown()
    {
        if (_weaponData.WeaponType == WeaponType.FixedTime)
        {
            // destroy any projectiles already created
            foreach (var spawnable in _projectilesParent.GetComponentsInChildren<PlayerSpawnable>())
            {
                spawnable.DestroySpawnable();
            }
        }
        yield return new WaitForSeconds(_weaponData.CurrentCooldown);

        // Fire Weapon
        if (_isFiring)
        {
            if (_weaponData.WeaponType == WeaponType.FixedAmount)
            {
                StartCoroutine(FireWeaponFixedAmount());
            }
            else if (_weaponData.WeaponType == WeaponType.FixedTime)
            {
                StartCoroutine(FireWeaponFixedTime());
            }

        }
    }
}
