using UnityEngine;
using System;

public class Hitbox : MonoBehaviour
{
	[SerializeField] private float damage = 10f;
	public event Action OnHit;

	private void OnTriggerEnter2D(Collider2D other)
	{
		Hurtbox hurtbox = other.GetComponent<Hurtbox>();
		if (hurtbox != null)
		{
			hurtbox.ReceiveHit(damage);
			OnHit?.Invoke(); // Notifie le projectile qu'il doit se désactiver
		}
	}
}
