using UnityEngine;

public class PlayerDeathHandler : MonoBehaviour
{
    public void OnDeath()
    {
        Debug.Log(gameObject.name + " died");
        Destroy(gameObject);
    }
}
