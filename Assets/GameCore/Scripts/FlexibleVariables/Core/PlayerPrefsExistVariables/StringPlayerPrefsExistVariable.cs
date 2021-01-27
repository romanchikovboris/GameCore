using UnityEngine;

namespace Romanchikov.GameCore.FlexibleVariables.Core
{
    public class StringPlayerPrefsExistVariable : PlayerPrefsExistVariable<string>
    {
        protected override string Load() => PlayerPrefs.GetString(prefsName);
        protected override void Save(string value) => PlayerPrefs.SetString(prefsName, value);

        public StringPlayerPrefsExistVariable(string prefsName) : base(prefsName)
        {
        }
    }
}