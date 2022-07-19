using System;
using System.Linq;
using UnityEngine;

public class VisionAttackCondition : MonoBehaviour, IAttackCondition
{
    private string[] _damageables;
    private IDoDamage _damageable;
    [SerializeField] private LayerMask LayerMask;
    [SerializeField] private Vector2 _raycastDirection;
    [SerializeField] private float _raycastDistance;
    [SerializeField] private bool _isPrimed;
    public bool IsPrimed => _isPrimed;


    private void Start()
    {
        _damageable = GetComponent<IDoDamage>();
        _damageables = _damageable.DamageableTags;
    }


    public void Activate()
    {
        if (_isPrimed)
            _isPrimed = false;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, _raycastDirection * _raycastDistance); 
    }

    public void PrimeCondition()
    {
        var hitInfo = Physics2D.Raycast(transform.position, _raycastDirection, _raycastDistance, LayerMask);
        if (hitInfo.collider != null)
        {
            if (_damageables.Any(damageableTag => hitInfo.collider.CompareTag(damageableTag)))
            {
                _isPrimed = true;
                return;
            }
        }

        _isPrimed = false;

    }
}