using UnityEngine;
using UnityEngine.Events;

public class HealthComponent : MonoBehaviour
{
	[SerializeField] private float maxHealth = 100f;
	private float currentHealth;

	public UnityEvent onDeath;

	private void Awake()
	{
		currentHealth = maxHealth;
	}

	public void TakeDamage(float damage)
	{
		currentHealth -= damage;
		if (currentHealth <= 0)
		{
			currentHealth = 0;
			onDeath?.Invoke();
			Destroy(gameObject); // Optionnel : peut être remplacé par un event
		}
	}

	public float GetCurrentHealth() => currentHealth;
}
