using System.Collections;
using UnityEngine;

// Code écris normalement puis traité par ChatGPT avec le prompt suivant :
// "En repartant de mon code, clean les noms de variables et revois l'agencement du code, mais ne change rien à la structure et au fonctionnement. N'essaye pas d'améliorer le code."

public class PlayerMovement : MonoBehaviour
{
	[SerializeField] private PlayerInput playerInput;
	[SerializeField] private Rigidbody2D rb;
	[SerializeField] private float moveSpeed;
	[SerializeField] private float dashForce = 10f;
	[SerializeField] private float dashCooldown = 2f;
	[SerializeField] private AnimationCurve dashCurve;
	[SerializeField] private float maxSpeed;

	private int verticalInput, horizontalInput;
	private float dashTimer;
	private bool isDashing;
	private bool dashRequested;
	private Coroutine dashCoroutine;

	private void OnEnable()
	{
		playerInput.onMoveUp.AddListener(OnMoveUp);
		playerInput.onMoveDown.AddListener(OnMoveDown);
		playerInput.onMoveLeft.AddListener(OnMoveLeft);
		playerInput.onMoveRight.AddListener(OnMoveRight);
		playerInput.onDash.AddListener(OnDashInput);
	}

	private void OnDisable()
	{
		playerInput.onMoveUp.RemoveListener(OnMoveUp);
		playerInput.onMoveDown.RemoveListener(OnMoveDown);
		playerInput.onMoveLeft.RemoveListener(OnMoveLeft);
		playerInput.onMoveRight.RemoveListener(OnMoveRight);
		playerInput.onDash.RemoveListener(OnDashInput);
	}

	private void Start()
	{
		dashCurve.keys[dashCurve.length - 1].value = maxSpeed;
	}

	private void Update()
	{
		if (dashTimer < dashCooldown)
			dashTimer += Time.deltaTime;
	}

	private void FixedUpdate()
	{
		if (dashRequested)
		{
			dashCoroutine = StartCoroutine(DashCoroutine());
			dashRequested = false;
			dashTimer = 0f;
		}

		if (!isDashing)
		{
			Vector2 moveDirection = new Vector2(horizontalInput, verticalInput).normalized;
			rb.AddForce(moveDirection * moveSpeed);
			rb.linearVelocity = Vector2.ClampMagnitude(rb.linearVelocity, maxSpeed);

			if (rb.linearVelocity.sqrMagnitude > 0.01f)
			{
				float angle = Mathf.Atan2(rb.linearVelocity.y, rb.linearVelocity.x) * Mathf.Rad2Deg - 90;
				rb.rotation = angle;
			}
		}


		horizontalInput = 0;
		verticalInput = 0;
	}

	private void OnMoveUp() => verticalInput = 1;
	private void OnMoveDown() => verticalInput = -1;
	private void OnMoveLeft() => horizontalInput = -1;
	private void OnMoveRight() => horizontalInput = 1;

	private void OnDashInput()
	{
		if (dashTimer >= dashCooldown)
			dashRequested = true;
	}

	private IEnumerator DashCoroutine()
	{
		isDashing = true;
		Vector2 dashDirection = rb.linearVelocity.normalized;
		float elapsedTime = 0f;

		while (elapsedTime <= dashCurve.keys[dashCurve.length - 1].time)
		{
			rb.linearVelocity = dashDirection * dashCurve.Evaluate(dashTimer);
			elapsedTime += Time.deltaTime;
			yield return null;
		}

		isDashing = false;
	}
}
