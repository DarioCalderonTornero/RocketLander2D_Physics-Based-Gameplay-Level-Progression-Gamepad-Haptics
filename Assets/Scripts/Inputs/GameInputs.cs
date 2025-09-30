using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInputs : MonoBehaviour
{
    public static GameInputs Instance { get; private set; }

    public event EventHandler OnPauseGame;
    public event EventHandler OnBackPauseGame;

    private MyInputActions inputActions;
    private PlayerInput playerInput;

    [HideInInspector] public string currentControlScheme;

    public enum InputMode
    {
        Controller,
        KeyboardMouse
    }

    private void Awake()
    {
        Instance = this;
        inputActions = new MyInputActions();
        playerInput = GetComponent<PlayerInput>();

        playerInput.onControlsChanged += PlayerInput_onControlsChanged;
        inputActions.Player.PauseGame.performed += PauseGame_performed;
        inputActions.Player.BackGamePause.performed += BackGamePause_performed;
    }

    private void BackGamePause_performed(InputAction.CallbackContext obj)
    {
        OnBackPauseGame?.Invoke(this, EventArgs.Empty);
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void PauseGame_performed(InputAction.CallbackContext obj)
    {
        OnPauseGame?.Invoke(this, EventArgs.Empty); 
    }

    private void PlayerInput_onControlsChanged(PlayerInput input)
    {
        Debug.Log("Device connected : " + input.currentControlScheme);
        currentControlScheme = input.currentControlScheme;
    } 
    
    public bool IsUpActionPressed()
    {
        return inputActions.Player.Up.IsPressed();
    }

    public bool IsLeftActionPressed()
    {
        if (GetCurrentInputMode() == InputMode.KeyboardMouse)
        {
            return inputActions.Player.Left.IsPressed();
        }

        return false;
    }
    public bool IsRightActionPressed()
    {
        if (GetCurrentInputMode() == InputMode.KeyboardMouse)
        {
            return inputActions.Player.Right.IsPressed();
        }

        return false;
    }
    

    public Vector2 GetPlayerMovement()
    {
        return inputActions.Player.Movement.ReadValue<Vector2>();
    }


    public InputMode GetCurrentInputMode()
    {
        if (currentControlScheme == "Gamepad")
        {
            return InputMode.Controller;
        }

        return InputMode.KeyboardMouse;
    }

    private void OnDisable()
    {
        inputActions.Disable();
        playerInput.onControlsChanged -= PlayerInput_onControlsChanged;
        inputActions.Player.PauseGame.performed -= PauseGame_performed;
    }

    public bool IsGamePadConnected()
    {
        return GameInputs.Instance != null && GetCurrentInputMode() == InputMode.Controller;      
    }
}
