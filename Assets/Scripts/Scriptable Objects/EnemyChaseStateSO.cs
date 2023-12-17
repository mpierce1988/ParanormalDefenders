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

    public override IEnumerator Enter(Enemy enemy)
    {
        enemy.NavMeshAgent.isStopped = false;
        enemy.NavMeshAgent.SetDestination(new Vector3(_targetPosition.Value.x, _targetPosition.Value.y, 0));

        yield return null;
    }

    public override IEnumerator Tick(Enemy enemy)
    {
        if (enemy != null)
        {
            if (((Vector3)_targetPosition.Value - enemy.gameObject.transform.position).magnitude < _stoppingDistance)
            {
                // switch to new state
                enemy.SwitchToState(_onStopSwitchToState);
            }
            else
            {
                enemy.NavMeshAgent.SetDestination((Vector3)_targetPosition.Value);
            }

            yield return null;
        }

    }

    public override IEnumerator Exit(Enemy enemy)
    {
        enemy.NavMeshAgent.isStopped = true;

        yield return null;
    }
}
