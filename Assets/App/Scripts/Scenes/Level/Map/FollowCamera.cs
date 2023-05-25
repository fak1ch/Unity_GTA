using UnityEngine;

namespace App.Scripts.Scenes.MainScene.Map
{
    public class FollowCamera : MonoBehaviour
    {
        public Vector2 LeftBottomPoint { get; private set; }
        public Vector2 RightTopPoint { get; private set; }
        
        [SerializeField] private Camera _mainCamera;

        private Transform _target;

        public void SetTarget(Transform target)
        {
            _target = target;
        }

        public void RecalculateBounds()
        {
            LeftBottomPoint = _mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
            RightTopPoint = _mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
        }
    }
}