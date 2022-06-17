using System;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerInputReader))]
public class Player : MonoBehaviour, IChangePoints
{
    [SerializeField] private Vector3 _startingPosition = Vector3.zero;
    private PlayerInputReader _input;
    private PlayerMovement _playerMovement;
    private PlayerAttack _playerAttack;
    private PlayerHealth _playerHealth;
    private StatManager _playerStatManager;
    private ComponentManager _componentManager;

    [Header("Debug")] [SerializeField] private WeaponSO _debugWeapon;


    public int PointsOnAction { get; } = -50;
    public static event Action<int> OnPointsAction;


    private void Awake()
    {
        _input = GetComponent<PlayerInputReader>();
        if (_input == null)
            Debug.LogError("The player input reader is null");

        _playerMovement = GetComponent<PlayerMovement>();
        if (_playerMovement == null)
            Debug.LogError("The movement controller is null");

        _playerAttack = GetComponent<PlayerAttack>();
        if (_playerAttack == null)
            Debug.LogError("The player attack component is null");

        _playerStatManager = GetComponent<StatManager>();
        if (_playerStatManager == null)
            Debug.LogError("The player stat manager is null");

        _componentManager = GetComponent<ComponentManager>();
        if (_componentManager == null)
            Debug.LogError("The player component manager is null");
    }

    void Start()
    {
        _playerMovement.Teleport(_startingPosition);
    }

    [ContextMenu("Change Weapon")]
    public void ChangeWeapon()
    {
        _playerAttack.ChangeWeapon(_debugWeapon);
    }

    public void ChangeWeapon(WeaponSO weapon)
    {
        _playerAttack.ChangeWeapon(weapon);
    }


    public void AdjustStat(StatModifier statModifier)
    {
        _playerStatManager.ModifyStat(statModifier);
    }

    public void AddAttachable(Attachable attachable)
    {
        _componentManager.Attach(attachable);
    }

    public void Die()
    {
        OnPointsAction?.Invoke(PointsOnAction);

        Debug.Log("Player Died");
        GameStateManager.Instance.SetGameOver();
        Destroy(gameObject);
    }
}