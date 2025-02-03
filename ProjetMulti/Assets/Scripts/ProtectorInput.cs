using UnityEngine;
using UnityEngine.Events;

public class ProtectorInput : MonoBehaviour
{
	[SerializeField] private KeyCode keyFire;

	[HideInInspector] public UnityEvent onFire;

	private void Update()
	{
		if (Input.GetKey(keyFire))
			onFire.Invoke();
	}
}
