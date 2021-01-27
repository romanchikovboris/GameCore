using System;

namespace Romanchikov.GameCore
{
    public interface IUpdateProcessor
    {
        event Action<float> TimeScaleChanged;
        
        float TimeScale { get; set; }
 
        void SetTimeScale(float newTimeScale);
        
        void SubscribeToUpdate(Action<float> action);
        void UnsubsribeToUpdate(Action<float> action);
        void SubscribeToFixedUpdate(Action<float> action);
        void UnsubsribeToFixedUpdate(Action<float> action);
        
        
    }
}