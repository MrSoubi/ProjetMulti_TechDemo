using UnityEngine;

public class PlayerSpawnManager : MonoBehaviour
{
    public Transform initialSpawnTransform;
    public GameObject harvesterPrefab, protectorPrefab;
    public RSE_PlayerSpawn playerSpawn;

    void Start()
    {
        GameObject harvester = Instantiate(harvesterPrefab, initialSpawnTransform.position + Vector3.right, Quaternion.identity);
        GameObject protector = Instantiate(protectorPrefab, initialSpawnTransform.position + Vector3.left, Quaternion.identity);

        playerSpawn.onTrigger?.Invoke(harvester.transform);
        playerSpawn.onTrigger?.Invoke(protector.transform);
    }
}
