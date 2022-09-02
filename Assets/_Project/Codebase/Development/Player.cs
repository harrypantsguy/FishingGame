using System;
using UnityEngine;
using UnityEngine.UI;

namespace FishingGame.Development
{
    public class Player : MonoSingleton<Player>
    {
        public Character playerController;
        [SerializeField] private Image _castStrengthImage;
        [SerializeField] private FishingRod _rod;
        private float castStrength;
        private float _castStartTime;
        private const float PERFECT_CAST_TIME = 1.25f;
        private const float PERFECT_CAST_CUSHION = .01f;
        private const float CAST_STRENGTH_MULTIPLIER = 10f;
        private CameraController _camera;
        private bool _overshotCast;
        private bool _casting;
        
        private void Start()
        {
            _camera = CameraController.Singleton;
            _camera.SetTargetTransform(playerController.transform);
            _camera.transform.position = playerController.transform.position;
            
            Application.targetFrameRate = 60;
        }

        private void Update()
        {
            if (playerController != null)
            {
                playerController.moveInput = _casting ? Vector2.zero : GameControls.DirectionalInput;
            }

            if (GameControls.CastLine.IsPressed)
            {
                _casting = true;
                
                _castStartTime = Time.time;
                castStrength = 0f;
                _overshotCast = false;
                _camera.SetTargetTransform(playerController.transform);
            }
            else if (GameControls.CastLine.IsHeld)
            {
                float t = Time.time - _castStartTime;
                if (t > PERFECT_CAST_TIME)
                {
                    castStrength -= Time.deltaTime / 2f;
                    _overshotCast = true;
                }
                else
                    castStrength = Mathf.Pow(t / PERFECT_CAST_TIME, 3f);

                castStrength = Mathf.Clamp01(castStrength);
            }
            else if (GameControls.CastLine.IsReleased)
            {
                bool isPerfectCast = false;
                if (Mathf.Abs(1f - castStrength) < PERFECT_CAST_CUSHION)
                {
                    isPerfectCast = true;
                    castStrength = 1f;
                    Debug.Log("perfect cast!");
                }

                float strength = Mathf.Max(castStrength * CAST_STRENGTH_MULTIPLIER * (isPerfectCast ? 1.25f : 1f), .707f);
                Vector2 throwVector = new Vector2(playerController.FlipValue * strength, 5f);
                _rod.ThrowLure(playerController.transform.position + new Vector3(0f, 1.5f), throwVector);
                _camera.SetTargetTransform(_rod.Lure.transform);
            }

            if (_castStrengthImage != null)
            {
                _castStrengthImage.fillAmount = castStrength;
                if (_overshotCast)
                    _castStrengthImage.color =
                        Color.Lerp(Color.green, Color.red, Mathf.Clamp01((1f - castStrength) * 2f));
                else
                    _castStrengthImage.color = Color.Lerp(Color.yellow, Color.green, castStrength);
            }
        }
    }
}