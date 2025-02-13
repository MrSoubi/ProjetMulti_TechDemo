using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Harvester : MonoBehaviour
{
    [SerializeField] private float harvestTime = 2f; // Temps nécessaire pour récolter une ressource
    private ResourceNode currentNode; // Référence au nœud de ressources à proximité
    private bool isHarvesting = false; // Indique si une récolte est en cours

    [HideInInspector]
    public UnityEvent onHarvestFinished;

    private void OnTriggerEnter(Collider other)
    {
        // Vérifie si le joueur entre dans la zone d'un nœud de ressources
        ResourceNode node = other.GetComponent<ResourceNode>();
        if (node != null)
        {
            currentNode = node;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Vérifie si le joueur quitte la zone d'un nœud de ressources
        if (currentNode != null && other.gameObject == currentNode.gameObject)
        {
            StopHarvesting();
        }
    }

    public void StartHarvesting()
    {
        if (currentNode != null && !isHarvesting)
        {
            StartCoroutine(HarvestCoroutine());
        }
    }

    public void StopHarvesting()
    {
        isHarvesting = false;
        StopAllCoroutines(); // Stoppe immédiatement la récolte
    }

    private IEnumerator HarvestCoroutine()
    {
        isHarvesting = true;
        while (currentNode != null)
        {
            yield return new WaitForSeconds(harvestTime);
            if (currentNode != null)
            {
                currentNode.CollectResource();
            }
        }
        isHarvesting = false;
        onHarvestFinished?.Invoke();
    }
}