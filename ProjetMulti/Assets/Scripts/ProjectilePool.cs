using System.Collections.Generic;
using UnityEngine;

// Ecris par ChatGPT, modifié manuellement pour utiliser les RSE.

public class ProjectilePool : MonoBehaviour
{
	[SerializeField] private Projectile projectilePrefab;
	[SerializeField] private int poolSize = 10;

	public RSE_SpawnProjectile spawnProjectile;

	private Queue<Projectile> pool = new Queue<Projectile>();

    private void OnEnable()
    {
		spawnProjectile.TriggerEvent += SpawnProjectile;
    }

    private void Start()
	{
		for (int i = 0; i < poolSize; i++)
		{
			Projectile newProjectile = Instantiate(projectilePrefab);
			newProjectile.gameObject.SetActive(false);
			pool.Enqueue(newProjectile);
		}
	}

	private void SpawnProjectile(Vector2 position, Quaternion direction)
	{
		Projectile projectile = GetProjectile();

		projectile.transform.position = position;
		projectile.transform.rotation = direction;
		projectile.Launch();
	}

	public Projectile GetProjectile()
	{
		if (pool.Count > 0)
		{
			Projectile projectile = pool.Dequeue();
			projectile.gameObject.SetActive(true);
			return projectile;
		}
		return null;
	}

	public void ReturnProjectile(Projectile projectile)
	{
		projectile.gameObject.SetActive(false);
		pool.Enqueue(projectile);
	}
}
