using UnityEngine;

public class EnemyMovement : EnemyMovementBase
{
    public override void Move()
    {
        if (!CanMove)
        {
            Debug.Log($"{transform.name} can't move");
            return;
        }

        var direction = (_moveDirection * MoveSpeed * Time.deltaTime);
        transform.Translate(direction);
    }

}