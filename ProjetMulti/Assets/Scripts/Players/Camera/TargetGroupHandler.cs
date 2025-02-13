using Unity.Cinemachine;
using UnityEngine;

public class TargetGroupHandler : MonoBehaviour
{
    public RSE_PlayerSpawn playerSpawn;

    [SerializeField] CinemachineTargetGroup targetGroup;

    private void OnEnable()
    {
        playerSpawn.onTrigger += AddTransformToTargetGroup;
    }

    private void OnDisable()
    {
        
    }

    void AddTransformToTargetGroup(Transform transform)
    {
        targetGroup.AddMember(transform, 1, 5);
    }
}
