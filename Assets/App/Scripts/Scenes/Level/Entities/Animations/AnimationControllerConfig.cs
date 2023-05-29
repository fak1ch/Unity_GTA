using System;

namespace App.Scripts.Scenes.MainScene.Entities
{
    [Serializable]
    public class AnimationControllerConfig
    {
        public string IsRunKey = "IsRun";
        public string SpeedPercentKey = "SpeedPercent";
        public string WalkSpeedPercentKey = "WalkSpeedPercent";
        public string EnterCarTriggerKey = "EnterCarTrigger";
        public string ExitCarTriggerKey = "ExitCarTrigger";
        public string IsTakeAimKey = "IsTakeAim";
        public string IsGroundKey = "IsGround";
        public string JumpTriggerKey = "JumpTrigger";
        public string ThrowGrenadeTriggerKey = "ThrowGrenadeTriggerKey";
        public string FootAttackTriggerKey = "FootAttackTrigger";
        public string HandAttackTriggerKey = "HandAttackTrigger";
    }
}