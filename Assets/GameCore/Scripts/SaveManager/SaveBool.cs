using UnityEngine;

namespace Romanchikov.GameCore.SaveManager
{
    public class SaveBool
    {
        private bool value = false;
        private bool firstLoaded = false;
        private string name;
        private bool defaultValue;

        public SaveBool(string name, bool defaultValue)
        {
            this.name = name;
            this.defaultValue = defaultValue;
        }

        public bool GetValue()
        {
            if (!firstLoaded)
            {
                firstLoaded = true;
                value = PlayerPrefs.GetInt(name, defaultValue ? 1 : 0) == 1;
            }

            return value;
        }

        public void SetValue(bool value)
        {
            if (this.value == value)
                return;

            this.value = value;
            PlayerPrefs.SetInt(name, value ? 1 : 0);
        }
    }
}