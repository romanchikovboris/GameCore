using System;
using System.Collections.Generic;

namespace Romanchikov.GameCore
{
    public interface IBlockable<T>
    {
        event Action StartLock;
        event Action StopLock;

        event Action<T> LockObjectAdded;
        event Action<T> LockObjectRemoved;
        
        
        bool IsActive { get; }
        bool IsBlocked { get; }
        
        void AddBlock(T obj);
        void RemoveBlock(T obj);
        IEnumerable<T> GetBlockObjects();
    }
}