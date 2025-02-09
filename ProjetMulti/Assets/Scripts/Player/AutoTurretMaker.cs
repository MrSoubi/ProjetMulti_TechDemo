using System.Collections;
using UnityEngine;

public class AutoTurretMaker : MonoBehaviour
{
    public RSO_ResourceCount resourceCount;
    public int cost;
    public GameObject turretPrefab;
    public GameObject spawnPosition;

    public void SpawnTurret()
    {
        if (resourceCount.Value >= cost)
        {
            Instantiate(turretPrefab, spawnPosition.transform.position, spawnPosition.transform.rotation);
            resourceCount.Value -= cost;
        }
    }
}