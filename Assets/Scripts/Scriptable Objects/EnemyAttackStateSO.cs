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

    public override IEnumerator Enter(Enemy enemy)
    {
        yield return new WaitForSeconds(_timeBeforeAttackDelay);
        enemy.StartAttack();
        yield return new WaitForSeconds(_attackDuration);
        enemy.StopAttack();
        yield return new WaitForSeconds(_timeAfterAttackDelay);
        enemy.SwitchToState(_onCompleteSwitchToState);
    }
}