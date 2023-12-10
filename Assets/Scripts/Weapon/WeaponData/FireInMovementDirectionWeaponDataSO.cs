using ScriptableObjectArchitecture;
using UnityEngine;

[CreateAssetMenu(fileName = "New WeaponData", menuName = "Custom/Fire in Movement Direction WeaponData")]
public class FireInMovementDirectionWeaponDataSO : WeaponDataSO
{
    [SerializeField]
    private Vector2Variable _movementDirection;

    [SerializeField]
    private int _spawnDistanceFromOrigin = 1;

    private Vector2 _lastNonZeroMovementDirection = Vector2.up;

    public override int SpawnProjectiles(SpawnFunction spawnFunction, Transform spawnerTransform, Transform projectileParentTransform)
    {

        Vector2 randomOrigin = GetRandomOrigin();

        Vector2 spawnPosition = new Vector2(spawnerTransform.position.x + randomOrigin.x,
            spawnerTransform.position.y + randomOrigin.y);

        Vector2 direction = _lastNonZeroMovementDirection.normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        Debug.Log("Spawning projectile with rotation: " + rotation);

        GameObject obj = spawnFunction(spawnPosition, rotation, projectileParentTransform.transform);


        return 1;
    }

    protected override Vector2 GetRandomOrigin()
    {
        if (_movementDirection.Value.magnitude > 0.2f)
        {
            _lastNonZeroMovementDirection = _movementDirection.Value;
        }

        return _lastNonZeroMovementDirection.normalized * _spawnDistanceFromOrigin;
    }


}
