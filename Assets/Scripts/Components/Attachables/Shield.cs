using UnityEngine;

public class Shield : MonoBehaviour, IAttachable, ITakeDamage
{
    private SpriteRenderer _spriteRenderer;
    private Player _player;

    [SerializeField] private bool _isEnemy;
    [field: SerializeField] public int Health { get; private set; }

    [SerializeField] private float _invulnerabilityLength = 0.25f;
    public float InvulnerabilityLength => _invulnerabilityLength;

    private float _invulnerabilityPeriod;

    [SerializeField] private int _maxHealth;
    [field: SerializeField] public bool IsActive { get; private set; } = false;

    public bool TakesCollisionDamage { get; } = true;
    public int CollisionDamage { get; }

    [SerializeField] private Color _fullStrengthColour = Color.white;
    [SerializeField] private Color _midStrengthColour = Color.green;
    [SerializeField] private Color _lowStrengthColour = Color.red;

    private Collider2D _collider;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (_spriteRenderer == null)
            Debug.LogError($"The sprite renderer is null on the {transform.name}");

        _collider = GetComponent<Collider2D>();
        if (_collider == null)
            Debug.LogError($"The collider is null on the {transform.name}");
    }

    private void Start()
    {
        SetActiveState(IsActive);
        if(_isEnemy)
            return;
        _player = GetComponentInParent<Player>();
        if (_player == null)
            Debug.LogError("The player is null");
    }

    public void Attach()
    {
        Health = _maxHealth;
        SetColour();
        SetActiveState(true);
    }

    public void Detach()
    {
        Health = 0;
        SetActiveState(false);
    }

    public void TakeDamage(int damageAmount)
    {
        if (Time.time < _invulnerabilityPeriod)
            return;

        _invulnerabilityPeriod = Time.time + _invulnerabilityLength;
        Health -= damageAmount;
        SetColour();

        if (Health < 1)
        {
            Invoke(nameof(Detach), _invulnerabilityLength);
            if(_isEnemy)
                return;
            _player.ShakeTrigger.TriggerLongShake();
        }
        else
        {
            if(_isEnemy)
                return;
            _player.ShakeTrigger.TriggerSmallShake();
        }
    }

    private void SetColour()
    {
        _spriteRenderer.color = Health switch
        {
            3 => _fullStrengthColour,
            2 => _midStrengthColour,
            _ => _lowStrengthColour
        };
    }

    private void SetActiveState(bool isActive)
    {
        IsActive = isActive;
        _spriteRenderer.enabled = isActive;
        _collider.enabled = IsActive;
    }
}