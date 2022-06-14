using UnityEngine;
using UnityEngine.Events;

public class MovementConstraints : MonoBehaviour
{
    [SerializeField] private float _xMinRange;
    public float XMinRange => _xMinRange;
    [SerializeField] private float _xMaxRange;
    public float XMaxRange => _xMaxRange;
    [SerializeField] private float _yMinRange;
    public float YMinRange => _yMinRange;
    [SerializeField] private float _yMaxRange;
    public float YMaxRange => _yMaxRange;

    [SerializeField] private bool _xBounded;
    [SerializeField] public UnityEvent _actionOnYMinBoundBreach;
    [SerializeField] public UnityEvent _actionOnYMaxBoundBreach;
    [SerializeField] public UnityEvent _actionOnXMinBoundBreach;
    [SerializeField] public UnityEvent _actionOnXMaxBoundBreach;

    private void Update()
    {
        CheckXPosition();

        CheckYPosition();
    }

    private void CheckYPosition()
    {
        if (transform.position.y <= _yMinRange)
        {
            if (_actionOnYMinBoundBreach != null)
            {
                _actionOnYMinBoundBreach.Invoke();
                Debug.Log($"Fire y min breach event from {transform.name}");

                
                return;
            }

            var currentPosition = transform.position;
            transform.position = new Vector3(currentPosition.x, _yMinRange, currentPosition.z);
        }

        if (transform.position.y > _yMaxRange)
        {
            if (_actionOnYMaxBoundBreach != null)
            {
                _actionOnYMaxBoundBreach.Invoke();
                Debug.Log($"Fire y max breach event from {transform.name}");

                return;
            }

            var currentPosition = transform.position;
            transform.position = new Vector3(currentPosition.x, _yMaxRange, currentPosition.z);
        }
    }

    private void CheckXPosition()
    {
        if (transform.position.x < _xMinRange)
        {
            if (_actionOnXMinBoundBreach != null)
            {
                _actionOnXMinBoundBreach.Invoke();
                Debug.Log($"Fire x min breach event from {transform.name}");
                return;
            }
            var currentPosition = transform.position;
            if (_xBounded)
            {
                transform.position = new Vector3(_xMinRange, currentPosition.y, currentPosition.z);
            }
            else
            {
                transform.position = new Vector3(_xMaxRange, currentPosition.y, currentPosition.z);
            }
        }

        if (transform.position.x > _xMaxRange)
        {
            if (_actionOnXMaxBoundBreach != null)
            {
                _actionOnXMaxBoundBreach.Invoke();
                Debug.Log($"Fire x max breach event from {transform.name}");

                return;
            }

            var currentPosition = transform.position;
            if (_xBounded)
            {
                transform.position = new Vector3(_xMaxRange, currentPosition.y, currentPosition.z);
            }
            else
            {
                transform.position = new Vector3(_xMinRange, currentPosition.y, currentPosition.z);
            }
        }
    }
}