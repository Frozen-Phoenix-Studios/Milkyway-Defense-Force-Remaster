using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputReader : MonoBehaviour
{
    private PlayerControls _controls;
    public static event Action OnRestartPressed;


    public Vector2 move;
    public bool shoot;
    
    private void Awake()
    {
        _controls = new PlayerControls();
        _controls.Player.Enable();
        _controls.GameMaager.Restart.performed += delegate(InputAction.CallbackContext context)
        {
            OnRestartPressed?.Invoke();
        };
    }

    private void OnEnable()
    {
        GameOverManager.OnGameOver += SwitchControls;
    }

    private void OnDisable()
    {
        GameOverManager.OnGameOver -= SwitchControls;
    }


    private void Update()
    {
        move = _controls.Player.Move.ReadValue<Vector2>().normalized;
        shoot = _controls.Player.Shoot.WasPerformedThisFrame();
    }

    private void SwitchControls()
    {
        _controls.Player.Disable();
        _controls.GameMaager.Enable();
    }

}

    
