public interface IMove
{
    MovementConstraints Constraints { get; }
    float MoveSpeed { get; }
    bool CanMove { get; }
    void Move();
    void StopMovement();
}
