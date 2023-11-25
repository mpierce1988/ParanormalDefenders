using UnityEngine;

public class PlayerMovement : MonoBehaviour, IMovement
{
    [SerializeField]
    private float _movementSpeed = 30f;

    public Vector2 GetNextPosition(Vector2 currentPosition, Vector2 movementVector, float timeInterval)
    {
        return currentPosition
            + (movementVector * _movementSpeed * timeInterval);
    }
}
