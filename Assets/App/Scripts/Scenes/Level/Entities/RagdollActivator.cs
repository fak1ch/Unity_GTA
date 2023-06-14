using System.Collections.Generic;
using UnityEngine;

namespace App.Scripts.Scenes.MainScene.Entities
{
    public class RagdollActivator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private List<Rigidbody> _ragdollRigidbodies;
        [SerializeField] private List<Collider> _ragdollColliders;
        [SerializeField] private Collider _mainCollider;
        [SerializeField] private Rigidbody _mainRigidbody;

        public void SetActive(bool value)
        {
            _animator.enabled = !value;
            _mainCollider.enabled = !value;
            _mainRigidbody.isKinematic = value;
            
            foreach (var ragdollCollider in _ragdollColliders)
            {
                ragdollCollider.enabled = value;
            }

            foreach (var ragdollRigidbody in _ragdollRigidbodies)
            {
                ragdollRigidbody.isKinematic = !value;
            }
        }
    }
}