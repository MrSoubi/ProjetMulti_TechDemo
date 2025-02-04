using UnityEngine;
using System.Collections.Generic;

public class AutoTurret : MonoBehaviour
{
    [Header("Turret Settings")]
    public float detectionRange = 5f;
    public float fireRate = 1f; // Secondes entre chaque tir
    public int damage = 10;
    public float lifetime = 10f;
    public float rotationSpeed = 180f; // Degrés par seconde

    private float fireCooldown = 0f;
    private float timer = 0f;
    private Enemy target;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= lifetime)
        {
            Destroy(gameObject);
            return;
        }

        fireCooldown -= Time.deltaTime;

        target = FindClosestEnemy();
        if (target != null)
        {
            RotateTowardsTarget(target.transform);

            if (fireCooldown <= 0f && IsFacingTarget(target.transform))
            {
                Shoot(target);
                fireCooldown = fireRate;
            }
        }
    }

    private Enemy FindClosestEnemy()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectionRange);
        Enemy closestEnemy = null;
        float closestDistance = float.MaxValue;

        foreach (Collider2D hit in hits)
        {
            Enemy enemy = hit.GetComponent<Enemy>();
            if (enemy != null)
            {
                float distance = Vector2.Distance(transform.position, hit.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = enemy;
                }
            }
        }

        return closestEnemy;
    }

    private void RotateTowardsTarget(Transform targetTransform)
    {
        Vector2 direction = (targetTransform.position - transform.position).normalized;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float currentAngle = transform.eulerAngles.z;
        float newAngle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, rotationSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0f, 0f, newAngle);
    }

    private bool IsFacingTarget(Transform targetTransform)
    {
        Vector2 directionToTarget = (targetTransform.position - transform.position).normalized;
        Vector2 turretForward = transform.right;
        float angleDifference = Vector2.Angle(turretForward, directionToTarget);
        return angleDifference < 5f; // Tolérance d'alignement
    }

    private void Shoot(Enemy target)
    {
        if (target == null) return;

        HealthComponent health = target.GetComponent<HealthComponent>();
        if (health != null)
        {
            health.TakeDamage(damage);
        }

        Debug.DrawLine(transform.position, target.transform.position, Color.red, 0.1f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}

