using System.Collections;
using UnityEngine;

public class Harvester : MonoBehaviour
{
    [SerializeField] private float harvestTime = 2f; // Temps nécessaire pour récolter une ressource
    private ResourceNode currentNode; // Référence au nœud de ressources à proximité
    private bool isHarvesting = false; // Indique si une récolte est en cours
    private bool isInputActive = false; // Indique si l'input est maintenu

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Vérifie si le joueur entre dans la zone d'un nœud de ressources
        ResourceNode node = other.GetComponent<ResourceNode>();
        if (node != null)
        {
            currentNode = node;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Vérifie si le joueur quitte la zone d'un nœud de ressources
        if (currentNode != null && other.gameObject == currentNode.gameObject)
        {
            StopHarvesting();
        }
    }

    public void StartHarvesting()
    {
        isInputActive = true; // L'input est maintenu
        if (currentNode != null && !isHarvesting)
        {
            StartCoroutine(HarvestCoroutine());
        }
    }

    public void StopHarvesting()
    {
        isInputActive = false; // L'input est relâché
        isHarvesting = false;
        StopAllCoroutines(); // Stoppe immédiatement la récolte
    }

    private IEnumerator HarvestCoroutine()
    {
        isHarvesting = true;
        while (currentNode != null && isInputActive)
        {
            yield return new WaitForSeconds(harvestTime);
            if (currentNode != null && isInputActive) // Vérifie que l'input est toujours actif
            {
                currentNode.CollectResource();
            }
        }
        isHarvesting = false;
    }
}
