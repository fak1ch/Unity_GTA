using App.Scripts.General.Singleton;
using DG.Tweening;
using UnityEngine;

namespace App.Scripts.General.UI.ButtonSpace
{
    public class ButtonClickSoundHandler : MonoSingleton<ButtonClickSoundHandler>
    {
        [SerializeField] private AudioSource _audioSource;
        
        public void PlayCustomButtonClickSound()
        {
            _audioSource.DORestart();
            _audioSource.Play();
        }
    }
}