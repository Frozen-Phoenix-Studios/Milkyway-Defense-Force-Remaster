using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerInputReader _input;
    [SerializeField] private float _speed = 5.0f;

    private void Start()
    {
        _input = GetComponent<PlayerInputReader>();
        if (_input == null)
        {
            Debug.LogError("The Input is null");
        }

    }
    void Update()
    {
        transform.Translate((_input.move * Time.deltaTime * _speed));
    }

    private void Teleport(Vector3 position)
    {
        
    }
}
