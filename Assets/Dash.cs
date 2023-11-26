using System.Collections;
using System.Collections.Generic;
using ScriptableObjectArchitecture;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Dash : MonoBehaviour
{
    [SerializeField]
    private Vector2Variable _movement;
    [SerializeField]
    private FloatVariable _dashAmount;
    [SerializeField]
    private FloatVariable _dashTime;
    [SerializeField]
    private BoolVariable _dashInput;
    [SerializeField]
    private BoolVariable _isDashing;
    [SerializeField]
    private FloatVariable _dashCooldown;
    [SerializeField]
    private BoolVariable _canDash;

    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _dashInput.AddListener(PerformDash);
    }

    private void OnDisable()
    {
        _dashInput.RemoveListener(PerformDash);
    }

    void PerformDash(bool isPerforming)
    {

        if (!isPerforming || !_canDash.Value)
        {
            // !isPerforming is when dash button is released
            // !CanDash means we are either dashing or in cooldown
            return;
        }

        StartCoroutine(DashWait(_dashTime));


    }

    IEnumerator DashWait(float timeToWait)
    {
        _isDashing.BaseValue = true;
        _isDashing.Raise();
        _canDash.BaseValue = false;
        _canDash.Raise();

        _rigidbody2D.velocity = Vector2.zero;

        Vector2 pushVector = _movement.Value * _dashAmount.Value;

        _rigidbody2D.AddForce(pushVector, ForceMode2D.Impulse);

        yield return new WaitForSeconds(timeToWait);


        _isDashing.BaseValue = false;
        _isDashing.Raise();

        StartCoroutine(DashCooldown(_dashCooldown));
    }

    IEnumerator DashCooldown(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);

        _canDash.BaseValue = true;
        _canDash.Raise();
    }
}
