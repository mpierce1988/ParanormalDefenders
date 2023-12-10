using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Upgrade Event", menuName = "Custom/New Upgrade Event")]
public class UpgradeGameEventSO : ScriptableObject
{
    public UnityEvent<UpgradeOption> OnUpgrade;

    public void AddListener(UnityAction<UpgradeOption> call)
    {
        OnUpgrade.AddListener(call);
    }

    public void RemoveListener(UnityAction<UpgradeOption> call)
    {
        OnUpgrade.RemoveListener(call);
    }

    public void Invoke(UpgradeOption option)
    {
        OnUpgrade.Invoke(option);
    }
}
