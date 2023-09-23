using UnityEngine;

/// <summary>
///  Animates character based on movementScript
/// </summary>
[RequireComponent(typeof(Animator))]
public class PlayerAnimatorScript : MonoBehaviour
{
    // Requiered components to use class
    private Animator _animator;
    private PlayerMovement _playerMovement;
    
    
    private readonly string _animatorSpeedTag = "Speed"; 
    
    private bool _isLookingLeft;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _playerMovement = GetComponentInParent<PlayerMovement>();
        if (_playerMovement == null)
            throw new MissingComponentException("PlayerMovement component is missing in parent");
    }
    
    private void Update()
    {
        UpdateAnimator();
    }
    
    private void UpdateAnimator()
    {
        // pass speed to the animation, speed is magnitude of the deltaPosition vector
        _animator.SetFloat(_animatorSpeedTag, _playerMovement.CurrentSpeed.magnitude);
        
        // check if character currently looks at the right side
        // if looking left, right would be if the character goes right 
        if (_isLookingLeft && _playerMovement.CurrentSpeed.x > 0)
        {
            _isLookingLeft = false;
            Mirror();
        }
        else if (!_isLookingLeft && _playerMovement.CurrentSpeed.x < 0)
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
