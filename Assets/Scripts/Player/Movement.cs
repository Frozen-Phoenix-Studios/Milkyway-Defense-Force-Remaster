using UnityEngine;

public class Movement : MonoBehaviour
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

        TestFunction();
    }

    void TestFunction()
    {
        Debug.Log("test");
    }
    
    void Update()
    {
        transform.Translate((_input.move * Time.deltaTime * _speed));
    }
}
