using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler: MonoBehaviour
{ 
    public static event Action OnTap;
    private InputActions _actions;

    private void Awake()
    {
        _actions = new InputActions();
        _actions.Enable();
    }

    private void Start()
    {
        _actions.Player.Tap.performed += OnTapPerformed;
    }

    private void OnDestroy()
    {
        _actions.Player.Tap.performed -= OnTapPerformed;
        _actions.Disable();
    }

    private void OnTapPerformed(InputAction.CallbackContext context)
    {
        OnTap?.Invoke();
    }
}
