using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputReader : MonoBehaviour
{
    public static event Action OnRestartPressed;
    public static event Action<bool> OnTurboChanged;
    public static event Action OnQuitPressed;
    private PlayerControls _controls;

    public Vector2 move;
    public bool shoot;

    private void Awake()
    {
        _controls = new PlayerControls();
        _controls.Player.Enable();
        _controls.Player.Quit.performed += delegate(InputAction.CallbackContext context) { OnQuitPressed?.Invoke(); };
        _controls.GameManager.Restart.performed += delegate(InputAction.CallbackContext context)
        {
            OnRestartPressed?.Invoke();
        };
        _controls.Player.Turbo.performed += delegate(InputAction.CallbackContext context)
        {
            OnTurboChanged?.Invoke(true);
        };
        _controls.Player.Turbo.canceled += delegate(InputAction.CallbackContext context)
        {
            OnTurboChanged?.Invoke(false);
        };

    }

    private void OnEnable()
    {
        GameStateManager.OnGameOver += SwitchControls;
    }

    private void OnDisable()
    {
        GameStateManager.OnGameOver -= SwitchControls;
    }


    private void Update()
    {
        move = _controls.Player.Move.ReadValue<Vector2>().normalized;
        shoot = _controls.Player.Shoot.WasPerformedThisFrame();
    }

    private void SwitchControls(bool gameOver)
    {
        if (!gameOver) return;
        _controls.Player.Disable();
        _controls.GameManager.Enable();
    }
    

}

    
