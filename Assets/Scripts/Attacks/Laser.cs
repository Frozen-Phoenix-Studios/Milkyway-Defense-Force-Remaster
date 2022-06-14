using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 8.0f;
    private void Update()
    {
        Movement();
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
