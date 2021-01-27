using UnityEngine;

namespace Romanchikov.GameCore.FlexibleVariables.Core
{
    public class IntPlayerPrefsExistVariable : PlayerPrefsExistVariable<int>
    {
        protected override int Load() => PlayerPrefs.GetInt(prefsName);
        protected override void Save(int value) => PlayerPrefs.SetInt(prefsName, value);

        public IntPlayerPrefsExistVariable(string prefsName) : base(prefsName)
        {
        }
    }
}