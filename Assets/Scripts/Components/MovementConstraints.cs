using UnityEngine;

public class MovementConstraints : MonoBehaviour
{
    [SerializeField] private float _xMinRange, _xMaxRange;
    [SerializeField] private float _yMinRange, _yMaxRange;
    [SerializeField] private bool _xBounded;

    private void Update()
    {
        CheckXPosition();

        CheckYPosition();
    }

    private void CheckYPosition()
    {
        if (transform.position.y <= _yMinRange)
        {
            var currentPosition = transform.position;
            transform.position = new Vector3(currentPosition.x, _yMinRange, currentPosition.z);
        }

        if (transform.position.y > _yMaxRange)
        {
            var currentPosition = transform.position;
            transform.position = new Vector3(currentPosition.x, _yMaxRange, currentPosition.z);
        }
    }

    private void CheckXPosition()
    {
        if (transform.position.x < _xMinRange)
        {
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
