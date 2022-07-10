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
            Debug.LogError($"The attack constraints are null on the {transform.name}");
        }
    }


    public bool CheckIsMet()
    {
        Debug.Log($"Condition {this} is met {_isPrimed}");

        return _isPrimed;
    }

    public bool PrimeCondition()
    {
        _isPrimed = _constraints.IsWithinConstraints();
        Debug.Log($"Condition {this} is primed {_isPrimed}");

        return _isPrimed;
    }
}