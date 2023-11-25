using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using ScriptableObjectArchitecture;

public class UnityPlayerInput : MonoBehaviour
{

    [SerializeField]
    private Vector2Variable _movement;
    [SerializeField]
    private BoolVariable _dash;
    [SerializeField]
    private BoolVariable _placeTower;
    [SerializeField]
    private BoolVariable _removeTower;
    [SerializeField]
    private BoolVariable _towerMode;
    [SerializeField]
    private FloatVariable _towerSelect;
    [SerializeField]
    private BoolVariable _pause;


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

        _playerActions.Gameplay.Dash.performed += Dash_performed;
        _playerActions.Gameplay.Dash.canceled += Dash_performed;

        _playerActions.Gameplay.PlaceTower.performed += PlaceTower_performed;
        _playerActions.Gameplay.PlaceTower.canceled += PlaceTower_performed;

        _playerActions.Gameplay.RemoveTower.performed += RemoveTower_performed;
        _playerActions.Gameplay.RemoveTower.canceled += RemoveTower_performed;

        _playerActions.Gameplay.TowerMode.performed += TowerMode_performed;
        _playerActions.Gameplay.TowerMode.canceled += TowerMode_performed;

        _playerActions.Gameplay.TowerSelection.performed += TowerSelection_performed;
        _playerActions.Gameplay.TowerSelection.canceled += TowerSelection_performed;

        _playerActions.Gameplay.Pause.performed += Pause_performed;
        _playerActions.Gameplay.Pause.canceled += Pause_performed;
    }

    private void Pause_performed(InputAction.CallbackContext obj)
    {
        _pause.BaseValue = obj.ReadValue<float>() > 0 ? true : false;
        _pause.Raise();
    }

    private void TowerSelection_performed(InputAction.CallbackContext obj)
    {
        _towerSelect.BaseValue = obj.ReadValue<float>();
        _towerSelect.Raise();
    }

    private void TowerMode_performed(InputAction.CallbackContext obj)
    {
        _towerMode.BaseValue = obj.ReadValue<float>() > 0 ? true : false;
        _towerMode.Raise();
    }

    private void RemoveTower_performed(InputAction.CallbackContext obj)
    {
        _removeTower.BaseValue = obj.ReadValue<float>() > 0 ? true : false;
        _removeTower.Raise();
    }

    private void PlaceTower_performed(InputAction.CallbackContext obj)
    {
        _placeTower.BaseValue = obj.ReadValue<float>() > 0 ? true : false;
        _placeTower.Raise();
    }

    private void Dash_performed(InputAction.CallbackContext obj)
    {
        _dash.BaseValue = obj.ReadValue<float>() > 0 ? true : false;
        _dash.Raise();
    }

    private void Movement_performed(InputAction.CallbackContext obj)
    {
        _movement.BaseValue = obj.ReadValue<Vector2>();
        _movement.Raise();
    }

    private void OnDestroy()
    {
        _playerActions.Gameplay.Movement.performed -= Movement_performed;
        _playerActions.Gameplay.Movement.canceled -= Movement_performed;

        _playerActions.Gameplay.Dash.performed -= Dash_performed;
        _playerActions.Gameplay.Dash.canceled -= Dash_performed;

        _playerActions.Gameplay.PlaceTower.performed -= PlaceTower_performed;
        _playerActions.Gameplay.PlaceTower.canceled -= PlaceTower_performed;

        _playerActions.Gameplay.RemoveTower.performed -= RemoveTower_performed;
        _playerActions.Gameplay.RemoveTower.canceled -= RemoveTower_performed;

        _playerActions.Gameplay.TowerMode.performed -= TowerMode_performed;
        _playerActions.Gameplay.TowerMode.canceled -= TowerMode_performed;

        _playerActions.Gameplay.TowerSelection.performed -= TowerSelection_performed;
        _playerActions.Gameplay.TowerSelection.canceled -= TowerSelection_performed;

        _playerActions.Gameplay.Pause.performed -= Pause_performed;
        _playerActions.Gameplay.Pause.canceled -= Pause_performed;

        _playerActions.Disable();
    }


}
