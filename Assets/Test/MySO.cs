using Romanchikov.GameCore;
using UnityEngine;

namespace GameCore
{ 
    [CreateAssetMenu(fileName = "FILENAME", menuName = "MENUNAME", order = 0)]
    public class MySO : ScriptableObject
    {
        [SerializeField] private DynamicPrefab<GameObject> dynamicPrefab;
        public DynamicPrefab<GameObject> DynamicPrefab => dynamicPrefab;
        
    }
}