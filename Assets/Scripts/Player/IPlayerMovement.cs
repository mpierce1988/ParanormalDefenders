using UnityEngine;

public interface IMovement
{
    Vector2 GetNextPosition(Vector2 currentPosition, Vector2 movementVector, float timeInterval);
}