using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInputReader _input;
    private StatManager _statManager;
    [SerializeField] private Stat _movementSpeed;
    [SerializeField] private float _currentSpeed = 5.0f;
    private MovementConstraints _constraints;

    private void Start()
    {
        _input = GetComponent<PlayerInputReader>();
        if (_input == null)
            Debug.LogError("The Input is null");

        _statManager = GetComponent<StatManager>();
        if (_statManager == null)
            Debug.LogError("The stat manager is null");

        _constraints = GetComponent<MovementConstraints>();
        if (_constraints == null)
            Debug.LogError("The movement constraints are null");

        _movementSpeed = _statManager.BindStat(_movementSpeed);
        _currentSpeed = _movementSpeed.GetCurrentValue();
        _movementSpeed.OnValueChanged += HandleMovementSpeedChanged;
    }

    private void HandleMovementSpeedChanged(float newValue)
    {
        _currentSpeed = newValue;
    }

    void Update()
    {
        transform.Translate((_input.move * Time.deltaTime * _currentSpeed));
    }

    public void Teleport(Vector3 newPosition)
    {
        transform.position = newPosition;
    }

    public void TeleportLeft()
    {
        var x = _constraints.XMinRange;
        var pos = transform.position;
        var newPosition = new Vector2(x, pos.y);
        transform.position = newPosition;
    }

    public void TeleportRight()
    {
        var x = _constraints.XMaxRange;
        var pos = transform.position;
        var newPosition = new Vector2(x, pos.y);
        transform.position = newPosition;
    }

    public void TeleportUp()
    {
        var y = _constraints.YMaxRange;
        var pos = transform.position;
        var newPosition = new Vector2(pos.x, y);
        transform.position = newPosition;
    }

    public void TeleportDown()
    {
        var y = _constraints.YMinRange;
        var pos = transform.position;
        var newPosition = new Vector2(pos.x, y);
        transform.position = newPosition;
    }
}

