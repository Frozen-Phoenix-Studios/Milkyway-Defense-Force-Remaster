using UnityEngine;

public class PositionAttackCondition: MonoBehaviour, IAttackCondition
{
    [SerializeField ]private AttackConstraints _constraints;
    private bool _isPrimed;
    public bool IsPrimed => _isPrimed;


    private void Start()
    {
        if (_constraints == null)
        {
            _constraints = GetComponent<AttackConstraints>();
        }
        
        if (_constraints == null)
        {
        }
    }


    public bool CheckIsMet()
    {

        return _isPrimed;
    }

    public bool PrimeCondition()
    {
        _isPrimed = _constraints.IsWithinConstraints();

        return _isPrimed;
    }
}