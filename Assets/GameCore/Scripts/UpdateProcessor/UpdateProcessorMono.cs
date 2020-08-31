using System;
using System.Collections.Generic;
using UnityEngine;

namespace Romanchikov.GameCore
{
    public class UpdateProcessorMono : MonoBehaviour, IUpdateProcessor
    {
        private List<Action> updateActions = new List<Action>();
        private List<Action> fixedUpdateActions = new List<Action>();
        
        private void Update() => CallAllAction(updateActions);
        private void FixedUpdate() => CallAllAction(fixedUpdateActions);


        public void SubscribeToUpdate(Action action) => Subscribe(action, updateActions);
        public void UnsubsribeToUpdate(Action action) => Unsubscribe(action, updateActions);
        public void SubscribeToFixedUpdate(Action action) => Subscribe(action, fixedUpdateActions);
        public void UnsubsribeToFixedUpdate(Action action) => Unsubscribe(action, fixedUpdateActions);

        
        
        public void CallAllAction(List<Action> actions)
        {
            for (int i = actions.Count - 1; i >= 0; i--)
            {
                var action = actions[i];
                if (action == null)
                {
                    updateActions.RemoveAt(i);
                    continue;
                }
                action?.Invoke();
            }
        }

        void Subscribe(Action action, List<Action> actions)
        {
            if(actions.Contains(action))
                return;
            
            actions.Add(action);
        }
        
        void Unsubscribe(Action action, List<Action> actions)
        {
            if(!actions.Contains(action))
                return;
            
            actions.Remove(action);
        }
    }
}