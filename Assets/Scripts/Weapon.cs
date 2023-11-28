using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
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
        int shotsFired = 0;

        while (shotsFired < _weaponData.CurrentNumProjectiles)
        {
            if (!_isFiring)
            {
                // stop firing
                yield break;
            }
            // spawn projectile at a random origin
            GameObject proj = GetProjectile();
            Vector2 originOffset = GetRandomOrigin();
            proj.transform.position =
                new Vector2(transform.position.x + originOffset.x,
                transform.position.y + originOffset.y);

            shotsFired++;

            yield return new WaitForSeconds(_weaponData.CurrentTimeBetweenProjectiles);
        }

        // start cooldown
        if (_isFiring)
        {
            StartCoroutine(Cooldown());
        }
    }

    public void StartFiring()
    {
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
