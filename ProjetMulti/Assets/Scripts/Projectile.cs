using UnityEngine;

// Ecris par ChatGPT
public class Projectile : MonoBehaviour
{
	[SerializeField] private float speed = 10f;
	[SerializeField] private float lifetime = 3f;

	public void Launch()
	{
		GetComponent<Rigidbody2D>().linearVelocity = transform.up * speed;
		Invoke(nameof(Deactivate), lifetime);
	}

	private void Deactivate()
	{
		gameObject.SetActive(false);
	}
}
