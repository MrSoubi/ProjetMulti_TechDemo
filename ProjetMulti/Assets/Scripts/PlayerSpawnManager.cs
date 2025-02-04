using UnityEngine;

public class PlayerSpawnManager : MonoBehaviour
{
    public Transform initialSpawnTransform;
    public GameObject harvesterPrefab, protectorPrefab;
    public RSE_PlayerSpawn playerSpawn;

    void Start()
    {
        GameObject harvester = Instantiate(harvesterPrefab, initialSpawnTransform.position + Vector3.right, initialSpawnTransform.rotation);
        GameObject protector = Instantiate(protectorPrefab, initialSpawnTransform.position + Vector3.left, initialSpawnTransform.rotation);

        playerSpawn.onTrigger?.Invoke(harvester.transform);
        playerSpawn.onTrigger?.Invoke(protector.transform);
    }
}
