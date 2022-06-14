using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private PlayerInputReader _input;

    [SerializeField] private Laser _laserPrefab;
    [SerializeField] private float _offset = 1.0f;
    [SerializeField] private float _attackSpeed = 0.25f;
    private float _nextAttackTime;

    private void Start()
    {
        _nextAttackTime = 0;
        _input = GetComponent<PlayerInputReader>();
        if (_input == null)
            Debug.LogError("The player input reader is null on the player attack");
    }

    private void Update()
    {
        if (_input.shoot && CanShoot())
            Shoot();
    }

    private bool CanShoot()
    {
        if (!(Time.time >= _nextAttackTime)) return false;
        
        _nextAttackTime = Time.time + _attackSpeed;
        return true;
    }

    private void Shoot()
    {
        var position = transform.position;
        position.y += _offset;

        Instantiate(_laserPrefab.gameObject, position, Quaternion.identity);
    }
}