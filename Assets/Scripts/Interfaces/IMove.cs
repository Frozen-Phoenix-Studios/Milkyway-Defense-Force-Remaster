public interface IMove
{
    MovementConstraints Constraints { get; }
    float MoveSpeed { get; }
    void Move();
}
