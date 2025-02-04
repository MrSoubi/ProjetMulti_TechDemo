using UnityEngine;

public class PositionInterpolator : MonoBehaviour
{
    [SerializeField] private Transform harvesterTransform;
    [SerializeField] private Transform protectorTransform;
    [Range(0f, 1f)]
    [SerializeField] private float weightHarvester = 0.5f;

    public RSE_PlayerSpawn playerSpawn;

    private void OnEnable()
    {
        playerSpawn.onTrigger += OnPlayerSpawned;
    }

    private void OnDisable()
    {
        playerSpawn.onTrigger -= OnPlayerSpawned;
    }

    private void OnPlayerSpawned(Transform playerTransform)
    {
        if (playerTransform.CompareTag("Harvester"))
        {
            harvesterTransform = playerTransform;
        }
        else if (playerTransform.CompareTag("Protector"))
        {
            protectorTransform = playerTransform;
        }
    }

    void Update()
    {
        if (harvesterTransform == null)
        {
            transform.position = protectorTransform.position;
        }
        else if(protectorTransform == null)
        {
            transform.position = harvesterTransform.position;
        }
        else
        {
            // Calcul de la position interpolée entre pointA et pointB en fonction du poids
            float weightB = 1f - weightHarvester;
            transform.position = (harvesterTransform.position * weightHarvester) + (protectorTransform.position * weightB);
        }
    }
}
