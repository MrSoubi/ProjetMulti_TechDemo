using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MuleBehaviour : MonoBehaviour
{
    public List<ResourceNode> resources; // Pas bon, à changer.
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Harvester harvester;

    private void OnEnable()
    {
        harvester.onHarvestFinished.AddListener(GoToNextNode);
    }

    private void Start()
    {
        agent.destination = resources[0].transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        ResourceNode node = other.GetComponent<ResourceNode>();

        if (node != null)
        {
            agent.isStopped = true;
            harvester.StartHarvesting();
        }
    }

    void GoToNextNode()
    {
        if (resources.Count == 0)
        {
            return;
        }

        resources.RemoveAt(0);

        if (resources.Count > 0)
        {
            agent.isStopped = false;
            agent.destination = resources[0].transform.position;
            Debug.Log("go to next node");
        }
    }
}