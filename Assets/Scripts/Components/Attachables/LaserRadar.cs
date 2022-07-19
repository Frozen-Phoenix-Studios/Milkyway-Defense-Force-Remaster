using System;
using System.Collections;
using UnityEngine;

public class LaserRadar : MonoBehaviour, IAttachable
{
    public Action<float> OnDodge;
    private bool _isActive;
    private bool _takesCollisionDamage;
    private int _health;
    private float _invulnerabilityLength;
    private int _collisionDamage;
    public bool IsActive => _isActive;
    
    private CircleCollider2D _collider;
    [SerializeField] private float _dodgeCooldown = 5.0f;
    [SerializeField] private float _dodgeLength = 0.5f;
    [SerializeField] private float _detectionRadius = 3.0f;

    private void Start()
    {
        Attach();
    }

    private void Update()
    {
        if (IsActive)
        {
            CheckForLasers();
        }
    }

    public void Attach()
    {
        _isActive = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = _isActive ? Color.green : Color.red;
        Gizmos.DrawSphere(transform.position, _detectionRadius);
    }

    public void Detach()
    {
        _isActive = false;
    }

    private void CheckForLasers()
    {
        if (_isActive)
        {
            var hitInfo = Physics2D.CircleCast(transform.position, _detectionRadius, Vector2.down);

            if (hitInfo.collider != null && hitInfo.collider.CompareTag("PlayerAttack"))
            {
                StartCoroutine(DodgeRoutine());
            }
        }

    }


    private IEnumerator DodgeRoutine()
    {
        Detach();
        OnDodge?.Invoke(_dodgeLength);
        yield return new WaitForSeconds(_dodgeCooldown);
        Attach();
    }
}
