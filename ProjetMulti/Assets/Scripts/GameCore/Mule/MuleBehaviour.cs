using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MuleBehaviour : MonoBehaviour
{
    public List<ResourceNode> resources;
    public NavMeshAgent agent;
    public Harvester harvester;

    public int resourceIndex = 0;

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
        resourceIndex++;

        if (resourceIndex >= resources.Count)
        {
            return;
        }

        agent.isStopped = false;
        agent.destination = resources[resourceIndex].transform.position;

        Debug.Log("go to next node");
    }
}