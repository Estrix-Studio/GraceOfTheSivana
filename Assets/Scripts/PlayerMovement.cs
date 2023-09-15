using UnityEngine;

/// <summary>
/// Moves character using old input system
/// Animates movement
/// </summary>
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField, Range(0, 10)] private float speed;

    private Vector2 _currentSpeed;
    private bool _isLookingLeft;

    private Animator _animator;
    private readonly string _animatorSpeedTag = "Speed"; 

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
        // Read Input
        var moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        
        // Find deltaPosition, dependent on deltaTime
        _currentSpeed = moveDirection * (Time.deltaTime * speed);
        
        // Move object
        transform.Translate(_currentSpeed);
    }

    private void UpdateAnimator()
    {
        // pass speed to the animation, speed is magnitude of the deltaPosition vector
        _animator.SetFloat(_animatorSpeedTag, _currentSpeed.magnitude);
        
        // check if character currently looks at the right side
        // if looking left, right would be if the character goes right 
        if (_isLookingLeft && _currentSpeed.x > 0)
        {
            _isLookingLeft = false;
            Mirror();
        }
        else if (!_isLookingLeft && _currentSpeed.x < 0)
        {
            _isLookingLeft = true;
            Mirror();
        }
    }

    private void Mirror()
    {            
        // mirror object by scaling by -1 by x axis
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
