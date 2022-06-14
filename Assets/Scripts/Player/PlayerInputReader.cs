using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputReader : MonoBehaviour
{
    private PlayerControls _controls;

    public Vector2 move;
    public bool shoot;
    
    
    
    private void Start()
    {
        _controls = new PlayerControls();
        _controls.Player.Enable();
    }
    

    private void Update()
    {
        move = _controls.Player.Move.ReadValue<Vector2>().normalized;
        shoot = _controls.Player.Shoot.WasPerformedThisFrame();
    }


    
}
