using UnityEngine;

namespace Adventure
{
    /// <summary>
    ///     Moves character using old input system
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] [Range(0, 10)] private float speed;

        private Rigidbody2D _rigidbody;

        public Vector2 CurrentSpeed { get; private set; }

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

            CurrentSpeed = moveDirection.normalized * speed;

            _rigidbody.velocity = CurrentSpeed;
        }
    }
}