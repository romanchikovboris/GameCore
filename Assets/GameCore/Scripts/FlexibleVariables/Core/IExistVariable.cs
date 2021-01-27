namespace Romanchikov.GameCore.FlexibleVariables.Core
{
    interface IExistVariable<T>
    {
        bool IsExist { get; }
        T Value { get; set; }

        T GetValue();
        void SetValue(T value);
    }
}