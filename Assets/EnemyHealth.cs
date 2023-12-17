using UnityEngine;

public class EnemyHealth : MonoBehaviour, ITakeDamage
{
    [SerializeField]
    private int _startingHealth = 1;

    [SerializeField]
    private ObjectPoolsDictionarySO _objectPools;

    [SerializeField]
    private string _objectToDropOnDie = "XP";

    private int _currentHealth;
    private IPoolable _pool;

    void Awake()
    {
        _pool = GetComponent<IPoolable>();
    }

    void OnEnable()
    {
        _currentHealth = _startingHealth;

    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            // die
            Die();
        }
    }

    private void Die()
    {
        // spawn item
        var itemObjectPooler = _objectPools.GetObjectPooler(_objectToDropOnDie);

        if (itemObjectPooler != null)
        {
            var item = itemObjectPooler.GetItem();
            item.transform.position = this.transform.position;
            item.SetActive(true);
        }
        if (_pool != null)
        {
            _pool.ReturnToPool();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
