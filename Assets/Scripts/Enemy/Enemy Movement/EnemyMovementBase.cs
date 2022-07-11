using UnityEngine;

public abstract class EnemyMovementBase : MonoBehaviour, IMove
{
    [SerializeField] private Stat _movementSpeed;
    [SerializeField] private float _currentMoveSpeed = 4.0f;
    [SerializeField] protected Vector3 _moveDirection;
    private StatManager _statManager;
    public MovementConstraints MovementConstraints { get; private set; }
    public float MoveSpeed => _currentMoveSpeed;
    public bool CanMove { get; protected set; }
    private void OnDisable() => _movementSpeed.OnValueChanged -= HandleMovementSpeedChanged;

    private void Start()
    {
        Initialize();
    }

    protected virtual void Initialize()
    {
        MovementConstraints = GetComponent<MovementConstraints>();
        if (MovementConstraints == null)
            Debug.LogError("The movement constraints are null");

        _statManager = GetComponent<StatManager>();
        if (_statManager == null)
            Debug.LogError("The enemy stat manager is null");

        CanMove = true;
        _movementSpeed = _statManager.BindStat(_movementSpeed);
        _movementSpeed.OnValueChanged += HandleMovementSpeedChanged;
        _currentMoveSpeed = _movementSpeed.GetCurrentModifiedValue();
    }

    private void HandleMovementSpeedChanged(float value) => _currentMoveSpeed = value;
    public abstract void Move();

    public void BlockLeft()
    {
        var x = MovementConstraints.XMinRange;
        var pos = transform.position;
        var newPosition = new Vector2(x, pos.y);
        transform.position = newPosition;
    }

    public void BlockRight()
    {
        var x = MovementConstraints.XMaxRange;
        var pos = transform.position;
        var newPosition = new Vector2(x, pos.y);
        transform.position = newPosition;
    }

    public void BlockUp()
    {
        var y = MovementConstraints.YMaxRange;
        var pos = transform.position;
        var newPosition = new Vector2(pos.x, y);
        transform.position = newPosition;
    }

    public void BlockDown()
    {
        var y = MovementConstraints.YMinRange;
        var pos = transform.position;
        var newPosition = new Vector2(pos.x, y);
        transform.position = newPosition;
    }
}