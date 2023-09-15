using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField, Range(0, 10)] private float speed;

    private Vector2 _currentSpeed;
    private bool _isLookingLeft;

    private Animator _animator;
    private readonly string _animatorSpeedTag = "Speed"; 
    private readonly string _animatorDirectionTag = "IsLookingLeft"; 

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
        UpdateAnimator();
    }

    private void Move()
    {
        var moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        _currentSpeed = moveDirection * (Time.deltaTime * speed);
        
        transform.Translate(_currentSpeed);
    }

    private void UpdateAnimator()
    {
        _animator.SetFloat(_animatorSpeedTag, _currentSpeed.magnitude);
        
        if (_isLookingLeft && _currentSpeed.x > 0)
        {
            _isLookingLeft = false;
            Vector3 v3_scale = transform.localScale;
            v3_scale.x *= -1;
            transform.localScale = v3_scale;
        }
        else if (!_isLookingLeft && _currentSpeed.x < 0)
        {
            _isLookingLeft = true;
            Vector3 v3_scale = transform.localScale;
            v3_scale.x *= -1;
            transform.localScale = v3_scale;
        }
        
        _animator.SetBool(_animatorDirectionTag, _isLookingLeft);
    }
}
