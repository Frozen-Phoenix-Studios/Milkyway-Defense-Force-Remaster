using UnityEngine;

public class EnemyMovement : MonoBehaviour, IMove
{
    [SerializeField] private float _moveSpeed = 4.0f;
    [SerializeField] private Vector3 _moveDirection;
    public float MoveSpeed => _moveSpeed;

    public void Move()
    {
        var direction = (_moveDirection * MoveSpeed * Time.deltaTime);
        transform.Translate(direction);
    }
}