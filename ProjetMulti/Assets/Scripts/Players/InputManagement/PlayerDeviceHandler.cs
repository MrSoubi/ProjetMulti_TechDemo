using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDeviceHandler : MonoBehaviour
{
    [SerializeField] float killDelayAfterDeviceLost;

    Coroutine deviceLostCoroutine;

    public void OnDeviceLost(PlayerInput playerInput)
    {
        StartCoroutine(OnDeviceLostCoroutine(playerInput));
    }

    public void OnDeviceRegained(PlayerInput playerInput)
    {
        StopAllCoroutines();
    }

    IEnumerator OnDeviceLostCoroutine(PlayerInput playerInput)
    {
        yield return new WaitForSeconds(killDelayAfterDeviceLost);
        Destroy(playerInput.gameObject);
    }
}
