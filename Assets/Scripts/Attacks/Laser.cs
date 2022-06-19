using System;
using UnityEngine;

public class Laser : MonoBehaviour, IDoDamage, IHaveAudio
{
    public event Action<bool> OnDestroy;

    [SerializeField] private float _movementSpeed = 8.0f;
    [SerializeField] private int _damageAmount = 1;
    [SerializeField] private string[] _damageableTags;
    private bool _destroyedFromCollision;
    public string[] DamageableTags => _damageableTags;
    public int DamageAmount => _damageAmount;
    [SerializeField] private bool _isEnemy;

    [field: SerializeField] public AudioClip AudioClip { get; private set; }


    private void OnEnable()
    {
        if (_isEnemy)
            _movementSpeed = -_movementSpeed;

        PlayAudio();
    }

    private void Update()
    {
        Movement();
    }

    private void OnTriggerEnter2D(Collider2D other)
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

    public void DealDamage(int damageAmount, ITakeDamage damageable)
    {
        damageable.TakeDamage(damageAmount);
        Destroy();
    }


    private void Movement()
    {
        transform.Translate(Vector3.up * _movementSpeed * Time.deltaTime);
    }

    public void Destroy()
    {
        OnDestroy?.Invoke(_destroyedFromCollision);
        Destroy(gameObject);
    }

    public void PlayAudio()
    {
        if (_isEnemy)
        {
            AudioManager.Instance.EnemyAttackAudioClip(this);
        }
        else
        {
            AudioManager.Instance.PlayPlayerAttackAudioClip(this);
        }

    }
}