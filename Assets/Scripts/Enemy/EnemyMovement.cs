using System;
using UnityEngine;

public class EnemyMovement : MonoBehaviour, IMove
{
    [SerializeField] private float _moveSpeed = 4.0f;
    [SerializeField] private Vector3 _moveDirection;
    public MovementConstraints Constraints { get; private set; }
    public float MoveSpeed => _moveSpeed;

    private void Start()
    {
        Constraints = GetComponent<MovementConstraints>();
        if (Constraints == null)
        {
            Debug.LogError("The movement constraints are null");
        }
    }

    public void Move()
    {
        var direction = (_moveDirection * MoveSpeed * Time.deltaTime);
        transform.Translate(direction);
    }

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