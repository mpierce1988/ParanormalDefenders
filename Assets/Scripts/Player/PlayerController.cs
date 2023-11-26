using System.Collections;
using System.Collections.Generic;
using ScriptableObjectArchitecture;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private ScriptableObjectArchitecture.Vector2Variable _movementInput;

    [SerializeField]
    private BoolVariable _isDashing;

    // private IPlayerInput _playerInput;
    private IMovement _playerMovement;
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        // _playerInput = GetComponent<IPlayerInput>();
        _playerMovement = GetComponent<IMovement>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_isDashing.Value == true)
        {
            // do not handle movement, we are dashing
            return;
        }

        // Debug.Log("Current Input: " + _movementInput.Value);
        _rigidbody2D.MovePosition(_playerMovement.GetNextPosition(
            _rigidbody2D.position, _movementInput.Value, Time.fixedDeltaTime));
    }

}
