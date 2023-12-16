using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugGiveWeaponOnTrigger : MonoBehaviour
{
    [SerializeField]
    private WeaponDataSO _weaponToGive;
    [SerializeField]
    private PlayerInventory _playerInventory;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController playerController = collision.GetComponent<PlayerController>();

        if (playerController != null)
        {
            _playerInventory.AddWeapon(_weaponToGive);

            Destroy(this.gameObject);
        }
    }
}
