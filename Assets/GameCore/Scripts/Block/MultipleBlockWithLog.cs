using System;
using System.Collections.Generic;
using System.Text;

namespace Romanchikov.GameCore
{
    public class MultipleBlockWithLog<T> : IBlockable<T>
    {
        public event Action StartLock;
        public event Action StopLock;
        
        public event Action<T> LockObjectAdded;
        public event Action<T> LockObjectRemoved;

        public bool IsActive => multipleBlock.IsActive;
        public bool IsBlocked => multipleBlock.IsBlocked;
        
        
        
        private MultipleBlock<T> multipleBlock;

        private ILogger logger;
        private LogLevel logLevel;
        private string logName;
        
        
        
        public MultipleBlockWithLog(IEnumerable<T> initBlockObjects, ILogger logger, LogLevel logLevel, string logName)
        {
            this.logger = logger;
            this.logLevel = logLevel;
            this.logName = logName;
            
            multipleBlock = new MultipleBlock<T>(initBlockObjects);
            
            multipleBlock.LockObjectAdded += OnLockObjectAdded;
            multipleBlock.LockObjectRemoved += OnLockObjectRemoved;
            multipleBlock.StartLock += OnStartLock;
            multipleBlock.StopLock += OnStopLock;
        }
        
        public MultipleBlockWithLog(ILogger logger, LogLevel logLevel, string logName) : this(null, logger, logLevel, logName){}

        
        public void AddBlock(T obj) => multipleBlock.AddBlock(obj);
        public void RemoveBlock(T obj) => multipleBlock.RemoveBlock(obj);
        public IEnumerable<T> GetBlockObjects() => multipleBlock.GetBlockObjects();
        
        
        private void OnLockObjectAdded(T obj)
        {
            logger?.Write($"{logName}. Added block {obj}. Remain blocks: {GetBlockedObjectNames()}", logLevel);
            LockObjectAdded?.Invoke(obj);
        }

        private void OnLockObjectRemoved(T obj)
        {
            logger?.Write($"{logName}. Remove block {obj}. Remain blocks: {GetBlockedObjectNames()}", logLevel);
            LockObjectRemoved?.Invoke(obj);
        }

        private void OnStartLock()
        {
            logger?.Write($"{logName}. Locked", logLevel);
            StartLock?.Invoke();
        }
        
        private void OnStopLock()
        {
            logger?.Write($"{logName}. Unlocked", logLevel);
            StopLock?.Invoke();
        }
        
        string GetBlockedObjectNames()
        {
            if (multipleBlock.IsActive)
                return $"not blocked";

            var stringBuilder = new StringBuilder(); 
            foreach (var o in multipleBlock.GetBlockObjects())
                stringBuilder.Append($"{o?.ToString()}, ");

            if(stringBuilder.Length > 2)
                stringBuilder.Length -= 2;

            return stringBuilder.ToString();
        }
 
    }
}