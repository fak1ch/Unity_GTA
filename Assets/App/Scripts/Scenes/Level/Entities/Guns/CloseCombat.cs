namespace App.Scripts.Scenes.MainScene.Entities.Bullets
{
    public class CloseCombat : Gun
    {
        protected override void SpawnBullet()
        {
            _character.AttackComponent.StartHandAttack();
        }
    }
}