using UnityEngine;
using ScriptableObjectArchitecture;

public class PlayerMovement : MonoBehaviour, IMovement
{
    [SerializeField]
    private FloatVariable _movementSpeed;

    public Vector2 GetNextPosition(Vector2 currentPosition, Vector2 movementVector, float timeInterval)
    {
        return currentPosition
            + (movementVector * _movementSpeed.Value * timeInterval);
    }
}
