using UnityEngine;

public class EnemyMovement : MonoBehaviour, IMove
{
    [SerializeField] private Stat _movementSpeed;

    [SerializeField] private float _currentMoveSpeed = 4.0f;

    [SerializeField] private Vector3 _moveDirection;
    private StatManager _statManager;
    public MovementConstraints Constraints { get; private set; }
    public float MoveSpeed => _currentMoveSpeed;
    public bool CanMove { get; private set; }

    private void OnDisable() => _movementSpeed.OnValueChanged -= HandleMovementSpeedChanged;

    private void Start()
    {
        Constraints = GetComponent<MovementConstraints>();
        if (Constraints == null)
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


    public void Move()
    {
        if (!CanMove)
            return;

        var direction = (_moveDirection * MoveSpeed * Time.deltaTime);
        transform.Translate(direction);
    }

    public void StopMovement() => CanMove = false;

    public void BlockLeft()
    {
        var x = Constraints.XMinRange;
        var pos = transform.position;
        var newPosition = new Vector2(x, pos.y);
        transform.position = newPosition;
    }

    public void BlockRight()
    {
        var x = Constraints.XMaxRange;
        var pos = transform.position;
        var newPosition = new Vector2(x, pos.y);
        transform.position = newPosition;
    }

    public void BlockUp()
    {
        var y = Constraints.YMaxRange;
        var pos = transform.position;
        var newPosition = new Vector2(pos.x, y);
        transform.position = newPosition;
    }

    public void BlockDown()
    {
        var y = Constraints.YMinRange;
        var pos = transform.position;
        var newPosition = new Vector2(pos.x, y);
        transform.position = newPosition;
    }
}