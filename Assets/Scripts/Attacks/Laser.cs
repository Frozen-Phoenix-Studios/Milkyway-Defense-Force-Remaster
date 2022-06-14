using UnityEngine;

public class Laser : MonoBehaviour, IDoDamage
{
    [SerializeField] private float _movementSpeed = 8.0f;
    [SerializeField] private int _damageAmount = 1;
    
    [SerializeField] private string[] _damageableTags;
    public string[] DamageableTags => _damageableTags;

    public int DamageAmount => _damageAmount;
    public void DealDamage(int damageAmount, ITakeDamage damageable)
    {
        damageable.TakeDamage(damageAmount);
        Destroy(gameObject);
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
                Debug.Log($"Collided with {damageableTag}");
                var damageable = other.GetComponent<ITakeDamage>();
                if (damageable != null)
                {
                    
                    DealDamage(_damageAmount, damageable);
                }

                return;
            }
        }
    }


    private void Movement()
    {
        transform.Translate(Vector3.up * _movementSpeed * Time.deltaTime);
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }


}
