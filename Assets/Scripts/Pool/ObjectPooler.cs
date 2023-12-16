using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField]
    private GameObject _objectToPool;

    [SerializeField]
    private int _defaultCapacity = 20;

    [SerializeField]
    private int _maxSize = 30;

    [SerializeField]
    private Transform _inactiveObjectsParent;

    [SerializeField]
    private Transform _activeObjectsParent;

    private ObjectPool<GameObject> _pool;

    private void OnEnable()
    {
        if (_objectToPool == null)
        {
            return;
        }
        _pool = CreatePool();
    }

    private void OnDisable()
    {
        if (_pool != null)
        {
            _pool.Dispose();
        }
    }

    public void Initialize(GameObject objectToPool, int defaultCapacity,
        int maxSize, Transform activeObjectsParent = null, Transform inactiveObjectsParent = null)
    {
        _objectToPool = objectToPool;
        _defaultCapacity = defaultCapacity;
        _maxSize = maxSize;
        _activeObjectsParent = activeObjectsParent;
        _inactiveObjectsParent = inactiveObjectsParent;

        if (_pool != null)
        {
            _pool.Dispose();
        }

        _pool = CreatePool();
    }

    public GameObject GetItem()
    {
        return _pool.Get();
    }

    public void ReturnItem(GameObject gameObject)
    {
        _pool.Release(gameObject);
    }

    private ObjectPool<GameObject> CreatePool()
    {
        return new ObjectPool<GameObject>(
            createFunc: CreateGameObject,
            actionOnGet: ActivateGameObject,
            actionOnRelease: DeactivateGameObject,
            actionOnDestroy: DestroyGameObject,
            collectionCheck: false,
            defaultCapacity: _defaultCapacity,
            maxSize: _maxSize
            );
    }

    private void DestroyGameObject(GameObject gameObject)
    {
        Destroy(gameObject);
    }

    private void DeactivateGameObject(GameObject gameObject)
    {
        gameObject.SetActive(false);
        if (_inactiveObjectsParent != null)
        {
            gameObject.transform.parent = _inactiveObjectsParent;
        }
        else
        {
            gameObject.transform.parent = this.transform;
        }
        gameObject.transform.position = Vector3.zero;
        gameObject.transform.rotation = Quaternion.identity;
        gameObject.transform.localScale = Vector3.one;
    }

    private void ActivateGameObject(GameObject gameObject)
    {
        gameObject.SetActive(true);
        if (_activeObjectsParent == null)
        {
            gameObject.transform.parent = null;
        }
        else
        {
            gameObject.transform.parent = _activeObjectsParent;
        }
    }

    private GameObject CreateGameObject()
    {
        GameObject result = Instantiate(_objectToPool);
        IPoolable poolable = result.GetComponent<IPoolable>();
        poolable.Initialize(ReturnItem);
        return result;
    }
}
