using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _speedBoost;
    private float _curSpeed;
    
    void Update()
    {
        ChangeSpeed();
        Move();
    }

    private void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = transform.forward * vertical + transform.right * horizontal;
        transform.position += direction * Time.deltaTime * _curSpeed;
    }

    private void ChangeSpeed()
    {
        if (_curSpeed == 0)
        {
            _curSpeed = _speed;
        }
        
        if (Input.GetButtonDown("left shift"))
        {
            _curSpeed = _speed * _speedBoost;
        }

        if (Input.GetButtonUp("left shift"))
        {
            _curSpeed = _speed;
        }
    }
}
