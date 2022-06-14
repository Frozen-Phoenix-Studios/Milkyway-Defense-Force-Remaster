using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(IMove))]
public class Enemy : MonoBehaviour, IRespawn    
{
    private IMove _movement;
    private MovementConstraints _constraints;

    [SerializeField] private float _respawnHeight;
    public float RespawnHeight => _respawnHeight;

    private void Start()
    {
        _constraints = GetComponent<MovementConstraints>();
        if (_constraints == null)
            Debug.LogError("The movement constraints are null on the enemy");

        _movement = GetComponent<IMove>();
        if (_movement == null)
            Debug.LogError("The movement is null on the enemy");
    }

    private void Update()
    {
        _movement.Move();
    }

    public void Respawn()
    {
        transform.position = ReturnNewSpawnPosition();
    }

    private Vector3 ReturnNewSpawnPosition()
    {
        var x = Random.Range(_constraints.XMinRange, _constraints.XMaxRange);
        var y = _respawnHeight;
        var z = transform.position.z;
        return new Vector3(x, y, z);

    }
}
