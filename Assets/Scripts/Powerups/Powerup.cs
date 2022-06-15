using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField] private WeaponSO _weaponPowerup;
    [SerializeField] private float _speed = 3.0f;

    private void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime );
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<Player>();
            if (player != null)
            {
                GivePowerup(player);
            }
        }
    }
    private void GivePowerup(Player player)
    {
        player.ChangeWeapon(_weaponPowerup);
        DestroySelf();
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
