using UnityEngine;

namespace FishingGame
{
    public class Bobber : MonoBehaviour
    {
        private Rigidbody2D _rb;

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _rb.simulated = false;
        }

        private void FixedUpdate()
        {
            //_rb.velocity = _rb.velocity.SetY(_rb.velocity.y - Time.fixedDeltaTime * 5f);
        }

        public void ThrowBobber(Vector2 start, Vector2 velocity)
        {
            _rb.simulated = true;
            _rb.position = start;
            _rb.velocity = velocity;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            //_rb.simulated = false;
            Debug.Log($"bobber hit, {Mathf.Abs(_rb.position.x - Player.Singleton.playerController.transform.position.x)}");
        }
    }
}