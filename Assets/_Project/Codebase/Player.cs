using UnityEngine;
using UnityEngine.UI;

namespace FishingGame
{
    public class Player : MonoSingleton<Player>
    {
        public Character playerController;
        [SerializeField] private Image _castStrengthImage;
        [SerializeField] private Bobber _bobber;
        private float castStrength;
        private float _castStartTime;
        private const float PERFECT_CAST_TIME = 1.25f;
        private const float PERFECT_CAST_CUSHION = .01f;
        private bool _overshotCast = false;
        
        private void Update()
        {
            if (playerController != null)
            {
                playerController.moveInput = GameControls.DirectionalInput;
            }

            if (GameControls.CastLine.IsPressed)
            {
                _castStartTime = Time.time;
                castStrength = 0f;
                _overshotCast = false;
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

                float strength = Mathf.Max(castStrength * 8f * (isPerfectCast ? 1.25f : 1f), .707f);
                Vector2 throwVector = new Vector2(playerController.FlipValue * strength, 5f);
                _bobber.ThrowBobber(playerController.transform.position + new Vector3(0f, 1.5f), throwVector);
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