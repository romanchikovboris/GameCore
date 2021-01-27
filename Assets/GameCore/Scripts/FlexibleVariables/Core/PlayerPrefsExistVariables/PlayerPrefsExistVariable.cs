using System;
using UnityEngine; 

namespace Romanchikov.GameCore.FlexibleVariables.Core
{
    public abstract class PlayerPrefsExistVariable<T> : IExistVariable<T>
    {
        public bool IsExist => PlayerPrefs.HasKey(prefsName);
        public T Value { get => GetValue(); set => SetValue(value); }
 
        private T _cacheValue;
        private bool isLoaded;
 
        protected string prefsName;
        
        protected PlayerPrefsExistVariable(string  prefsName)
        {
            this.prefsName = prefsName;
        }
         
        public T GetValue()
        { 
            if (!Application.isPlaying)
                return Load();
              
            if (isLoaded)
                return _cacheValue; 
             
            _cacheValue = Load();
            isLoaded = true;  
            return _cacheValue; 
        }

        public void SetValue(T value)
        {
            if(_cacheValue.Equals(value))
                return;

            _cacheValue = value;
            Save(value);
        }

        public void Clear() => PlayerPrefs.DeleteKey(prefsName);

        protected abstract T Load();
        protected abstract void Save(T value);
        
    }
}