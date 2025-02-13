using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Harvester : MonoBehaviour
{
    [SerializeField] float harvestTime = 2f; // Temps nécessaire pour récolter une ressource

    // Un seul node n'est référencé à la fois
    // Dans les niveaux les nodes ne seront jamais assez proches pour que cela pose problème
    ResourceNode currentNode; // Référence au nœud de ressources à proximité

    bool isHarvesting = false; // Indique si une récolte est en cours

    [HideInInspector]
    public UnityEvent onHarvestFinished; // A remplacer par un RSE (ou utiliser le RSO ResourceCount ?)

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