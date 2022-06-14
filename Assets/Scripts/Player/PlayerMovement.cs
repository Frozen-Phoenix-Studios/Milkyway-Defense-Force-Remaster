using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerInputReader _input;
    [SerializeField] private float _speed = 5.0f;
    private MovementConstraints _constraints;

    private void Start()
    {
        _input = GetComponent<PlayerInputReader>();
        if (_input == null)
        {
            Debug.LogError("The Input is null");
        }

        _constraints = GetComponent<MovementConstraints>();
        if (_constraints == null)
        {
            Debug.LogError("The movement constraints are null");
        }
    }

    void Update()
    {
        transform.Translate((_input.move * Time.deltaTime * _speed));
    }

    public void Teleport(Vector3 newPosition)
    {
        transform.position = newPosition;
    }

    public void TeleportLeft()
    {
        var x = _constraints.XMinRange;
        var pos = transform.position;
        var newPosition = new Vector2(x, pos.y);
        transform.position = newPosition;
    }

    public void TeleportRight()
    {
        var x = _constraints.XMaxRange;
        var pos = transform.position;
        var newPosition = new Vector2(x, pos.y);
        transform.position = newPosition;
    }

    public void TeleportUp()
    {
        var y = _constraints.YMaxRange;
        var pos = transform.position;
        var newPosition = new Vector2(pos.x, y);
        transform.position = newPosition;
    }

    public void TeleportDown()
    {
        var y = _constraints.YMinRange;
        var pos = transform.position;
        var newPosition = new Vector2(pos.x, y);
        transform.position = newPosition;
    }
}