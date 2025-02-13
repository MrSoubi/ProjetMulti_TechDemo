using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeapon : MonoBehaviour
{
	[SerializeField] private Transform firePoint; // Point d’apparition du projectile
	[SerializeField] private float fireRate = 0.2f; // Cadence de tir

	PlayerInput playerInput;

	private float fireCooldown;
	private bool isFiring;

    [SerializeField] GameObject projectilePrefab;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
	{
		if (fireCooldown > 0)
			fireCooldown -= Time.deltaTime;

		if (!isFiring)
			return;

        if (fireCooldown > 0)
            return;

        GameObject projectile = Instantiate(projectilePrefab);
        projectile.transform.position = firePoint.position;
        projectile.transform.rotation = firePoint.rotation;

        fireCooldown = fireRate;
    }

	public void Fire(InputAction.CallbackContext context)
	{
        if (!IsThisPlayer(context)) return;

        if (context.performed)
		{
			isFiring = true;
		}

		if (context.canceled)
		{
			isFiring = false;
		}
	}

    private bool IsThisPlayer(InputAction.CallbackContext context)
    {
        return context.control.device == playerInput.devices[0];
    }
}
