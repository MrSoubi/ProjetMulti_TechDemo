using UnityEngine;

// Ecris par ChatGPT, modifié manuellement pour intégrer les RSE.

public class PlayerWeapon : MonoBehaviour
{
	[SerializeField] private PlayerInput playerInput; // Référence aux entrées du joueur
	[SerializeField] private Transform firePoint; // Point d’apparition du projectile
	[SerializeField] private float fireRate = 0.2f; // Cadence de tir

	private float fireCooldown;

	public RSE_SpawnProjectile spawnProjectile;

	private void OnEnable()
	{
		playerInput.onFire.AddListener(Fire);
	}

	private void OnDisable()
	{
		playerInput.onFire.RemoveListener(Fire);
	}

	private void Update()
	{
		if (fireCooldown > 0)
			fireCooldown -= Time.deltaTime;
	}

	private void Fire()
	{
		if (fireCooldown > 0)
			return;

		spawnProjectile.TriggerEvent(firePoint.position, transform.rotation);

		fireCooldown = fireRate;
	}
}
