using UnityEngine;

public class Mine : Attack, IExplode
{
    [SerializeField] private Explosion _explosionPrefab;
    [SerializeField] private float _attractionRadius;
    private Transform _playerTransform;
    private bool _tractorBeamActive;
    [SerializeField] private Explosion _explosion;
    public Explosion Explosion => _explosion;
    private bool _canChasePlayer = true;

    protected override void Initialize()
    {
        PlayAudio();

        _playerTransform = FindObjectOfType<Player>().transform;
        if (_playerTransform == null)
            Debug.LogError($"The player is null on the {transform.name}");

        MagneticFieldManager.OnTractorBeamActive += HandleTractorBeamChanges;
        GameStateManager.OnGameOver += HandleGameOverStateChange;
    }

    private void HandleGameOverStateChange(bool isGameOver)
    {
        _canChasePlayer = !isGameOver;
    }

    private void OnDisable()
    {
        MagneticFieldManager.OnTractorBeamActive -= HandleTractorBeamChanges;
        GameStateManager.OnGameOver -= HandleGameOverStateChange;
    }

    private void HandleTractorBeamChanges(bool isActive, GameObject tractorBeamSource) => _tractorBeamActive = isActive;

    private void Update()
    {
        if (!_canChasePlayer) return;

        if (_tractorBeamActive || CheckPlayerInRange())
            ChasePlayer();
    }

    private void ChasePlayer() => transform.position =
        Vector2.MoveTowards(transform.position, _playerTransform.position, _movementSpeed * Time.deltaTime);

    private bool CheckPlayerInRange() =>
        Vector2.Distance(transform.position, _playerTransform.position) <= _attractionRadius;


    protected override void HandleDamageDealing(int damageAmount, ITakeDamage damageable)
    {
        Explode();
        base.HandleDamageDealing(damageAmount, damageable);
    }

    public void Explode()
    {
        if (_explosionPrefab != null)
        {
            Instantiate(_explosionPrefab.gameObject, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}