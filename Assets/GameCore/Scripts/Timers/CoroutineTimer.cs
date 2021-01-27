using System;
using System.Collections;
using System.Timers;
using Kingdoms;
using Romanchikov.GameCore;
using UnityEngine;

namespace Romanchikov.GameCore
{
    public class CoroutineTimer : ITimer
    {
        public event Action<float> SecondsChanged;
        public event Action TimesUp;
        public event Action AllCompleted;
        public float RemainTime { get; private set; }
        public bool IsPaused { get; private set; }
        public bool IsCompleted { get; private set; }
        public int LoopCount { get; private set; }

        private float time;
        private int totalLoopCount;
        private IUpdateProcessor updateProcessor;
        private float lastFrameTime;
 
        
        public CoroutineTimer(IUpdateProcessor updateProcessor )
        {
            this.updateProcessor = updateProcessor;
        }


        public void Start() => updateProcessor.SubscribeToUpdate(Update);

        public void Stop()
        {
            Complete();
            Restart();
        }

        public void Pause() => IsPaused = true;
        public void Continue() => IsPaused = false;

        public void Restart()
        {
            RemainTime = time;
            LoopCount = totalLoopCount;
            IsCompleted = false;
        }

        public ITimer WithTime(float time)
        {
            RemainTime = this.time = time;  
            return this;
        }
        
        
        public ITimer WithLoopsCount(int loop)
        {
            LoopCount = totalLoopCount = Mathf.Max(1, loop);
            return this;
        }

        public ITimer WithInfinityLoops()
        {
            LoopCount = totalLoopCount = -1;
            return this;
        }


        void Update(float deltaTime)
        {
            if (RemainTime <= 0)
                return;
            
            if(IsPaused)
                return;
            
            if(IsCompleted)
                return;
              
            RemainTime -= deltaTime;
            RemainTime = Mathf.Max(0, RemainTime);
            
            var currentSeconds = Mathf.FloorToInt(RemainTime) + 1;
            var wasSeconds = Mathf.FloorToInt(lastFrameTime) + 1;
            
            if(currentSeconds != wasSeconds)
                SecondsChanged?.Invoke(currentSeconds);

            if (RemainTime == 0)
            { 
                SecondsChanged?.Invoke(0);
                TimesUp?.Invoke();
                
                if (LoopCount > 0)
                    LoopCount--;

                if (LoopCount != 0)
                    RemainTime = time;
                else
                    Complete();

            }

            lastFrameTime = RemainTime;
        }


        void Complete()
        {
            IsCompleted = true;
            updateProcessor.UnsubsribeToUpdate(Update);
            AllCompleted?.Invoke();
        }
    }
}