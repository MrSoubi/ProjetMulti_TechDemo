using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public RSO_PlayerPosition RSO_Position;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float maxSpeed;

    [SerializeField] private bool allowDash = false;

    [ShowIf("allowDash")]
    [SerializeField] private float dashCooldown = 2f;
    [ShowIf("allowDash")]
    [SerializeField] private AnimationCurve dashCurve;

    private float dashTimer;
    private bool isDashing;
    private bool dashRequested;
    private Coroutine dashCoroutine;
    private Vector2 moveInput, lookInput;

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
            Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y).normalized;
            rb.AddForce(moveDirection * moveSpeed, ForceMode.Acceleration);
            rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, maxSpeed);

            if (lookInput.magnitude > 0.2)
            {
                Vector3 lookDirection = new Vector3(lookInput.x, 0, lookInput.y).normalized;
                Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
                rb.rotation = targetRotation;
            }
        }

        moveInput = Vector3.zero;
        lookInput = Vector3.zero;

        RSO_Position.Value = transform.position;
    }

    public void Move(Vector2 direction)
    {
        moveInput = direction;
    }

    public void Look(Vector2 direction)
    {
        lookInput = direction;
    }

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
        Vector3 dashDirection = rb.linearVelocity.normalized;
        float elapsedTime = 0f;

        while (elapsedTime <= dashCurve.keys[dashCurve.length - 1].time)
        {
            rb.linearVelocity = dashDirection * dashCurve.Evaluate(elapsedTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isDashing = false;
    }
}
