using System;
using System.Collections;
using UnityEngine;

namespace Romanchikov.GameCore
{
    public interface IAsyncProcessor
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
        void StopCoroutine(Coroutine coroutine);
        void DoWithDelay(Action func, float delay);
        void DoAtNextFrame(Action func);
        IEnumerator DoAtNextFrameCoroutine(Action func);
    }
}