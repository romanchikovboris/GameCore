using System;

namespace Romanchikov.GameCore
{
    public interface IUpdateProcessor
    {
        void SubscribeToUpdate(Action action);
        void UnsubsribeToUpdate(Action action);
        void SubscribeToFixedUpdate(Action action);
        void UnsubsribeToFixedUpdate(Action action);
    }
}