using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolsManager : MonoBehaviour
{
    [SerializeField]
    private ObjectPoolsDictionarySO _objectPoolDictionary;

    [SerializeField]
    private List<ObjectPoolsData> _objectPoolsData;

    private void Awake()
    {
        _objectPoolDictionary.Clear();
        _objectPoolDictionary.Initialize(_objectPoolsData);
    }
}
