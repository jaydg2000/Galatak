using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static readonly float _thrustForce = .025f;
    private static readonly float _friction = .01f;
    private float _moveSpeed = 0f;
    private float _maxSpeed = 5.0f;
    private Rigidbody2D _rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessKeyboard();
        Move();
    }

    private void ProcessKeyboard()
    {
        var thrusting = false;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            thrusting = true;
            ThrustLeft();
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            thrusting = true;
            ThrustRight();
        }

        if (!thrusting)
        {
            Slow();
        }
    }

    private void ThrustLeft()
    {
        _moveSpeed -= _thrustForce;
    }

    private void ThrustRight()
    {
        _moveSpeed += _thrustForce;
    }

    private void Slow()
    {
        if (_moveSpeed == 0f)
        {
            return;
        }

        if (_moveSpeed > 0f)
        {
            _moveSpeed -= _friction;
            if (_moveSpeed < 0f)
            {
                _moveSpeed = 0f;
            }
        }
        else
        {
            _moveSpeed += _friction;
            if (_moveSpeed > 0f)
            {
                _moveSpeed = 0f;
            }
        }
    }

    private void Move()
    {
        _rigidbody.velocity = new Vector2(_moveSpeed * _maxSpeed, _rigidbody.velocity.y);
    }
}
