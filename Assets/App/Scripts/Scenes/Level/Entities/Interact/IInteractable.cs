namespace App.Scripts.Scenes.MainScene.Entities.Interact
{
    public interface IInteractable
    {
        public void Interact(Character character);
        public string GetInteractMessage();
    }
}