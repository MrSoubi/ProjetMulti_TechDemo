using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    [SerializeField] private int resourceCount = 5; // Nombre de ressources disponibles

    public void CollectResource()
    {
        if (resourceCount > 0)
        {
            resourceCount--;
            Debug.Log("Ressource récoltée ! Restantes: " + resourceCount);

            if (resourceCount <= 0)
            {
                Destroy(gameObject); // Supprime le nœud quand il est épuisé
            }
        }
    }
}
