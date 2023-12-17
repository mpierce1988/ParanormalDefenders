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

    public virtual IEnumerator Enter()
    {
        yield return null;
    }

    public virtual IEnumerator Tick()
    {
        yield return null;
    }

    public virtual IEnumerator Exit()
    {
        yield return null;
    }

    public virtual void Reset()
    {
        _enemy = null;
    }
}
