using UnityEngine;

namespace Romanchikov.GameCore.FlexibleVariables.Core
{
    public class FloatPlayerPrefsExistVariable : PlayerPrefsExistVariable<float>
    {
        protected override float Load() => PlayerPrefs.GetFloat(prefsName);
        protected override void Save(float value) => PlayerPrefs.SetFloat(prefsName, value);

        public FloatPlayerPrefsExistVariable(string prefsName) : base(prefsName)
        {
        }
    }
}