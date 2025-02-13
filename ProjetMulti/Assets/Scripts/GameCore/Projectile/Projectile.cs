using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] float lifetime = 3f;
    [SerializeField] Rigidbody rb;
    [SerializeField] int damage = 1;

    [SerializeField] LayerMask wallMask;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        var healthComponent = collision.gameObject.GetComponent<HealthComponent>();
        if (healthComponent != null)
        {
            healthComponent.TakeDamage(damage);
        }

        Deactivate();
    }

    private void OnEnable()
    {
        Invoke(nameof(Deactivate), lifetime);
    }

    public void Start()
    {
        rb.linearVelocity = transform.forward * speed;
    }

    private void Deactivate()
    {
        CancelInvoke(); // Annule la destruction automatique si désactivé avant le lifetime
        Destroy(gameObject);
    }
}
