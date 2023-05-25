using TMPro;
using UnityEngine;

namespace App.Scripts.Scenes.MainScene.Entities.Interact
{
    public class InteractView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _interactMessage;

        public void SetInteractMessage(string message)
        {
            _interactMessage.text = message;
        }
    }
}