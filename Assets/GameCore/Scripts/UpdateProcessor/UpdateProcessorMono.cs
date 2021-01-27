using System;
using System.Collections.Generic;
using UnityEngine;

namespace Romanchikov.GameCore
{
    public class UpdateProcessorMono : MonoBehaviour, IUpdateProcessor
    {
        public event Action<float> TimeScaleChanged;
        private List<Action<float>> updateActions = new List<Action<float>>();
        private List<Action<float>> fixedUpdateActions = new List<Action<float>>();

        private void Update() => CallAllAction(updateActions, Time.deltaTime);
        private void FixedUpdate() => CallAllAction(fixedUpdateActions, Time.fixedDeltaTime);
 
        float timeScale = 1;
        public float TimeScale { get => timeScale; set => SetTimeScale(value); }

        public void SetTimeScale(float newTimeScale)
        {
            if(timeScale == newTimeScale)
                return;

            timeScale = newTimeScale;
            TimeScaleChanged?.Invoke(timeScale);
        }

        public void SubscribeToUpdate(Action<float> action) => Subscribe(action, updateActions);

        public void UnsubsribeToUpdate(Action<float> action) => Unsubscribe(action, updateActions);

        public void SubscribeToFixedUpdate(Action<float> action) => Subscribe(action, fixedUpdateActions);
        public void UnsubsribeToFixedUpdate(Action<float> action) => Unsubscribe(action, fixedUpdateActions);

        
        
        public void CallAllAction(List<Action<float>> actions, float time)
        {
            time *= TimeScale;
            for (int i = actions.Count - 1; i >= 0; i--)
            {
                var action = actions[i];
                if (action == null)
                {
                    updateActions.RemoveAt(i);
                    continue;
                }
                action?.Invoke(time);
            }
        }

        void Subscribe(Action<float> action, List<Action<float>> actions)
        {
            if(actions.Contains(action))
                return;
            
            actions.Add(action);
        }
        
        void Unsubscribe(Action<float> action, List<Action<float>> actions)
        {
            if(!actions.Contains(action))
                return;
            
            actions.Remove(action);
        }
    }
}