using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerInputReader))]
[RequireComponent(typeof(PlayerInput))]
public class Player : MonoBehaviour
{
    [SerializeField] private Vector3 _startingPosition = Vector3.zero;
    [SerializeField] private PlayerInputReader _input;
    [SerializeField] private PlayerMovement playerMovement;
    
    private void Awake()
    {
        _input = GetComponent<PlayerInputReader>();
        if (_input == null)
            Debug.LogError("The player input reader is null");

        playerMovement = GetComponent<PlayerMovement>();
        if (playerMovement == null)
            Debug.LogError("The movement controller is null");
    }

    void Start()
    {
        transform.position = _startingPosition;
    }
}