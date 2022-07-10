using System;

public interface IMove
{
    MovementConstraints MovementConstraints { get; }
    float MoveSpeed { get; }
    bool CanMove { get; }
    void Move();
}
