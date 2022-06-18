using UnityEngine;

public class ThrusterManager : MonoBehaviour
{
    private  PlayerInputReader _input;
    [SerializeField] private GameObject[] _thrustersArray;
    private float _speed;
    private int _index;

    private void Start()
    {
        _input = GetComponentInParent<PlayerInputReader>();
        if (_input == null)
            Debug.LogError("The player input is null on the thruster manager");
    }

    private void Update()
    {
        _speed = _input.move.y;
        
        if (_speed < 0)
        {
            _index = 0;
        }
        else if (_speed == 0)
        {
            _index = 1;
        }
        else
        {
            _index = 2;
        }

        SetThrusterActive(_index);
    }

    private void SetThrusterActive(int index)
    {
        for (int i = 0; i < _thrustersArray.Length; i++)
            _thrustersArray[i].SetActive(i == index);
    }
}
