using System.Linq;
using UnityEngine;

public class VisionAttackCondition : MonoBehaviour, IAttackCondition
{
    private string[] _damageables;
    private IDoDamage _damageable;
    [SerializeField] private LayerMask LayerMask;
    [SerializeField] private Vector2 _raycastDirection;
    [SerializeField] private float _raycastDistance;
    private bool _isPrimed;
    public bool IsPrimed => _isPrimed;


    private void Start()
    {
        _damageable = GetComponent<IDoDamage>();
        _damageables = _damageable.DamageableTags;
    }


    public bool CheckIsMet()
    {
        if (_isPrimed)
        {
            _isPrimed = false;
            return true;
        }
        return _isPrimed;
    }

    public bool PrimeCondition()
    {
        var hitInfo = Physics2D.Raycast(transform.position, _raycastDirection, _raycastDistance, LayerMask);
        if (hitInfo.collider != null)
        {
            if (_damageables.Any(damageableTag => hitInfo.collider.CompareTag(damageableTag)))
            {
                _isPrimed = true;

                return _isPrimed;
            }
        }

        _isPrimed = false;
        return _isPrimed;

    }
}