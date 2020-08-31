using NotImplementedException = System.NotImplementedException;

namespace Romanchikov.GameCore
{
    public class UnityLogger: ILogger
    {
        public void Debug(string message)
        {
            UnityEngine.Debug.Log(message);
        }

        public void Info(string message)
        {
            UnityEngine.Debug.Log(message);
        }

        public void Error(string message)
        {
            UnityEngine.Debug.LogError(message);
        }

        public void Warning(string message)
        {
            UnityEngine.Debug.LogWarning(message);
        }

        public void Write(string message, LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.None: break;
                case LogLevel.Debug: Debug(message); break;
                case LogLevel.Info: Info(message); break;
                case LogLevel.Warning: Warning(message); break;
                case LogLevel.Error: Error(message); break;
            }
        }
    }
}