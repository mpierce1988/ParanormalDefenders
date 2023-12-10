using System.Collections;
using System.Collections.Generic;
using ScriptableObjectArchitecture;
using UnityEngine;

public class DebugUpgradeWeaponOnTrigger : MonoBehaviour
{
    [SerializeField]
    private GameEvent _upgradeWeaponEvent;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();

        if (player != null)
        {
            _upgradeWeaponEvent.Raise();
            Destroy(this.gameObject);
        }
    }
}
