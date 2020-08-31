using System;
using System.Collections;
using System.Collections.Generic; 
using UnityEngine; 

namespace Romanchikov.GameCore
{
    public class MultipleBlock<T> : IBlockable<T>
    {
        public event Action StartLock;
        public event Action StopLock;

        public event Action<T> LockObjectAdded;
        public event Action<T> LockObjectRemoved;

        public List<T> lockObjects = new List<T>();

        public bool IsActive => lockObjects.Count == 0;
        public bool IsBlocked => !IsActive;


        string logName;

        public MultipleBlock(IEnumerable<T> initBlockObjects)
        {
            if(initBlockObjects != null)
                foreach (var initBlockObject in initBlockObjects)
                    AddBlock(initBlockObject);
        }

        public MultipleBlock() : this(null) {}

        public void AddBlock(T obj)
        {
            var wasLock = IsBlocked;
            
            if(lockObjects.Contains(obj))
                return;
          
            lockObjects.Add(obj);
            
            LockObjectAdded?.Invoke(obj);

            if (!wasLock)
                StartLock?.Invoke();
        }

        public void RemoveBlock(T obj)
        {
            var wasLock = IsBlocked;

            if (lockObjects.Contains(obj))
                lockObjects.Remove(obj);

            LockObjectRemoved?.Invoke(obj); 

            if (wasLock && !IsBlocked)
                StopLock?.Invoke();
        }

        

        public IEnumerable<T> GetBlockObjects() => lockObjects;
    }
}