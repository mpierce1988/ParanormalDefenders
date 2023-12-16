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
    private ObjectPooler _projectilePool;

    public void SetObjectPool(ObjectPooler projectilePool)
    {
        _projectilePool = projectilePool;
    }


    public void SetWeaponData(WeaponDataSO weaponData)
    {
        if (_weaponData != null)
        {
            _weaponData.UpgradeWeaponEvent.RemoveListener(UpgradeWeapon);
        }

        _weaponData = weaponData;

        _weaponData.ResetWeaponData();

        _weaponData.UpgradeWeaponEvent.AddListener(UpgradeWeapon);

    }

    public void SetProjectilesParent(Transform projectilesParent)
    {
        _projectilesParent = projectilesParent;
    }

    IEnumerator FireWeaponFixedTime()
    {
        float timeBetweenShots = 0f;

        if (_weaponData.CurrentTimeBetweenProjectiles > 0)
        {
            timeBetweenShots = _weaponData.CurrentTimeBetweenProjectiles;
        }
        else
        {
            timeBetweenShots = _weaponData.CurrentWeaponFireTime / _weaponData.CurrentNumProjectiles;
        }

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
        float totalTimePassed = 0;

        while (totalShotsFired < _weaponData.CurrentNumProjectiles)
        {
            if (!_isFiring)
            {
                // stop firing
                yield break;
            }

            totalShotsFired += _weaponData.SpawnProjectiles(SpawnProjectile, this.transform, _projectilesParent);

            yield return new WaitForSeconds(_weaponData.CurrentTimeBetweenProjectiles);
            totalTimePassed += _weaponData.CurrentTimeBetweenProjectiles;
        }

        // check if weapon fire time is being used. if so, we might need to wait until weapon fire
        // time is finished
        if (totalTimePassed < _weaponData.CurrentWeaponFireTime)
        {
            // wait for the remaining time
            yield return new WaitForSeconds(_weaponData.CurrentWeaponFireTime - totalTimePassed);
        }

        // start cooldown
        if (_isFiring)
        {
            StartCoroutine(Cooldown());
        }
    }

    private GameObject SpawnProjectile(Vector2 position, Quaternion rotation, Transform parentTransform)
    {
        if (_projectilePool == null)
        {
            // no pool, instantiate a new gameobject
            GameObject projectile = Instantiate(_weaponData.CurrentProjectilePrefab, position, rotation, parentTransform);
            projectile.SetActive(true);
            return projectile;
        }
        var result = _projectilePool.GetItem();
        result.SetActive(false);
        result.transform.position = position;
        result.transform.rotation = rotation;
        result.transform.parent = parentTransform;
        result.SetActive(true);
        return result;

    }

    private void UpgradeWeapon()
    {
        if (_weaponData != null)
        {
            _weaponData.ApplyNextUpgrade();
        }
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
        if (_weaponData.CurrentClearProjectilesOnCooldown)
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
