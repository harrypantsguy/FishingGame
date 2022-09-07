using UnityEngine;

namespace FishingGame.Development.RopePhysics
{
    [RequireComponent(typeof(LineRenderer))]
    public sealed class PhysicsRope : MonoBehaviour
    {
        [SerializeField] private int _ropeLength;
        [SerializeField] private float _segmentLength;
        [SerializeField] private GameObject _physicsRopeSegmentPrefab;

        private static readonly Vector2 _Gravity = new Vector2(0f, -2f);
        
        private PhysicsRopeSegment[] _segments;
        private LineRenderer _lineRenderer;

        private void Start()
        {
            _segments = new PhysicsRopeSegment[_ropeLength];

            for (var i = 0; i < _ropeLength; i++)
            {
                var pos = new Vector2(0f, -i * _segmentLength);

                var segment = Instantiate(_physicsRopeSegmentPrefab, pos, Quaternion.identity).GetComponent<PhysicsRopeSegment>();
                segment.PreviousPosition = pos;
                
                _segments[i] = segment;
            }
            
            _lineRenderer = GetComponent<LineRenderer>();
            _lineRenderer.positionCount = _ropeLength;
        }

        private void FixedUpdate()
        {
            Simulate();
            Render();
        }

        private void Simulate()
        {
            for (var i = 0; i < _segments.Length; i++)
            {
                var segment = _segments[i];
                
                if (i == 0)
                {
                    segment.Position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    continue;
                }
                
                segment.Velocity = segment.Position - segment.PreviousPosition;
                segment.PreviousPosition = segment.Position;
                segment.Position += segment.Velocity;
                segment.Position += _Gravity * Time.fixedDeltaTime;
            }

            for (var i = 0; i < 50; i++)
                ApplyConstraint();
        }

        private void ApplyConstraint()
        {
            for (var i = 0; i < _segments.Length - 1; i++)
            {
                var node = _segments[i];
                var nextNode = _segments[i + 1];
                
                var distToNextNode = Vector2.Distance(node.Position, nextNode.Position);
                var errorDist = Mathf.Abs(distToNextNode - _segmentLength);
                var errorDir = (node.Position - nextNode.Position).normalized * (distToNextNode > _segmentLength ? 1f : -1f);
                var correction = errorDir * errorDist;

                if (i != 0)
                {
                    node.Position -= correction * .5f;
                    nextNode.Position += correction * .5f;
                }
                else
                    nextNode.Position += correction;
            }
        }

        private void Render()
        {
            for (var i = 0; i < _segments.Length; i++)
                _lineRenderer.SetPosition(i, _segments[i].Position);
        }
    }
}