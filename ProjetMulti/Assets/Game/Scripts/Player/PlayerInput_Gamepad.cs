using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInput_Gamepad : MonoBehaviour
{
    public UnityEvent<Vector2> OnMove;
    public UnityEvent<Vector2> OnLook;
    public UnityEvent OnShoot;

    public InputAction moveInput;
    public InputAction lookInput;
    public InputAction shootInput;

    bool isShootInputPressed = false;

    private void OnEnable()
    {
        moveInput.performed += ctx => OnMoveInput(ctx);
        moveInput.Enable();
        lookInput.performed += ctx => OnLookInput(ctx);
        lookInput.Enable();

        shootInput.started += ctx => OnShootInputBegin();
        shootInput.canceled += ctx => OnShootInputEnd();
        shootInput.Enable();
    }

    private void OnDisable()
    {
        moveInput.performed -= ctx => OnMoveInput(ctx);
        moveInput.Disable();
        lookInput.performed -= ctx => OnLookInput(ctx);
        lookInput.Disable();

        shootInput.started -= ctx => OnShootInputBegin();
        shootInput.canceled -= ctx => OnShootInputEnd();
        shootInput.Disable();
    }

    private void OnMoveInput(InputAction.CallbackContext ctx)
    {
        OnMove.Invoke(ctx.ReadValue<Vector2>());
        Debug.Log(ctx.ReadValue<Vector2>());
    }

    private void OnLookInput(InputAction.CallbackContext ctx)
    {
        OnLook.Invoke(ctx.ReadValue<Vector2>());
        Debug.Log(ctx.ReadValue<Vector2>());
    }

    private void OnShootInputBegin()
    {
        isShootInputPressed = true;
    }

    private void OnShootInputEnd()
    {
        isShootInputPressed = false;
    }

    private void Update()
    {
        if (isShootInputPressed)
        {
            OnShoot.Invoke();
        }
    }
}
