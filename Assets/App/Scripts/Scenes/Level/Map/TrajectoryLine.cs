using UnityEngine;

namespace App.Scripts.Scenes.MainScene.Map
{
    public class TrajectoryLine : MonoBehaviour
    {
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private int _lineSegments = 60;
        [SerializeField] private float _timeOfTheFlight = 5;
        
        public void ShowTrajectoryLine(Vector3 startPoint, Vector3 startVelocity)
        {
            float timeStep = _timeOfTheFlight / _lineSegments;
            
            Vector3[] lineRendererPoints = CalculateTrajectoryLine(startPoint, startVelocity, timeStep);
            
            _lineRenderer.positionCount = _lineSegments;
            _lineRenderer.SetPositions(lineRendererPoints);
        }

        private Vector3[] CalculateTrajectoryLine(Vector3 startPoint, Vector3 startVelocity, float timeStep)
        {
            Vector3[] lineRendererPoints = new Vector3[_lineSegments];

            lineRendererPoints[0] = startPoint;

            for (int i = 1; i < _lineSegments; i++)
            {
                float timeOffset = timeStep * i;

                Vector3 progressBeforeGravity = startVelocity * timeOffset;
                Vector3 gravityOffset = Vector3.up * -0.5f * Physics.gravity.y * timeOffset * timeOffset;
                Vector3 newPosition = startPoint + progressBeforeGravity - gravityOffset;
                lineRendererPoints[i] = newPosition;
            }

            return lineRendererPoints;
        }

        public void SetActive(bool value)
        {
            _lineRenderer.enabled = value;
        }
    }
}