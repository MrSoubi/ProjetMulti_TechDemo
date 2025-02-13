using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    [SerializeField] private int resourceCount = 5; // Nombre de ressources disponibles

    public RSO_ResourceCount RSO_resourceCount;

    public void CollectResource()
    {
        if (resourceCount <= 0)
            return;

        resourceCount--;
        RSO_resourceCount.Value++;
        Debug.Log("Ressource récoltée ! Restantes: " + resourceCount);

        if (resourceCount <= 0)
        {
            Destroy(gameObject); // Supprime le nœud quand il est épuisé
        }
    }
}
