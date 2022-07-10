using System;
using UnityEngine;

public class EnemyCircleMovement : EnemyMovementBase
{
    private Transform _parent;
    [SerializeField] private float _frequency;
    [SerializeField] private float _amplitude;

    protected override void Initialize()
    {
        base.Initialize();
        _parent = transform.parent;
    }

    public override void Move()
    {
        
        float x = MathF.Cos(Time.time * _frequency) * _amplitude;
        float y = MathF.Sin(Time.time * _frequency) * _amplitude;
        float z = 0;

        var circleOrbit = new Vector3(x, y, z);
        transform.localPosition = circleOrbit;
    }


}

