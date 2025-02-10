using UnityEngine;
using System;

public class Hitbox : MonoBehaviour
{
	[SerializeField] private int damage = 10;
	public event Action OnHit;

	private void OnTriggerEnter2D(Collider2D other)
	{
		Hurtbox hurtbox = other.GetComponent<Hurtbox>();
		if (hurtbox != null)
		{
			hurtbox.ReceiveHit(damage);
            OnHit?.Invoke();
        }

        //Destroy(gameObject);
    }
}
