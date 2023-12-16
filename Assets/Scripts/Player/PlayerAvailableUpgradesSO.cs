using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Player Available Upgrades", menuName = "Custom/Player Available Upgrades")]
public class PlayerAvailableUpgradesSO : ScriptableObject
{
    [SerializeField]
    private List<UpgradeOption> _upgradeOptions;

    public List<UpgradeOption> UpgradeOptions => _upgradeOptions;

    public UnityEvent OnUpgradeOptionsChange;

    public void SetUpgradeOptions(List<UpgradeOption> options)
    {
        _upgradeOptions = options;
        OnUpgradeOptionsChange.Invoke();
    }

    public void AddListener(UnityAction call)
    {
        OnUpgradeOptionsChange.AddListener(call);
    }

    public void RemoveListener(UnityAction call)
    {
        OnUpgradeOptionsChange.RemoveListener(call);
    }

    internal void ClearOptions()
    {
        _upgradeOptions.Clear();
    }
}
