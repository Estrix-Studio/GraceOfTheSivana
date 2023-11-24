using UnityEngine;

namespace Adventure
{
    /// <summary>
    /// Moves character using old input system
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField, Range(0, 10)] private float speed;

        private Vector2 _currentSpeed;
    
        public Vector2 CurrentSpeed => _currentSpeed;

        private Rigidbody2D _rigidbody;
    
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }
    
    
        private void Update()
        {
            Move();
        }

        private void Move()
        {
            // Read Input
            var moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        
            _currentSpeed = moveDirection.normalized * speed;
        
            _rigidbody.velocity = _currentSpeed;
        }
    }
}
