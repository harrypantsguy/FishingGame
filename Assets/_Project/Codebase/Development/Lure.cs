using FishingGame.Utilities;
using UnityEngine;

namespace FishingGame.Development
{
    public class Lure : MonoBehaviour
    {
        public Rigidbody2D RB { get; private set; }
        [SerializeField] private Bobber _bobber;

        private void Start()
        {
            RB = GetComponent<Rigidbody2D>();
        }
        
        private void FixedUpdate()
        {
            bool underwater = RB.position.y < 0f;
            RB.drag = underwater ? 8f : 0f;
            if (!underwater)
                transform.up = -RB.velocity;
            else
                transform.eulerAngles = transform.eulerAngles.SetZ(Mathf.LerpAngle(transform.eulerAngles.z,
                    Utils.DirectionToAngle((_bobber.transform.position - transform.position).normalized) - 90f,
                    25f * Time.deltaTime));
        }
    }
}