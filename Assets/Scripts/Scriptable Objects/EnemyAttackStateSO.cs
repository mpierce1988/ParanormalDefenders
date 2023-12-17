using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Attack State", menuName = "Enemy States/Attack State")]
public class EnemyAttackStateSO : EnemyStateSO
{
    [SerializeField]
    private float _timeBeforeAttackDelay = 0.25f;
    [SerializeField]
    private float _attackDuration = 1f;
    [SerializeField]
    private float _timeAfterAttackDelay = 0.5f;
    [SerializeField]
    private string _onCompleteSwitchToState = "Chase";

    public override IEnumerator Enter()
    {
        yield return new WaitForSeconds(_timeAfterAttackDelay);
        _enemy.StartAttack();
        yield return new WaitForSeconds(_attackDuration);
        _enemy.StopAttack();
        yield return new WaitForSeconds(_timeAfterAttackDelay);
        _enemy.SwitchToState(_onCompleteSwitchToState);
    }
}