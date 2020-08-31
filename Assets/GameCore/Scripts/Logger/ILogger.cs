namespace Romanchikov.GameCore
{
public enum LogLevel
    {
        None, 
        Debug,
        Info,
        Warning,
        Error
    }
    
    public interface ILogger
    {
        void Debug(string message);
        void Info(string message);
        void Error(string message);
        void Warning(string message);
        
        void Write(string message, LogLevel logLevel);
    }
}