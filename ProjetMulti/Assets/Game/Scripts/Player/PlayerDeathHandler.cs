using UnityEngine;

public class PlayerDeathHandler : MonoBehaviour
{
    public void OnDeath()
    {
        Destroy(gameObject);
    }
}
