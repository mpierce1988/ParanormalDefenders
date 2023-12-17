using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateSO : ScriptableObject
{
    protected Enemy _enemy;

    public void SetEnemy(Enemy enemy)
    {
        _enemy = enemy;
    }

    public virtual IEnumerator Enter(Enemy enemy)
    {
        yield return null;
    }

    public virtual IEnumerator Tick(Enemy enemy)
    {
        yield return null;
    }

    public virtual IEnumerator Exit(Enemy enemy)
    {
        yield return null;
    }

    public virtual void Reset()
    {
        _enemy = null;
    }

    public EnemyStateSO() { }

    public EnemyStateSO(EnemyStateSO enemyStateSO)
    {
        _enemy = enemyStateSO._enemy;
    }
}
