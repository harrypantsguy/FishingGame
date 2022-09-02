using FishingGame.Utilities;
using UnityEngine;

namespace FishingGame.Development
{
    public class Bobber : MonoBehaviour
    {
        public Rigidbody2D RB { get; private set; }
        public CircleCollider2D Collider { get; private set; }

        private void Start()
        {
            RB = GetComponent<Rigidbody2D>();
            Collider = GetComponent<CircleCollider2D>();
        }

        private void FixedUpdate()
        {
            bool underwater = RB.position.y < 0f;

            RB.drag = underwater ? 8f : 0f;
            //RB.angularDrag = underwater ? 1f : .05f;

            if (underwater)
            {
                float waterDensity = 200f;
                float force = RB.position.y.ClampedRemap(-Collider.radius, 0f,
                    Collider.radius * waterDensity, 0f);
                RB.velocity += new Vector2(0f, force * Time.fixedDeltaTime);
                
                transform.eulerAngles = transform.eulerAngles.SetZ(Mathf.LerpAngle(transform.eulerAngles.z,
                    0f, 10f * Time.deltaTime));
            }

            //RB.velocity = RB.velocity.SetY(RB.velocity.y - Time.fixedDeltaTime * 5f);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            //RB.simulated = false;
            Debug.Log($"bobber hit, {Mathf.Abs(RB.position.x - Player.Singleton.playerController.transform.position.x)}");
        }
    }
}