using UnityEngine;

public class Projectile : MonoBehaviour
{
	[SerializeField] private float speed = 10f;
	[SerializeField] private float lifetime = 3f;
	private Rigidbody2D rb;
	private Hitbox hitbox;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		hitbox = GetComponent<Hitbox>();

		if (hitbox != null)
		{
			hitbox.OnHit += OnProjectileHit;
		}
	}

	private void OnEnable()
	{
		Invoke(nameof(Deactivate), lifetime);
	}

	public void Launch(Vector2 direction)
	{
		rb.linearVelocity = direction.normalized * speed;
	}

	private void OnProjectileHit()
	{
		Deactivate();
	}

	private void Deactivate()
	{
		CancelInvoke(); // Annule la destruction automatique si d�sactiv� avant le lifetime
		gameObject.SetActive(false); // Compatible avec le pooling
	}
}
