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

    private bool _isFiring = false;

    private void OnEnable()
    {
        // Initialize initial WeaponData values
        _weaponData.ResetWeaponData();
        // DEBUG remove later
        StartFiring();
    }

    IEnumerator FireWeapon()
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
        GameObject projectile = Instantiate(_weaponData.CurrentProjectilePrefab);
        projectile.transform.position = position;
        projectile.transform.rotation = rotation;
        projectile.transform.parent = parentTransform.transform;
        projectile.SetActive(true);
        return projectile;
    }

    private Vector2 GetProjectileStartPosition(Vector2 origin, Vector2 originOffset)
    {
        return new Vector2(origin.x + originOffset.x, origin.y + originOffset.y);
    }

    public void StartFiring()
    {
        Debug.Log("Weapon start firing...");
        _isFiring = true;
        StartCoroutine(FireWeapon());
    }

    public void StopFiring()
    {
        _isFiring = false;
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(_weaponData.CurrentCooldown);

        // Fire Weapon
        if (_isFiring)
        {
            StartCoroutine(FireWeapon());
        }
    }

    GameObject GetProjectile()
    {
        GameObject projectile = Instantiate(_weaponData.CurrentProjectilePrefab);
        projectile.transform.parent = _projectilesParent.transform;
        return projectile;
    }

    Vector2 GetRandomOrigin()
    {
        int randomIndex = Random.Range(0, _weaponData.ProjectileOrigins.Count - 1);
        return _weaponData.ProjectileOrigins[randomIndex];
    }
}
