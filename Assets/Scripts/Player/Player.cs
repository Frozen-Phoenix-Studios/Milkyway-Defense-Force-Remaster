using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(PlayerInputReader))]
[RequireComponent(typeof(PlayerInput))]
public class Player : MonoBehaviour
{
    [SerializeField] private Vector3 _startingPosition = Vector3.zero;
    [SerializeField] private PlayerInputReader _input;
    [SerializeField] private Movement _movement;


    private void Awake()
    {
        _input = GetComponent<PlayerInputReader>();
        if (_input == null)
            Debug.LogError("The player input reader is null");

        _movement = GetComponent<Movement>();
        if (_movement == null)
            Debug.LogError("The movement controller is null");
    }

    void Start()
    {
        transform.position = _startingPosition;
    }
    
    
}