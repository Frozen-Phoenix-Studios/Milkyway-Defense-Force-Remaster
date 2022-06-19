using UnityEngine;

public class Shield : MonoBehaviour, IAttachable, ITakeDamage
{ 
    private SpriteRenderer _spriteRenderer;
    [field: SerializeField] public int Health { get; private set; }

    [SerializeField]  private float _invulnerabilityLength = 0.25f;
    public float InvulnerabilityLength => _invulnerabilityLength;

    private float _invulnerabilityPeriod;

    [SerializeField] private int _maxHealth;
   [field: SerializeField] public bool IsActive { get; private set; } = false;

   public bool TakesCollisionDamage { get; } = true;
    public int CollisionDamage { get; }


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
    }

    public void Attach()
    {
        Health = _maxHealth;
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
        if (Health < 1)
            Detach();

    }

    private void SetActiveState(bool isActive)
    {
        IsActive = isActive;
        _spriteRenderer.enabled = isActive;
        _collider.enabled = IsActive;
    }
}
