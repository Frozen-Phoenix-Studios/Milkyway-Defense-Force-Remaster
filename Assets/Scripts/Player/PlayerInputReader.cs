using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputReader : MonoBehaviour
{
    public Vector2 move;
    public bool shoot;
    
    public void OnMove(InputValue value)
    {
        MoveInput(value.Get<Vector2>());

    }

    public void MoveInput(Vector2 moveDirection)
    {
        move = moveDirection.normalized;
    }
}
