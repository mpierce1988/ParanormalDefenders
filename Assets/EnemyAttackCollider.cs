using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackCollider : MonoBehaviour
{
    [SerializeField]
    private int _attackDamage = 5;

    private bool _isEnabled = false;

    private void OnEnable()
    {
        _isEnabled = true;
    }

    private void OnDisable()
    {
        _isEnabled = false;
    }

    public void Toggle(bool state)
    {
        gameObject.SetActive(state);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isEnabled)
        {
            return;
        }

        Debug.Log("Enemy Attack Triggered...");

        ITakeDamage takeDamage = collision.gameObject.GetComponent<ITakeDamage>();

        if (takeDamage != null)
        {
            takeDamage.TakeDamage(_attackDamage);
        }
    }
}
