using UnityEngine;
using UnityEngine.Events;

// Code écris normalement puis traité par ChatGPT avec le prompt suivant :
// "En repartant de mon code, clean les noms de variables et revois l'agencement du code, mais ne change rien à la structure et au fonctionnement. N'essaye pas d'améliorer le code."


public class PlayerInput : MonoBehaviour
{
	[SerializeField] private KeyCode keyUp;
	[SerializeField] private KeyCode keyDown;
	[SerializeField] private KeyCode keyLeft;
	[SerializeField] private KeyCode keyRight;
	[SerializeField] private KeyCode keyFire;
	[SerializeField] private KeyCode keyDash;
	[SerializeField] private KeyCode keyInteract;

	[HideInInspector] public UnityEvent onMoveUp;
	[HideInInspector] public UnityEvent onMoveDown;
	[HideInInspector] public UnityEvent onMoveLeft;
	[HideInInspector] public UnityEvent onMoveRight;
	[HideInInspector] public UnityEvent onDash;
	[HideInInspector] public UnityEvent onFire;
	[HideInInspector] public UnityEvent onInteract;

	private void Update()
	{
		CheckMovementInputs();

		if (Input.GetKeyDown(keyFire))
			onFire.Invoke();

		if (Input.GetKey(keyDash))
			onDash.Invoke();

		if (Input.GetKey(keyInteract))
			onInteract.Invoke();
	}

	private void CheckMovementInputs()
	{
		if (Input.GetKey(keyUp))
			onMoveUp.Invoke();

		if (Input.GetKey(keyDown))
			onMoveDown.Invoke();

		if (Input.GetKey(keyLeft))
			onMoveLeft.Invoke();

		if (Input.GetKey(keyRight))
			onMoveRight.Invoke();
	}
}
