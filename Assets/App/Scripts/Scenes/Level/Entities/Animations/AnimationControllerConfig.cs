using System;

namespace App.Scripts.Scenes.MainScene.Entities
{
    [Serializable]
    public class AnimationControllerConfig
    {
        public string IsRunKey = "IsRun";
        public string SpeedPercentKey = "SpeedPercent";
        public string EnterCarTriggerKey = "EnterCarTrigger";
        public string ExitCarTriggerKey = "ExitCarTrigger";
        public string IsTakeAimKey = "IsTakeAim";
    }
}