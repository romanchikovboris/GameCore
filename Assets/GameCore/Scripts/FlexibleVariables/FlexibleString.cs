using Romanchikov.GameCore.FlexibleVariables.Core;
using UnityEngine;

namespace Romanchikov.GameCore.FlexibleVariables
{
    [CreateAssetMenu(fileName = "flexible string", menuName = "Flexible variables/String")]
    public class FlexibleString: FlexibleVariable<string>
    {
          
        private StringPlayerPrefsExistVariable prefsVar;
         
        protected override PlayerPrefsExistVariable<string> GetPlayerPrefsExistVariable()
        { 
            if(prefsVar == null)
                prefsVar = new StringPlayerPrefsExistVariable(prefsName);
            return prefsVar; 
        }
    }
}