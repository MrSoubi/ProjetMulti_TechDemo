using System.Collections;
using UnityEngine;

public class Harvester : MonoBehaviour
{
    [SerializeField] private float harvestTime = 2f; // Temps n�cessaire pour r�colter une ressource
    private ResourceNode currentNode; // R�f�rence au n�ud de ressources � proximit�
    private bool isHarvesting = false; // Indique si une r�colte est en cours
    private bool isInputActive = false; // Indique si l'input est maintenu

    private void OnTriggerEnter2D(Collider2D other)
    {
        // V�rifie si le joueur entre dans la zone d'un n�ud de ressources
        ResourceNode node = other.GetComponent<ResourceNode>();
        if (node != null)
        {
            currentNode = node;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // V�rifie si le joueur quitte la zone d'un n�ud de ressources
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
        isInputActive = false; // L'input est rel�ch�
        isHarvesting = false;
        StopAllCoroutines(); // Stoppe imm�diatement la r�colte
    }

    private IEnumerator HarvestCoroutine()
    {
        isHarvesting = true;
        while (currentNode != null && isInputActive)
        {
            yield return new WaitForSeconds(harvestTime);
            if (currentNode != null && isInputActive) // V�rifie que l'input est toujours actif
            {
                currentNode.CollectResource();
            }
        }
        isHarvesting = false;
    }
}
