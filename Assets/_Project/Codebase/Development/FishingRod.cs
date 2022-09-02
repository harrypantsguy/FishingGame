using System;
using UnityEngine;

namespace FishingGame.Development
{
    public class FishingRod : MonoBehaviour
    {
        [SerializeField] private DistanceJoint2D _bobberToRodJoint;
        [SerializeField] private DistanceJoint2D _boberToLureJoint;
        [SerializeField] private Bobber _bobber;
        [field: SerializeField] public Lure Lure { get; private set; }

        private bool _fishing;
        
        private const float REEL_IN_SPEED = 4f; 
        private const float REEL_OUT_SPEED = 4f; 
        private const float MIN_LENGTH = 1f; 
        private const float MAX_LENGTH = 20f;

        private void Update()
        {
            if (!_fishing) return;
            
            float newDistance = _bobberToRodJoint.distance;
            if (GameControls.ReelOut.IsHeld)
                newDistance += REEL_OUT_SPEED * Time.deltaTime;
            if (GameControls.ReelIn.IsHeld)
                newDistance -= REEL_IN_SPEED * Time.deltaTime;

            newDistance = Mathf.Clamp(newDistance, MIN_LENGTH, MAX_LENGTH);
            _bobberToRodJoint.distance = newDistance;
        }
        
        public void ThrowLure(Vector2 start, Vector2 velocity)
        {
            _fishing = true;
            float xDir = Mathf.Sign(velocity.x);
            Lure.RB.simulated = true;
            Lure.RB.position = start;
            Lure.RB.velocity = velocity;
            Lure.RB.angularVelocity = 60f * -xDir;
            Lure.transform.up = -velocity;

            float length = MAX_LENGTH * .75f;
            _bobberToRodJoint.distance = length;
            _boberToLureJoint.distance = 10f;
            _bobber.transform.position = Lure.RB.position + new Vector2(0f, 5f);//new Vector2(-xDir * length / 1.41f,  length / 1.41f);
            _bobber.RB.velocity = Lure.RB.velocity * .85f;
        }
    }
}