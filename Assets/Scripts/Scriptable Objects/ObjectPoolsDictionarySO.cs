using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Object Pools Dictionary", menuName = "Custom/Object Pools Dictionary")]
public class ObjectPoolsDictionarySO : ScriptableObject
{
    [SerializeField]
    private List<ObjectPoolsData> _objectPools = new();

    private Dictionary<string, ObjectPooler> _objectPoolerDictionary = new();


    public void Initialize(List<ObjectPoolsData> objectPools)
    {
        _objectPools = objectPools;

        PopulateDictionaryFromList();
    }

    private void PopulateDictionaryFromList()
    {
        foreach (var op in _objectPools)
        {
            _objectPoolerDictionary.Add(op.Name, op.ObjectPooler);
        }
    }

    public void AddObjectPool(ObjectPoolsData objectPool)
    {
        _objectPools.Add(objectPool);
        _objectPoolerDictionary.Add(objectPool.Name, objectPool.ObjectPooler);
    }

    public ObjectPooler GetObjectPooler(string name)
    {
        if (_objectPoolerDictionary.TryGetValue(name, out ObjectPooler result))
        {
            return result;
        }

        return null;
    }

    public void Clear()
    {
        _objectPools.Clear();
        _objectPoolerDictionary.Clear();
    }
}

[Serializable]
public struct ObjectPoolsData
{
    public string Name;
    public ObjectPooler ObjectPooler;
}
