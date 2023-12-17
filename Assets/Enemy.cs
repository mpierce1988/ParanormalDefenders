using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    [SerializeField]
    private List<EnemyStateData> _enemyStates;
    [SerializeField]
    private float _ticksPerSecond = 2f;

    public NavMeshAgent NavMeshAgent => _navMeshAgent;

    private NavMeshAgent _navMeshAgent;
    private Dictionary<string, EnemyStateSO> _enemyStateDictionary = new();
    private EnemyStateSO _currentState;
    private float _timeBetweenTicks;
    private bool _isActive;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        // disable rotation on navmeshagent
        NavMeshAgent.updateRotation = false;
        NavMeshAgent.updateUpAxis = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        string startState = "";
        // reset and initialize each state
        foreach (EnemyStateData esd in _enemyStates)
        {
            esd.EnemyStateSO.Reset();
            esd.EnemyStateSO.SetEnemy(this);
            if (esd.IsStartState)
            {
                startState = esd.Name;
            }

            _enemyStateDictionary.Add(esd.Name, esd.EnemyStateSO);
        }

        SwitchToState(startState);

        // start ticks
        _timeBetweenTicks = 1f / _ticksPerSecond;
        StartTicking();


    }

    private void StartTicking()
    {
        _isActive = true;
        StartCoroutine(TickingCoroutine());
    }

    private IEnumerator TickingCoroutine()
    {
        while (_isActive)
        {
            yield return new WaitForSeconds(_timeBetweenTicks);
            if (_currentState != null)
            {
                StartCoroutine(_currentState.Tick());
            }
        }
    }

    internal void StartAttack()
    {
        Debug.Log("Starting attack...");
    }

    internal void StopAttack()
    {
        Debug.Log("Ending attack...");
    }

    internal void SwitchToState(string onCompleteSwitchToState)
    {
        if (!_enemyStateDictionary.TryGetValue(onCompleteSwitchToState, out var nextState))
        {
            nextState = null;
            return;
        }

        if (_currentState != null)
        {
            StartCoroutine(_currentState.Exit());
        }

        _currentState = nextState;

        StartCoroutine(_currentState.Enter());
    }
}

[Serializable]
public struct EnemyStateData
{
    public EnemyStateSO EnemyStateSO;
    public string Name;
    public bool IsStartState;
}