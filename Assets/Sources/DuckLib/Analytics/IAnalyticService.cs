using System.Collections.Generic;

namespace DuckLib.Analytics
{
    public interface IAnalyticService
    {
        void LogEvent(string eventName, IDictionary<string, object> eventData = null);
    }
}