using DG.Tweening;
using UnityEngine;

namespace App.Scripts.Scenes.MainScene.Entities
{
    public class DoorAnimation : MonoBehaviour
    {
        [SerializeField] private Transform _door;
        [SerializeField] private Vector3 _newEulerAngles;
        [SerializeField] private float _duration;
        [SerializeField] private Ease _ease;

        private Sequence _moveSequence;
        
        public void Play()
        {
            _moveSequence?.Kill();
            
            _moveSequence = DOTween.Sequence();
            _moveSequence.Append(_door.DOLocalRotate(_newEulerAngles, _duration));
            _moveSequence.Append(_door.DOLocalRotate(Vector3.zero, _duration));
            _moveSequence.Play();
        }
    }
}