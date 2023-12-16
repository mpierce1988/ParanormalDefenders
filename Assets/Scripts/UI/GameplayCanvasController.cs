using System.Collections;
using System.Collections.Generic;
using ScriptableObjectArchitecture;
using UnityEngine;

public class GameplayCanvasController : MonoBehaviour
{
    [SerializeField]
    private GameManagerStateSO _gameManagerState;
    [SerializeField]
    private GameObject _gameplayPanel;
    [SerializeField]
    private GameObject _pausePanel;
    [SerializeField]
    private GameObject _upgradesPanel;

    private GameManager _gm;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        _gameManagerState.OnGameManagerStateChange.AddListener(HandleGameManagerStateChange);

        HandleGameManagerStateChange(_gameManagerState.GameManagerState);
    }

    private void HandleGameManagerStateChange(GameManagerState state)
    {
        if (state is PlayState)
        {
            _gameplayPanel.SetActive(true);
            _pausePanel.SetActive(false);
            _upgradesPanel.SetActive(false);
        }
        else if (state is PauseState)
        {
            _pausePanel.SetActive(true);
            _gameplayPanel.SetActive(false);
            _upgradesPanel.SetActive(false);
        }
        else if (state is UpgradeState)
        {
            _upgradesPanel.SetActive(true);
            _pausePanel.SetActive(false);

            UIUpgradeController uiUpgradeController = _upgradesPanel.GetComponent<UIUpgradeController>();
            uiUpgradeController.Refresh();
        }
    }

}
