using UnityEngine;

public class DownwardsMover : EnemyMovementBase
{
    private float _respawnHeight;

    protected override void Initialize()
    {
        base.Initialize();
        _respawnHeight = MovementConstraints.YMaxRange;
    }

    // private void Update()
    // {
    //     Move();
    // }

    public override void Move()
    {
        var direction = (_moveDirection * MoveSpeed * Time.deltaTime);
        transform.Translate(direction);
    }

    
    public void Respawn() => transform.position = CreateNewRandomSpawnPosition();

    private Vector3 CreateNewRandomSpawnPosition()
    {
        var x = Random.Range(MovementConstraints.XMinRange, MovementConstraints.XMaxRange);
        var y = _respawnHeight;
        var z = transform.position.z;
        return new Vector3(x, y, z);
    }
}
