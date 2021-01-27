using System;

#if UNITY_EDITOR 
using UnityEditor;
#endif

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif
using UnityEngine;

namespace Romanchikov.GameCore.FlexibleVariables.Core
{
    [System.Serializable]
    public abstract class FlexibleVariable<T> : ScriptableObject
    { 
        public event Action<T> Changed;
 
        [SerializeField] private T baseValue;

        public T BaseValue => baseValue;
        
        #if ODIN_INSPECTOR
        [ShowInInspector] [ReadOnly] 
        #endif
        public T Value {get => GetValue(); set => SetValue(value);} 
        
        
        [SerializeField] [HideInInspector] protected string prefsName;
 
        private TempExistVariable<T> tempVariable = new TempExistVariable<T>();
        private PlayerPrefsExistVariable<T> SaveVariable => GetPlayerPrefsExistVariable();

        public void OnValidate()
        { 
#if UNITY_EDITOR  
            if (Event.current != null)
                switch (Event.current.commandName)
                {
                    case "Duplicate":
                    case "Paste":
                        prefsName = null; 
                        break;
                }
#endif
            if(string.IsNullOrEmpty(prefsName))
                prefsName = $"{nameof(T)}_{DateTime.Now.ToShortDateString()}_{ new System.Random().Next()}";
        }
 
        public T GetValue()
        {
             
            if (tempVariable.IsExist)
                return tempVariable.GetValue();
 
            if (SaveVariable.IsExist)
                return SaveVariable.GetValue();
 
            return baseValue;
        }
                
#if ODIN_INSPECTOR
        [Button]   
#endif
        public void SetValue(T value)
        { 
            if(SaveVariable.GetValue().Equals(value))
                return;
            
            SaveVariable.SetValue(value);
            Changed?.Invoke(value);
        }
        
#if ODIN_INSPECTOR
        [Button]   
#endif
        public void SetTempValue(T value)
        {
            if(tempVariable.Equals(value))
                return;
            
            tempVariable.SetValue(value);
            Changed?.Invoke(value);
        }

        
        protected abstract PlayerPrefsExistVariable<T> GetPlayerPrefsExistVariable();
        
#if ODIN_INSPECTOR
        [Button]   
#endif
        public void Clear()
        {
            SaveVariable.Clear();
            tempVariable.Clear();
        }
        
    }
}