using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _movementSpeed = 30f;

    private IPlayerInput _playerInput;
    private IMovement _playerMovement;
    private Rigidbody2D _rigidbody2D;

    private Vector2 _movementVector;

    private void Awake()
    {
        _playerInput = GetComponent<IPlayerInput>();
        _playerMovement = GetComponent<IMovement>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void OnEnable()
    {
        _playerInput.OnMovementChange += UpdateMovementVector;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _rigidbody2D.MovePosition(_playerMovement.GetNextPosition(
            _rigidbody2D.position, _movementVector, Time.fixedDeltaTime));
    }

    private void OnDisable()
    {
        _playerInput.OnMovementChange -= UpdateMovementVector;
    }

    private void UpdateMovementVector(Vector2 obj)
    {
        _movementVector = obj;
    }
}
