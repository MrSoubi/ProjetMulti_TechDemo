using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public RSO_PlayerPosition RSO_Position;

	[SerializeField] private Rigidbody2D rb;
	[SerializeField] private float moveSpeed;
	[SerializeField] private float maxSpeed;

	[SerializeField] private bool allowDash = false;

	[ShowIf("allowDash")]
	[SerializeField] private float dashCooldown = 2f;
	[ShowIf("allowDash")]
	[SerializeField] private AnimationCurve dashCurve;
	

	private int verticalInput, horizontalInput;
	private float dashTimer;
	private bool isDashing;
	private bool dashRequested;
	private Coroutine dashCoroutine;

	private void Start()
	{
		if (allowDash)
		{
            dashCurve.keys[dashCurve.length - 1].value = maxSpeed;
        }
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

		RSO_Position.Value = transform.position;

    }

	public void OnMoveUp() => verticalInput = 1;
	public void OnMoveDown() => verticalInput = -1;
	public void OnMoveLeft() => horizontalInput = -1;
	public void OnMoveRight() => horizontalInput = 1;

	public void OnDashInput()
	{
		if (!allowDash)
			return;

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
