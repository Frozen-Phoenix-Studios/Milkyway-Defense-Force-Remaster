using System;
using UnityEngine;

public abstract class Attack : MonoBehaviour, IDoDamage, IHaveAudio
{
    public event Action<bool> OnDestroy;

    [Header("Movement")] [SerializeField] private bool _movesUpScreen = true;
    private Vector2 _moveDirection = Vector2.zero;
    [SerializeField] protected float _movementSpeed = 8.0f;

    [Header("Damage")] [SerializeField] protected int _damageAmount = 1;
    [SerializeField] protected string[] _damageableTags;

    [Header("Audio")] [SerializeField] protected AudioClip _audioClip;
    [SerializeField] protected AudioType _audioType;
    public AudioType AudioType => _audioType;

    [SerializeField] private protected bool _destroyedFromCollision;

    public string[] DamageableTags => _damageableTags;

    public int DamageAmount => _damageAmount;
    public AudioClip AudioClip => _audioClip;

    private void OnEnable()
    {
        Initialize();
    }

    protected virtual void Initialize()
    {
        PlayAudio();
        _moveDirection.y = _movesUpScreen ? 1 : -1;
    }

    protected virtual void Movement()
    {
        transform.Translate(_moveDirection * _movementSpeed * Time.deltaTime);
    }

    public void PlayAudio()
    {
        AudioManager.Instance.PlayAudio(this);
    }

    public virtual void Destroy()
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