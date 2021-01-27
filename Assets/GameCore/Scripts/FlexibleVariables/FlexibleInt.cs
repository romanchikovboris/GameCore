using Romanchikov.GameCore.FlexibleVariables.Core;
using UnityEngine;

namespace Romanchikov.GameCore.FlexibleVariables
{
    [CreateAssetMenu(fileName = "flexible int", menuName = "Flexible variables/Int")]
    public class FlexibleInt: FlexibleVariable<int>
    {
        private IntPlayerPrefsExistVariable prefsVar;
        protected override PlayerPrefsExistVariable<int> GetPlayerPrefsExistVariable()
        {
            if(prefsVar == null)
                prefsVar = new IntPlayerPrefsExistVariable(prefsName);
            return prefsVar;
        }
    }
}