using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private PlayerInputReader _input;
    [SerializeField] private Laser _laserPrefab;

    private void Start()
    {
        _input = GetComponent<PlayerInputReader>();
        if (_input == null)
        {
            Debug.LogError("The player input reader is null on the player attack");
        }
    }

    private void Update()
    {
        if (_input.shoot)
            Shoot();
    }

    private void Shoot()
    {
        Debug.Log("Shooting");
        Instantiate(_laserPrefab.gameObject, transform.position, Quaternion.identity);
    }
}
