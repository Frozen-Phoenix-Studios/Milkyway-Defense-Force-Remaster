using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerInputReader))]
public class Player : MonoBehaviour
{
    [SerializeField] private Vector3 _startingPosition = Vector3.zero;
    private PlayerInputReader _input;
    private PlayerMovement _playerMovement;

    private void Awake()
    {
        _input = GetComponent<PlayerInputReader>();
        if (_input == null)
            Debug.LogError("The player input reader is null");

        _playerMovement = GetComponent<PlayerMovement>();
        if (_playerMovement == null)
            Debug.LogError("The movement controller is null");
    }

    void Start()
    {
        _playerMovement.Teleport(_startingPosition);
    }
}