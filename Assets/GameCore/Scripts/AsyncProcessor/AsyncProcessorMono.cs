using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Romanchikov.GameCore
{
    public class AsyncProcessorMono : MonoBehaviour, IAsyncProcessor
    {
        public Coroutine StartCoroutine(IEnumerator coroutine) => base.StartCoroutine(coroutine);
        public void StopCoroutine(Coroutine coroutine) => this.StopCoroutine(coroutine);


        public void DoWithDelay(Action func, float delay)
        {
            if (delay <= 0)
                func();
            else
                StartCoroutine(DoWithDelayCoroutine(func, delay));
        }

        public void DoAtNextFrame(Action func) => StartCoroutine(DoAtNextFrameCoroutine(func));

        IEnumerator DoWithDelayCoroutine(Action func, float delay)
        {
            yield return new WaitForSeconds(delay);
            func?.Invoke();
        }

        public IEnumerator DoAtNextFrameCoroutine(Action func)
        {
            yield return null;
            func();
        }
    }
}
