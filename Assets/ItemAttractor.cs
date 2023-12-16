using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ScriptableObjectArchitecture;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class ItemAttractor : MonoBehaviour
{
    [SerializeField]
    private FloatVariable _attractionRadius;
    [SerializeField]
    private FloatVariable _attractionForce;
    [SerializeField]
    private FloatVariable _smoothTime;


    private CircleCollider2D _itemAttractionCollider;
    private List<Rigidbody2D> _itemsToAttractRBs = new List<Rigidbody2D>();
    private Dictionary<Rigidbody2D, Vector2> _velocities = new Dictionary<Rigidbody2D, Vector2>();

    private void Awake()
    {
        _itemAttractionCollider = GetComponent<CircleCollider2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        // set attraction radius
        _itemAttractionCollider.radius = _attractionRadius;
    }

    private void FixedUpdate()
    {
        if (_itemsToAttractRBs.Count == 0)
        {
            return;
        }

        // check all items in list to see if they still exist        
        List<Rigidbody2D> rbToRemove = _itemsToAttractRBs.Where(rb => rb == null).ToList();

        rbToRemove.ForEach(rb =>
        {
            _itemsToAttractRBs.Remove(rb);
            _velocities.Remove(rb);
        });


        _itemsToAttractRBs.ForEach(rb =>
        {
            rb.MovePosition(CalculateNextPosition(rb));
        });

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Pickup pickup = collision.GetComponent<Pickup>();

        if (pickup != null)
        {
            _itemsToAttractRBs.Add(collision.attachedRigidbody);
            _velocities[collision.attachedRigidbody] = Vector2.zero;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Pickup pickup = collision.GetComponent<Pickup>();

        if (pickup != null)
        {
            _itemsToAttractRBs.Remove(collision.attachedRigidbody);
            _velocities.Remove(collision.attachedRigidbody);
        }
    }

    private Vector2 CalculateNextPosition(Rigidbody2D rigidbody)
    {
        Vector2 targetPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 currentVelocity = Vector2.zero;
        return Vector2.SmoothDamp(rigidbody.position, targetPosition,
            ref currentVelocity, _smoothTime, _attractionForce);

    }
}
