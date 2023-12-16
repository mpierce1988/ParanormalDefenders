using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
        Refresh();

    }

    public void Refresh()
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

            _instantiatedButtons.Add(btn);
        }

        // set up button navigation and currently selected button
        for (int i = 0; i < _instantiatedButtons.Count; i++)
        {
            if (i == 0)
            {
                EventSystem.current.SetSelectedGameObject(_instantiatedButtons[i]);
                EventSystem.current.firstSelectedGameObject = _instantiatedButtons[i];
            }

            Navigation navigation = new Navigation();
            navigation.mode = Navigation.Mode.Explicit;
            // set select left
            int selectLeftIndex = -1;
            if (i == 0)
            {
                selectLeftIndex = _instantiatedButtons.Count - 1;
            }
            else
            {
                // set left selected to the item on the left
                selectLeftIndex = i - 1;
            }
            navigation.selectOnLeft = _instantiatedButtons[selectLeftIndex].GetComponent<Button>();

            // set select right
            int selectRightIndex = -1;
            if (i == _instantiatedButtons.Count - 1)
            {
                // set select left to first item
                selectRightIndex = 0;
            }
            else
            {
                // set select right to the next item
                selectRightIndex = i + 1;
            }
            navigation.selectOnRight = _instantiatedButtons[selectRightIndex].GetComponent<Button>();

            _instantiatedButtons[i].GetComponent<Button>().navigation = navigation;
        }
    }

    private void OnDisable()
    {
        DeleteInstantiatedButtons();
    }

    private void DeleteInstantiatedButtons()
    {
        List<GameObject> buttonsToDestroy = new List<GameObject>();
        buttonsToDestroy.AddRange(_instantiatedButtons);

        foreach (GameObject btn in buttonsToDestroy)
        {
            _instantiatedButtons.Remove(btn);
            Destroy(btn.gameObject);
        }
    }
}
