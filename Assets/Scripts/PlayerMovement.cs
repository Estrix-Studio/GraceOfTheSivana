using UnityEngine;

/// <summary>
/// Moves character using old input system
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    [SerializeField, Range(0, 10)] private float speed;

    private Vector2 _currentSpeed;
    
    public Vector2 CurrentSpeed => _currentSpeed;

    private void Update()
    {
        Move();
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
}
