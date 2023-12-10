using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjectArchitecture;
using UnityEngine;

[CreateAssetMenu(fileName = "New WeaponData", menuName = "Custom/WeaponData")]
public class WeaponDataSO : ScriptableObject
{
    [SerializeField]
    private string _name;

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

    [SerializeField]
    private List<WeaponUpgrade> _weaponUpgrades;

    [SerializeField]
    private GameEvent _upgradeWeaponEvent;

    private int _currentNumProjectiles;
    private float _currentTimeBetweenProjectiles;
    private float _currentWeaponFireTime;
    private float _currentCooldown;
    private GameObject _currentProjectilePrefab;
    private int _currentProjectileDamage;
    private List<Vector2> _currentProjectileOrigins;
    private int _currentUpgrade = -1;

    public WeaponType WeaponType => _weaponType;
    public string WeaponName => _name;
    public int InitialNumProjectiles => _initialNumProjectiles;
    public float InitialTimeBetweenProjectiles => _initialTimeBetweenProjectiles;
    public float InitialWeaponFireTime => _initialWeaponFireTime;
    public float InitialCooldown => _initialCooldown;
    public GameObject InitialProjectilePrefab => _initialProjectilePrefab;
    public int InitialProjectileDamage => _initialProjectileDamage;
    public List<Vector2> ProjectileOrigins => _projectileOrigins;
    public GameEvent UpgradeWeaponEvent => _upgradeWeaponEvent;

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
        if (_upgradeWeaponEvent != null)
        {
            _upgradeWeaponEvent.AddListener(ApplyNextUpgrade);
        }
    }

    private void Start()
    {

    }

    private void OnDestroy()
    {
        if (_upgradeWeaponEvent != null)
        {
            _upgradeWeaponEvent.RemoveListener(ApplyNextUpgrade);
        }
    }

    public void ResetWeaponData()
    {
        _currentNumProjectiles = _initialNumProjectiles;
        _currentTimeBetweenProjectiles = _initialTimeBetweenProjectiles;
        _currentWeaponFireTime = _initialWeaponFireTime;
        _currentCooldown = _initialCooldown;
        _currentProjectilePrefab = _initialProjectilePrefab;
        _currentProjectileDamage = _initialProjectileDamage;
        _currentProjectileOrigins = _projectileOrigins;
        _currentUpgrade = -1;
    }

    virtual public int SpawnProjectiles(SpawnFunction spawnFunction, Transform spawnerTransform, Transform projectileParentTransform)
    {
        Vector2 randomOrigin = GetRandomOrigin();

        Vector2 spawnPosition = new Vector2(spawnerTransform.position.x + randomOrigin.x,
            spawnerTransform.position.y + randomOrigin.y);

        GameObject obj = spawnFunction(spawnPosition, spawnerTransform.rotation, projectileParentTransform.transform);

        return 1;
    }

    virtual protected Vector2 GetRandomOrigin()
    {
        int randomIndex = UnityEngine.Random.Range(0, _currentProjectileOrigins.Count);
        return _currentProjectileOrigins[randomIndex];
    }

    virtual public void ApplyNextUpgrade()
    {
        Debug.Log("UPGRADING WEAPON!!!!!");
        var nextUpgradeIndex = _currentUpgrade + 1;

        if (nextUpgradeIndex < _weaponUpgrades.Count)
        {
            _currentUpgrade++;
            ApplyUpgrade(_weaponUpgrades[_currentUpgrade]);
        }
    }

    virtual protected void ApplyUpgrade(WeaponUpgrade weaponUpgrade)
    {
        if (weaponUpgrade.NumProjectiles > 0)
        {
            _currentNumProjectiles = weaponUpgrade.NumProjectiles;
        }

        if (weaponUpgrade.TimeBetweenProjectiles > 0)
        {
            _currentTimeBetweenProjectiles = weaponUpgrade.TimeBetweenProjectiles;
        }

        if (weaponUpgrade.WeaponFireTime > 0)
        {
            _currentWeaponFireTime = weaponUpgrade.WeaponFireTime;
        }

        if (weaponUpgrade.Cooldown > 0)
        {
            _currentCooldown = weaponUpgrade.Cooldown;
        }

        if (weaponUpgrade.ProjectilePrefab != null)
        {
            _currentProjectilePrefab = weaponUpgrade.ProjectilePrefab;
        }

        if (weaponUpgrade.ProjectileDamage > 0)
        {
            _currentProjectileDamage = weaponUpgrade.ProjectileDamage;
        }

        if (weaponUpgrade.ProjectileOrigins.Count > 0)
        {
            _currentProjectileOrigins = weaponUpgrade.ProjectileOrigins;
        }
    }

    public WeaponUpgrade? GetNextUpgrade()
    {
        var nextUpgradeIndex = _currentUpgrade + 1;

        if (nextUpgradeIndex < _weaponUpgrades.Count)
        {
            return _weaponUpgrades[nextUpgradeIndex];
        }

        return null;
    }
}

[Serializable]
public struct WeaponUpgrade
{
    public int NumProjectiles;
    public float TimeBetweenProjectiles;
    public float WeaponFireTime;
    public float Cooldown;
    public GameObject ProjectilePrefab;
    public int ProjectileDamage;
    public List<Vector2> ProjectileOrigins;
}
