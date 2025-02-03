using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    private HealthComponent health;

    private void Awake()
    {
        health = GetComponentInParent<HealthComponent>();
    }

    public void ReceiveHit(float damage)
    {
        if (health != null)
        {
            health.TakeDamage(damage);
        }
    }
}
