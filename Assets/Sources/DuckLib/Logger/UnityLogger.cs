using UnityEngine;

namespace DuckLib.Logger
{
    public class UnityLogger : ILogger
    {
        public void Log(object message)
        {
            Debug.Log(message);
        }

        public void LogError(object error)
        {
            Debug.LogError(error);
        }
    }
}