using System;
using UnityEngine;

public abstract class Attack : MonoBehaviour, IDoDamage, IHaveAudio
{
    public event Action<bool> OnDestroy;
    
    [Header("Movement")]
    [SerializeField] private bool _isEnemy;
    [SerializeField] protected float _movementSpeed = 8.0f;
    
    [Header("Damage")]
    [SerializeField] protected int _damageAmount = 1;
    [SerializeField] protected string[] _damageableTags;
    
    [Header("Audio")]
    [SerializeField] private AudioClip _audioClip;
    
    private protected bool _destroyedFromCollision;
    
    public string[] DamageableTags => _damageableTags;


    public int DamageAmount => _damageAmount; 
    public AudioClip AudioClip => _audioClip;

    protected virtual void Initialize()
    {
        if (_isEnemy)
            _movementSpeed = -_movementSpeed;

        PlayAudio();
    }
    
    protected void Movement()
    {
        transform.Translate(Vector3.up * _movementSpeed * Time.deltaTime);
    }
    
    public void PlayAudio()
    {
        if (_audioClip == null)
            return;

        if (_isEnemy)
        {
            AudioManager.Instance.EnemyAttackAudioClip(this);
        }
        else
        {
            AudioManager.Instance.PlayPlayerAttackAudioClip(this);
        }
    }
    
    protected virtual void Destroy()
    {
        OnDestroy?.Invoke(_destroyedFromCollision);
        Destroy(gameObject);
    }
    
    public void DealDamage(int damageAmount, ITakeDamage damageable)
    {
        HandleDamageDealing(damageAmount, damageable);
    }
    
    protected virtual void HandleDamageDealing(int damageAmount, ITakeDamage damageable)
    {
        damageable.TakeDamage(damageAmount);
        Destroy();
    }
    
    protected void OnTriggerEnter2D(Collider2D other)
    {
        foreach (var damageableTag in _damageableTags)
        {
            if (other.CompareTag(damageableTag))
            {
                var damageable = other.GetComponent<ITakeDamage>();
                if (damageable != null)
                {
                    _destroyedFromCollision = true;
                    DealDamage(_damageAmount, damageable);
                }
                return;
            }
        }
    }



}
