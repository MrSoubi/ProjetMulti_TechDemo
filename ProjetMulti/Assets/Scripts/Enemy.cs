using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public RSO_PlayerPosition harvesterPosition, protectorPosition;
    public float harassementDistanceThreshold = 5;
    public NavMeshAgent agent;

    private void OnEnable()
    {
        harvesterPosition.onValueChanged += OnHarvesterMoved;
        protectorPosition.onValueChanged += OnProtectorMoved;
    }

    private void OnDisable()
    {
        harvesterPosition.onValueChanged -= OnHarvesterMoved;
        protectorPosition.onValueChanged -= OnProtectorMoved;
    }

    private void OnHarvesterMoved(Vector3 newPosition)
    {
        float distance = (newPosition - transform.position).magnitude;

        if (distance < harassementDistanceThreshold)
        {
            agent.destination = newPosition;
        }
    }

    private void OnProtectorMoved(Vector3 newPosition)
    {
        float distance = (newPosition - transform.position).magnitude;

        if (distance < harassementDistanceThreshold)
        {
            agent.destination = newPosition;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, harassementDistanceThreshold);
    }
}
