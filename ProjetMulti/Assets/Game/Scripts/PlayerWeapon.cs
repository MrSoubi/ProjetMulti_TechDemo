using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
	[SerializeField] private Transform firePoint; // Point d’apparition du projectile
	[SerializeField] private float fireRate = 0.2f; // Cadence de tir

	private float fireCooldown;

	public GameObject projectilePrefab;

	private void Update()
	{
		if (fireCooldown > 0)
			fireCooldown -= Time.deltaTime;
	}

	public void Fire()
	{
		if (fireCooldown > 0)
			return;

		GameObject projectile = Instantiate(projectilePrefab);
		projectile.transform.position = firePoint.position;
		projectile.transform.rotation = firePoint.rotation;

		fireCooldown = fireRate;
	}
}
