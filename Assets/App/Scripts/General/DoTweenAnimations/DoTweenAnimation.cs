using UnityEngine;

namespace App.Scripts.General.DoTweenAnimations
{
    public abstract class DoTweenAnimation : MonoBehaviour
    {
        public abstract void StartAnimation();
        public abstract void Pause();
        public abstract void Kill();

        private void OnDestroy()
        {
            Kill();
        }
    }
}