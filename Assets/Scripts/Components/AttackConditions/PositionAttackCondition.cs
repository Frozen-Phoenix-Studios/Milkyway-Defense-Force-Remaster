using UnityEngine;

public class PositionAttackCondition: MonoBehaviour, IAttackCondition
{
    [SerializeField] private AttackConstraints _constraints;
    [SerializeField] private bool _isPrimed;
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


    public void Activate()
    {
        PrimeCondition();
    }

    public void PrimeCondition()
    {
        _isPrimed = _constraints.IsWithinConstraints();
    }
}