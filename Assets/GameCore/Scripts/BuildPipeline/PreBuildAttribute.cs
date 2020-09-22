namespace Romanchikov.GameCore
{
    public class PreBuildAttribute : System.Attribute
    {
        public string FunctionName { get; private set; }

        public PreBuildAttribute(string functionName)
        {
            FunctionName = functionName;
        }
    }
}