using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIUpgradeController : MonoBehaviour
{
    [SerializeField]
    private PlayerAvailableUpgradesSO _playerAvailableUpgrades;

    [SerializeField]
    private GameObject _upgradeButtonPrefab;

    [SerializeField]
    private GameObject _upgradeButtonsParent;

    private List<GameObject> _instantiatedButtons = new();

    private void OnEnable()
    {
        // Delete any existing buttons
        DeleteInstantiatedButtons();

        // create buttons for each upgrade option
        foreach (UpgradeOption option in _playerAvailableUpgrades.UpgradeOptions)
        {
            // instantiate button prefab
            GameObject btn = Instantiate(_upgradeButtonPrefab, _upgradeButtonsParent.transform);
            btn.SetActive(false);
            UIUpgradeButton upgradeButton = btn.GetComponent<UIUpgradeButton>();
            upgradeButton.SetUpgradeOption(option);
            btn.SetActive(true);
        }

    }

    private void OnDisable()
    {
        DeleteInstantiatedButtons();
    }

    private void DeleteInstantiatedButtons()
    {
        List<GameObject> buttonsToDestroy = _instantiatedButtons;

        foreach (GameObject btn in buttonsToDestroy)
        {
            _instantiatedButtons.Remove(btn);
            Destroy(btn.gameObject);
        }
    }
}
