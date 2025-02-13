using UnityEngine;
using UnityEngine.Events;

public class HealthComponent : MonoBehaviour
{
	[SerializeField] private int maxHealth;

	private int currentHealth;

	public UnityEvent onDeath;

	private void Start()
	{
		currentHealth = maxHealth;
	}

	public void TakeDamage(int damage)
	{
		currentHealth -= damage;

		if (currentHealth <= 0)
		{
			currentHealth = 0;
			onDeath?.Invoke();
		}
	}
}
