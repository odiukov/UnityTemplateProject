namespace DuckLib.Logger
{
    public interface ILogger
    {
        void Log(object message);
        void LogError(object error);
    }
}
