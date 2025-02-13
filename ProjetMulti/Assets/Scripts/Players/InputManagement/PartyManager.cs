using UnityEngine;
using UnityEngine.InputSystem;

public class PartyManager : MonoBehaviour
{
    public RSE_PlayerSpawn playerSpawn;

    [SerializeField] Transform spawnTransform;

    public void OnPlayerJoined(PlayerInput playerInput)
    {
        GameObject newPlayer = playerInput.gameObject;

        newPlayer.transform.position = spawnTransform.position;
        newPlayer.transform.rotation = spawnTransform.rotation;

        // I don't understand why but it's needed to set the position and rotation of the Rigidbody too
        newPlayer.GetComponent<Rigidbody>().position = spawnTransform.position;
        newPlayer.GetComponent<Rigidbody>().rotation = spawnTransform.rotation;

        playerSpawn.onTrigger?.Invoke(newPlayer.transform);

        Debug.Log(newPlayer.name + " joined the game!");
    }

    public void OnPlayerLeft(PlayerInput playerInput)
    {
        Debug.Log(playerInput.gameObject.name + " left the game!");
    }
}
