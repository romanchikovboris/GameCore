using Romanchikov.GameCore.FlexibleVariables.Core;
using UnityEngine;

namespace Romanchikov.GameCore.FlexibleVariables
{
    [CreateAssetMenu(fileName = "flexible bool", menuName = "Flexible variables/Bool")]
    public class FlexibleBool: FlexibleVariable<bool>
    {
        
        private BoolPlayerPrefsExistVariable prefsVar;
        
        protected override PlayerPrefsExistVariable<bool> GetPlayerPrefsExistVariable()
        {
            if(prefsVar == null)
                prefsVar = new BoolPlayerPrefsExistVariable(prefsName);
            return prefsVar; 
        }
         
    }
}