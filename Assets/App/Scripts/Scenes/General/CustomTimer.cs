using System;
using UnityEngine;

namespace App.Scripts.Scenes.General
{
    public class CustomTimer
    {
        public event Action OnStart;
        public event Action OnEnd;
        public event Action OnBreak;
        public event Action<float> OnTick;

        public bool TimerStarted { get; private set; } = false;
        public float MaxTime => _time;
        public float CurrentTime => _timeTemp;

        private float _time;
        private float _timeTemp;

        public void Tick(float deltaTime)
        {
            if (TimerStarted == false) return;
            
            _timeTemp -= deltaTime;
            OnTick?.Invoke(_timeTemp);
            if (_timeTemp <= 0)
            {
                EndTimer();
            }
        }

        public void StartTimer(float time)
        {
            _time = time;
            _timeTemp = _time;
            
            TimerStarted = true;
            OnStart?.Invoke();
        }

        public void BreakTimer()
        {
            TimerStarted = false;
            OnBreak?.Invoke();
        }
        
        private void EndTimer()
        {
            TimerStarted = false;
            OnEnd?.Invoke();
        }
    }
}