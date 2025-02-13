using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public RSO_PlayerPosition harvesterPosition, protectorPosition;

    [SerializeField] float harassementDistanceThreshold = 5;
    [SerializeField] NavMeshAgent agent;

    private void OnEnable()
    {
        harvesterPosition.onValueChanged += OnPlayerMoved;
        protectorPosition.onValueChanged += OnPlayerMoved;
    }

    private void OnDisable()
    {
        harvesterPosition.onValueChanged -= OnPlayerMoved;
        protectorPosition.onValueChanged -= OnPlayerMoved;
    }

    private void OnPlayerMoved(Vector3 newPosition)
    {
        float distance = (newPosition - transform.position).magnitude;

        if (distance < harassementDistanceThreshold)
        {
            agent.destination = newPosition;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        HealthComponent healthComponent = collision.gameObject.GetComponent<HealthComponent>();

        if (healthComponent != null && (collision.gameObject.CompareTag("Protector") || collision.gameObject.CompareTag("Harvester")))
        {
            healthComponent.TakeDamage(1);
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, harassementDistanceThreshold);
    }
}