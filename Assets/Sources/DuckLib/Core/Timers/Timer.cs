using System;

namespace DuckLib.Core.Timers
{
    public class Timer
    {
        public float RemainingSeconds { get; private set; }

        public Timer(float duration) => RemainingSeconds = duration;

        public event Action OnTimerEnd;

        public void UpdateDuration(float duration) => RemainingSeconds = duration;

        public void Tick(float deltaTime)
        {
            if (RemainingSeconds < 0f) { return; }
            RemainingSeconds -= deltaTime;
            CheckForTimerEnd();
        }
        private void CheckForTimerEnd()
        {
            if (RemainingSeconds > 0f) { return; }
            RemainingSeconds = 0f;
            OnTimerEnd?.Invoke();
        }
    }
}