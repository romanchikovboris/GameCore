using UnityEngine;

namespace Romanchikov.GameCore.FlexibleVariables.Core
{
    public class BoolPlayerPrefsExistVariable : PlayerPrefsExistVariable<bool>
    {
        protected override bool Load() => PlayerPrefs.GetInt(prefsName) == 1;
        protected override void Save(bool value) => PlayerPrefs.SetInt(prefsName, value ? 1 : 0);

        public BoolPlayerPrefsExistVariable(string prefsName) : base(prefsName)
        {
        }
    }
}