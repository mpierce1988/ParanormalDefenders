using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UnityPlayerInput : MonoBehaviour, IPlayerInput
{

    public event Action<Vector2> OnMovementChange;
    public Vector2 CurrentMovement => _currentMovement;

    private Vector2 _currentMovement;
    private PlayerActions _playerActions;

    private void Awake()
    {
        _playerActions = new PlayerActions();
    }
    // Start is called before the first frame update
    void Start()
    {
        _playerActions.Enable();
        _playerActions.Gameplay.Movement.performed += Movement_performed;
        _playerActions.Gameplay.Movement.canceled += Movement_performed;
    }

    private void OnDestroy()
    {
        _playerActions.Gameplay.Movement.performed -= Movement_performed;
        _playerActions.Gameplay.Movement.canceled -= Movement_performed;
        _playerActions.Disable();
    }

    private void Movement_performed(InputAction.CallbackContext obj)
    {
        _currentMovement = obj.ReadValue<Vector2>();
        OnMovementChange?.Invoke(_currentMovement);
    }


}
