using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    private HealthComponent health;

    private void Awake()
    {
        health = GetComponentInParent<HealthComponent>();
    }

    public void ReceiveHit(int damage)
    {
        if (health != null)
        {
            health.TakeDamage(damage);
        }
    }
}
