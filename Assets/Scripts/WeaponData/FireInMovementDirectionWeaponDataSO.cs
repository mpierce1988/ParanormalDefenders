using ScriptableObjectArchitecture;
using UnityEngine;

[CreateAssetMenu(fileName = "New WeaponData", menuName = "Custom/Fire in Movement Direction WeaponData")]
public class FireInMovementDirectionWeaponDataSO : WeaponDataSO
{
    [SerializeField]
    private Vector2Variable _movementDirection;

    [SerializeField]
    private int _spawnDistanceFromOrigin = 1;

    protected override Vector2 GetRandomOrigin()
    {
        return ((Vector2)_movementDirection.BaseValue).normalized * _spawnDistanceFromOrigin;
    }


}
