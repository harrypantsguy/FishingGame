using UnityEngine;

namespace FishingGame.Development.RopePhysics
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class PhysicsRopeSegment : MonoBehaviour
    {
        public Vector2 PreviousPosition { get; set; }

        public Vector2 Position
        {
            get => transform.position;
            set => transform.position = value;
        }
        
        public Vector2 Velocity { get; set; }

        private Rigidbody2D _rb;
        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }
    }
}