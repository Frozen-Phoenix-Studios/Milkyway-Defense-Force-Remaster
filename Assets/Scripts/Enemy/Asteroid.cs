using UnityEngine;

public class Asteroid : MonoBehaviour, ITakeDamage, IDoDamage
{
    public bool TakesCollisionDamage { get; }
    public int Health { get; } = 1;
    public int CollisionDamage { get; } = 1;
    public int DamageAmount { get; }
    private MovementConstraints _constraints;
    [field: SerializeField] public string[] DamageableTags { get; private set; }
    

    [SerializeField] private float _rotationSpeed = 3.0f;
    private Vector3 _rotationDirection = new Vector3(0,0,1);
    private Animator _anim;

    private void Start()
    {
        _anim = GetComponent<Animator>();
        if(_anim == null)
            Debug.LogError("The animator is null on the asteroid");
        
        _constraints = GetComponent<MovementConstraints>();
        if(_constraints == null)
            Debug.LogError("The constraints are null on the asteroid");

        SetRandomPosition();
    }

    private void SetRandomPosition()
    {
        var x = Random.Range(_constraints.XMinRange, _constraints.XMaxRange);
        var y = Random.Range(_constraints.YMinRange, _constraints.YMaxRange);
        transform.position = new Vector3(x, y, 0);
    }

    void Update() => transform.Rotate(_rotationDirection * _rotationSpeed * Time.deltaTime);

    private void OnTriggerEnter2D(Collider2D other)
    {
        foreach (var damageableTag in DamageableTags)
        {
            if (other.CompareTag(damageableTag))
            {
                var damageable = other.GetComponent<ITakeDamage>();
                if (damageable != null)
                {
                    DealDamage(DamageAmount, damageable);
                }
                return;
            }
        }
    }
    
    public void TakeDamage(int damageAmount) => Explode();

    private void Explode()
    {
        _anim.SetTrigger("Explode");
        GameStateManager.Instance.TriggerGameStart();
        
    }

    private void Destroy() => Destroy(gameObject);

    public void DealDamage(int damageAmount, ITakeDamage damageable)
    {
        damageable.TakeDamage(DamageAmount);
        Explode();
    }
}
