namespace Romanchikov.GameCore.FlexibleVariables.Core
{
    class TempExistVariable<T> : IExistVariable<T>
    {
        public bool IsExist { get; private set; }
        public T Value { get =>  GetValue(); set => SetValue(value); }

        private T _value;
        
        public T GetValue() => _value;
        public void SetValue(T value)
        {
            IsExist = true;
            _value = value;
        }

        public void Clear() => IsExist = false;
    }
}