using System.Collections;
using ScriptableObjectArchitecture;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Chase State", menuName = "Enemy States/Chase State")]
public class EnemyChaseStateSO : EnemyStateSO
{
    [SerializeField]
    private Vector2Variable _targetPosition;

    [SerializeField]
    private float _stoppingDistance;

    [SerializeField]
    private string _onStopSwitchToState = "Attack";

    public override IEnumerator Enter()
    {
        _enemy.NavMeshAgent.isStopped = false;
        _enemy.NavMeshAgent.SetDestination(new Vector3(_targetPosition.Value.x, _targetPosition.Value.y, 0));

        yield return null;
    }

    public override IEnumerator Tick()
    {
        if (((Vector3)_targetPosition.Value - _enemy.gameObject.transform.position).magnitude < _stoppingDistance)
        {
            // switch to new state
            _enemy.SwitchToState(_onStopSwitchToState);
        }
        else
        {
            _enemy.NavMeshAgent.SetDestination((Vector3)_targetPosition.Value);
        }

        yield return null;
    }

    public override IEnumerator Exit()
    {
        _enemy.NavMeshAgent.isStopped = true;

        yield return null;
    }
}
