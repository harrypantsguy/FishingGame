using UnityEngine;

namespace FishingGame.Development
{
    public class FishingLine : MonoBehaviour
    {
        [SerializeField] private Bobber _bobber;
        [SerializeField] private Lure _lure;
        [SerializeField] private Transform _fishingRodTip;

        private LineRenderer _lineRenderer1;

        private void Start()
        {
            _lineRenderer1 = GetComponent<LineRenderer>();
        }

        private void LateUpdate()
        {
            _lineRenderer1.SetPositions(new[]
            {
                _lure.transform.position,
                _bobber.transform.position + (_bobber.Collider.radius * -_bobber.transform.up),
            });
        }
    }
}