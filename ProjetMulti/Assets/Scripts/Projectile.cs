using UnityEditor;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	[SerializeField] private float speed = 10f;
	[SerializeField] private float lifetime = 3f;
	private Rigidbody2D rb;
	public int damage = 1;

	public LayerMask wallMask;
	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
		if (collision.gameObject.GetComponent<HealthComponent>() != null)
		{
			collision.gameObject.GetComponent<HealthComponent>().TakeDamage(damage);
		}

		Deactivate();
    }

    private void OnEnable()
	{
		Invoke(nameof(Deactivate), lifetime);
	}

	public void Start()
	{
		rb.linearVelocity = transform.up * speed;
	}

	private void Deactivate()
	{
		CancelInvoke(); // Annule la destruction automatique si désactivé avant le lifetime
		Destroy(gameObject);
	}
}
