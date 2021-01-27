using System;
using Kingdoms;
using Romanchikov.GameCore;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace _Kingdoms.Scripts
{
    public class TimeTest : MonoBehaviour
    {
        [SerializeField] private float time = 1;
        [SerializeField] private int loopCount;
        [SerializeField] private bool isInfinityLoop;
        
        private ITimer timer;

        [Inject]
        void Construct(IUpdateProcessor updateProcessor)
        {
            timer = new CoroutineTimer(updateProcessor).WithTime(time);
            if (isInfinityLoop)
                timer.WithInfinityLoops();
            else
                timer.WithLoopsCount(loopCount);
        }

        private void Start()
        {
            timer.SecondsChanged += f => Debug.Log(f);
            timer.TimesUp += () => Debug.Log("Times up");
            timer.AllCompleted += () => Debug.Log("All completed");
        }

        [Button]
        private void Run()
        {
            timer.Start();
        }

        [Button]
        void Stop()
        {
            timer.Stop();
        }

        [Button]
        void Pause()
        {
            timer.Pause();
        }

        [Button]
        void Continue()
        {
            timer.Continue();
        }

        [Button]
        void Reset()
        {
            timer.Restart();
        }
    }
}