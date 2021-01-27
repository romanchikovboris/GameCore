using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Romanchikov.GameCore.SaveManager
{
    public class SaveManager
    {
        public SaveInt LastCompletedLevel = new SaveInt("LastCompletedLevel", 1);
        public SaveInt CurrentLevel = new SaveInt("CurrentLevel", 1); 

        public int GetLevelStar(int level) => PlayerPrefs.GetInt($"Level star {level}", 0);
        public void SetLevelStar(int level, int star) => PlayerPrefs.SetInt($"Level star {level}", star);
        
        public void ForceSave() => PlayerPrefs.Save();
    }
}
