using System;

namespace Romanchikov.GameCore
{
    public interface ITimer
    {
        event Action<float> SecondsChanged;
        event Action TimesUp;
        event Action AllCompleted;
        
        float RemainTime { get; }
        bool IsPaused { get; }
        bool IsCompleted { get; }
   
        void Start();  
        void Stop();  
        void Pause(); 
        void Continue(); 
        void Restart(); 

        ITimer WithTime(float time);
        ITimer WithLoopsCount(int loop);
        ITimer WithInfinityLoops();

    }
}