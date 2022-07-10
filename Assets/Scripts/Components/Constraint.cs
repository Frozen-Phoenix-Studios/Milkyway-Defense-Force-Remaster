using UnityEngine;
using UnityEngine.Events;

public class Constraint : MonoBehaviour
{
    [SerializeField] private float _xMinRange;
    [SerializeField] private float _xMaxRange;
    [SerializeField] private float _yMinRange;
    [SerializeField] private float _yMaxRange;
    [SerializeField] private bool _xBounded;
    [SerializeField] private bool _yBounded = true;
    [SerializeField] public UnityEvent _actionOnYMinBoundBreach;
    [SerializeField] public UnityEvent _actionOnYMaxBoundBreach;
    [SerializeField] public UnityEvent _actionOnXMinBoundBreach;
    [SerializeField] public UnityEvent _actionOnXMaxBoundBreach;
    public float XMinRange => _xMinRange;
    public float XMaxRange => _xMaxRange;
    public float YMinRange => _yMinRange;
    public float YMaxRange => _yMaxRange;

    private void Update()
    {
        CheckXPosition();

        CheckYPosition();
    }

    private void CheckYPosition()
    {
        var currentPosition = transform.position;

        if (transform.position.y <= _yMinRange)
        {
            if (_yBounded)
                transform.position = new Vector3(currentPosition.x, _yMinRange, currentPosition.z);

            if (_actionOnYMinBoundBreach != null)
            {
                _actionOnYMinBoundBreach.Invoke();

                return;
            }
        }

        if (transform.position.y > _yMaxRange)
        {
            if (_yBounded)
                transform.position = new Vector3(currentPosition.x, _yMaxRange, currentPosition.z);
            
            if (_actionOnYMaxBoundBreach != null)
                _actionOnYMaxBoundBreach.Invoke();
        }
    }

    private void CheckXPosition()
    {
        var currentPosition = transform.position;

        if (transform.position.x < _xMinRange)
        {
            if (_xBounded)
                transform.position = new Vector3(_xMinRange, currentPosition.y, currentPosition.z);

            if (_actionOnXMinBoundBreach != null)
            {
                _actionOnXMinBoundBreach.Invoke();
                return;
            }
        }

        if (transform.position.x > _xMaxRange)
        {
            if (_xBounded)
                transform.position = new Vector3(_xMaxRange, currentPosition.y, currentPosition.z);

            if (_actionOnXMaxBoundBreach != null)
                _actionOnXMaxBoundBreach.Invoke();
        }
    }
}