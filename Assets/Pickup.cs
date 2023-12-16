using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjectArchitecture;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField]
    private int _amount;

    [SerializeField]
    private IntGameEvent _pickupEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();

        if (player != null)
        {
            _pickupEvent.Raise(_amount);
            DestroySelf();
        }
    }

    private void DestroySelf()
    {
        Destroy(this.gameObject);
    }
}
