using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Harvester : MonoBehaviour
{
    [SerializeField] float harvestTime = 2f; // Temps n�cessaire pour r�colter une ressource

    // Un seul node n'est r�f�renc� � la fois
    // Dans les niveaux les nodes ne seront jamais assez proches pour que cela pose probl�me
    ResourceNode currentNode; // R�f�rence au n�ud de ressources � proximit�

    bool isHarvesting = false; // Indique si une r�colte est en cours

    [HideInInspector]
    public UnityEvent onHarvestFinished; // A remplacer par un RSE (ou utiliser le RSO ResourceCount ?)

    private void OnTriggerEnter(Collider other)
    {
        // V�rifie si le joueur entre dans la zone d'un n�ud de ressources
        ResourceNode node = other.GetComponent<ResourceNode>();
        if (node != null)
        {
            currentNode = node;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // V�rifie si le joueur quitte la zone d'un n�ud de ressources
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
        StopAllCoroutines(); // Stoppe imm�diatement la r�colte
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