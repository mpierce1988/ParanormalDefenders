using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New WeaponData", menuName = "Custom/WeaponData")]
public class WeaponDataSO : ScriptableObject
{
    [SerializeField]
    private WeaponType _weaponType;

    [SerializeField]
    private int _initialNumProjectiles;

    [SerializeField]
    private float _initialTimeBetweenProjectiles;

    [SerializeField]
    private float _initialWeaponFireTime;

    [SerializeField]
    private float _initialCooldown;

    [SerializeField]
    private GameObject _initialProjectilePrefab;

    [SerializeField]
    private int _initialProjectileDamage;

    [SerializeField]
    private List<Vector2> _projectileOrigins;

    private int _currentNumProjectiles;
    private float _currentTimeBetweenProjectiles;
    private float _currentWeaponFireTime;
    private float _currentCooldown;
    private GameObject _currentProjectilePrefab;
    private int _currentProjectileDamage;

    public WeaponType WeaponType => _weaponType;
    public int InitialNumProjectiles => _initialNumProjectiles;
    public float InitialTimeBetweenProjectiles => _initialTimeBetweenProjectiles;
    public float InitialWeaponFireTime => _initialWeaponFireTime;
    public float InitialCooldown => _initialCooldown;
    public GameObject InitialProjectilePrefab => _initialProjectilePrefab;
    public int InitialProjectileDamage => _initialProjectileDamage;
    public List<Vector2> ProjectileOrigins => _projectileOrigins;

    public int CurrentNumProjectiles => _currentNumProjectiles;
    public float CurrentTimeBetweenProjectiles => _currentTimeBetweenProjectiles;
    public float CurrentWeaponFireTime => _currentWeaponFireTime;
    public float CurrentCooldown => _currentCooldown;
    public GameObject CurrentProjectilePrefab => _currentProjectilePrefab;
    public int CurrentProjectileDamage => _currentProjectileDamage;

    public delegate GameObject SpawnFunction(Vector2 position, Quaternion rotation, Transform parentTransform);

    private void Awake()
    {
        ResetWeaponData();
    }

    public void ResetWeaponData()
    {
        _currentNumProjectiles = _initialNumProjectiles;
        _currentTimeBetweenProjectiles = _initialTimeBetweenProjectiles;
        _currentWeaponFireTime = _initialWeaponFireTime;
        _currentCooldown = _initialCooldown;
        _currentProjectilePrefab = _initialProjectilePrefab;
        _currentProjectileDamage = _initialProjectileDamage;
    }

    public int SpawnProjectiles(SpawnFunction spawnFunction, Transform spawnerTransform, Transform projectileParentTransform)
    {
        Vector2 randomOrigin = GetRandomOrigin();

        Vector2 spawnPosition = new Vector2(spawnerTransform.position.x + randomOrigin.x,
            spawnerTransform.position.y + randomOrigin.y);

        GameObject obj = spawnFunction(spawnPosition, spawnerTransform.rotation, projectileParentTransform.transform);

        return 1;
    }

    virtual protected Vector2 GetRandomOrigin()
    {
        int randomIndex = UnityEngine.Random.Range(0, _projectileOrigins.Count);
        return _projectileOrigins[randomIndex];
    }
}
