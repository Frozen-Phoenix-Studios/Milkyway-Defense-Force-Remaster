using UnityEngine;

public class DistanceAttackCondition: MonoBehaviour, IAttackCondition
{
    [SerializeField] private bool _isPrimed;
    public bool IsPrimed => _isPrimed;

    [SerializeField] private Transform _target;
    [SerializeField] private float _maxDistance;
    

    public bool CheckIsMet()
    {
        return PrimeCondition();
    }

    public bool PrimeCondition()
    {
        var distance = Vector3.Distance(transform.position, _target.position);
        _isPrimed = distance <= _maxDistance;
        return _isPrimed;
    }
}