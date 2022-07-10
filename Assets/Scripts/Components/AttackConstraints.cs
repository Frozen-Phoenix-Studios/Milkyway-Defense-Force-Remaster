﻿using UnityEngine;

public class AttackConstraints : Constraint
{
    public bool IsWithinConstraints()
    {
        var position = transform.position;
        Debug.Log($"Position {position}");
        if
        (
            position.y >= YMinRange
            && position.y <= YMaxRange
            && position.x >= XMinRange
            && position.x <= XMaxRange
        ) 
            return true;
        return false;
    }
}