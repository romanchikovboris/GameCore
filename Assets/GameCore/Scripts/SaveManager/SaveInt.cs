using UnityEngine;

namespace Romanchikov.GameCore.SaveManager
{
    public class SaveInt
    {
        private int value = -1;
        private string name;
        private int defaultValue;

        public SaveInt(string name, int defaultValue)
        {
            this.name = name;
            this.defaultValue = defaultValue;
        }

        public int GetValue()
        {
            if (value < 0)
                value = PlayerPrefs.GetInt(name, defaultValue);
            return value;
        }

        public void SetValue(int value)
        {
            if (this.value == value)
                return;

            this.value = value;
            PlayerPrefs.SetInt(name, value);
        }
    }
}