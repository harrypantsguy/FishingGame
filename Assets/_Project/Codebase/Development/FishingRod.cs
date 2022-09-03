using System;
using UnityEngine;

namespace FishingGame.Development
{
    public class FishingRod : MonoBehaviour
    {
        [SerializeField] private DistanceJoint2D _bobberToRodJoint;
        [SerializeField] private DistanceJoint2D _bobberToLureJoint;
        [SerializeField] private Bobber _bobber;
        [field: SerializeField] public Lure Lure { get; private set; }
        public bool ReeledIn { get; private set; }
        private const float REEL_IN_SPEED = 4f; 
        private const float REEL_OUT_SPEED = 4f; 
        private const float MIN_BOBBER_TO_ROD_LENGTH = 1f; 
        private const float MAX_BOBBER_TO_ROD_LENGTH = 20f;
        private const float MIN_BOBBER_TO_LURE_LENGTH = 1f; 
        private const float MAX_BOBBER_TO_LURE_LENGTH = 20f;
        private const float LURE_HORIZONTAL_MOVE_SPEED = 4f;

        private void Start()
        {
            ReeledIn = true;
        }

        private void Update()
        {
            if (ReeledIn) return;
            
            float bobberToRodDist = Vector2.Distance(_bobberToRodJoint.transform.position,
                _bobberToRodJoint.connectedBody.position);
            if (GameControls.ReelIn.IsPressed || bobberToRodDist >= MAX_BOBBER_TO_ROD_LENGTH)
            {
                _bobberToRodJoint.enabled = true;
                _bobberToRodJoint.maxDistanceOnly = false;
                _bobberToRodJoint.distance = bobberToRodDist;
            }

            /*
            if (GameControls.MoveLureLeft.IsHeld)
                _bobber.RB.velocity += Vector2.left * (LURE_HORIZONTAL_MOVE_SPEED * Time.deltaTime);
            if (GameControls.MoveLureRight.IsHeld)
                _bobber.RB.velocity += Vector2.right * (LURE_HORIZONTAL_MOVE_SPEED * Time.deltaTime);
                */
            
            bool reelingOut = GameControls.ReelOut.IsHeld;
            bool reelingIn = GameControls.ReelIn.IsHeld;

            float reelAmount = (reelingIn ? -REEL_IN_SPEED : REEL_OUT_SPEED) * Time.deltaTime;
            
            if (reelingOut)
            {
                if (_bobberToLureJoint.distance < MAX_BOBBER_TO_LURE_LENGTH)
                    ChangeJointDistance(_bobberToLureJoint, reelAmount, MIN_BOBBER_TO_LURE_LENGTH,
                        MAX_BOBBER_TO_LURE_LENGTH);
                else
                    ChangeJointDistance(_bobberToRodJoint, reelAmount, MIN_BOBBER_TO_ROD_LENGTH,
                        MAX_BOBBER_TO_ROD_LENGTH);
            }
            else if (reelingIn)
            {
                if (_bobberToLureJoint.distance > MIN_BOBBER_TO_LURE_LENGTH)
                    ChangeJointDistance(_bobberToLureJoint, reelAmount, MIN_BOBBER_TO_LURE_LENGTH,
                        MAX_BOBBER_TO_LURE_LENGTH);
                else
                {
                    ChangeJointDistance(_bobberToRodJoint, reelAmount, MIN_BOBBER_TO_ROD_LENGTH,
                        MAX_BOBBER_TO_ROD_LENGTH);

                    if (Math.Abs(_bobberToRodJoint.distance - MIN_BOBBER_TO_ROD_LENGTH) < .01f)
                    {
                        ReeledIn = true;
                    }
                }
            }
        }
        
        private void ChangeJointDistance(DistanceJoint2D joint, float amount, float min, float max) => 
            joint.distance = Mathf.Clamp(joint.distance + amount, min, max);


        public void ThrowLure(Vector2 velocity)
        {
            ReeledIn = false;
            
            float xDir = Mathf.Sign(velocity.x);
            Lure.RB.simulated = true;
           // Lure.RB.position = start;
            Lure.RB.velocity = velocity;
            Lure.RB.angularVelocity = 60f * -xDir;
            Lure.transform.up = -velocity;

            float length = MAX_BOBBER_TO_ROD_LENGTH * .75f;
            
            _bobberToRodJoint.enabled = false;
            
            //_bobberToLureJoint.distance = 10f;
            //_bobber.transform.position = Lure.RB.position + new Vector2(0f, 5f);//new Vector2(-xDir * length / 1.41f,  length / 1.41f);
            _bobber.RB.velocity = Lure.RB.velocity * .85f;
        }
    }
}