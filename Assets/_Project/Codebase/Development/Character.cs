using UnityEngine;

namespace FishingGame.Development
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private GameObject _graphics;
        public Vector2 Velocity { get; private set; }
        public bool FacingLeft { get; private set; }
        public float FlipValue => FacingLeft ? -1f : 1f;
        
        [HideInInspector] public Vector2 moveInput;
        
        private Rigidbody2D _rb;
        private Vector2 _smoothVel;
        private const float WALK_SPEED = 5f;

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            float speed = WALK_SPEED;
            Velocity = Vector2.SmoothDamp(Velocity, new Vector2(moveInput.x * speed, 0f),
                ref _smoothVel, .125f);

            _rb.velocity = Velocity;
            
            FacingLeft = Velocity.x < 0f;
            
            if (_graphics != null)
                _graphics.transform.localScale = new Vector3(FacingLeft ? -1f : 1f, 1f, 1f);
        }
    }
}