using System;
using UnityEngine;

public interface IPlayerInput
{
    Vector2 CurrentMovement { get; }

    event Action<Vector2> OnMovementChange;
}