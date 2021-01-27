using Romanchikov.GameCore.FlexibleVariables.Core;
using UnityEngine;

namespace Romanchikov.GameCore.FlexibleVariables
{
    [CreateAssetMenu(fileName = "flexible float", menuName = "Flexible variables/Float")]
    public class FlexibleFloat: FlexibleVariable<float>
    {
        
        private FloatPlayerPrefsExistVariable prefsVar;
        protected override PlayerPrefsExistVariable<float> GetPlayerPrefsExistVariable()
        {
            if(prefsVar == null)
                prefsVar = new FloatPlayerPrefsExistVariable(prefsName);
            return prefsVar;
        }
         
    }
}