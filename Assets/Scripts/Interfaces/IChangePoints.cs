using System;

public interface IChangePoints
{
    public static event Action<int> OnPointsAction;
    public int PointsOnAction { get; }

}