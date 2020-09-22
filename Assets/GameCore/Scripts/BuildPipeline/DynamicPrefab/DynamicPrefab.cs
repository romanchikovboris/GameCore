using System; 
using System.IO;
using UnityEngine; 
#if UNITY_EDITOR
using UnityEditor;
#endif


namespace Romanchikov.GameCore
{
    [Serializable]
    [PreBuild("PrepareBuild")]
    public class DynamicPrefab<T> where T : UnityEngine.Object
    {
#if UNITY_EDITOR
        [SerializeField] private T prefab;
        [SerializeField] private bool isDynamic;
#endif
        [HideInInspector] [SerializeField] private string prefabPath;
        [HideInInspector] [SerializeField] private T buildPrefab;
        public T Prefab => GetPrefab();

        public T GetPrefab()
        {
#if UNITY_EDITOR
            return prefab;
#endif
            if (buildPrefab != null)
                return buildPrefab;
            else
                return Resources.Load<T>(prefabPath);
        }

#if UNITY_EDITOR
        void PrepareBuild()
        {
            if(isDynamic)
                PreparePath();
            else
                PreparePrefab();
           
        }

        void PreparePath()
        {
            buildPrefab = null;
            prefabPath = AssetDatabase.GetAssetPath(prefab);
            var isValid = prefabPath.Contains($"{Path.AltDirectorySeparatorChar}Resources{Path.AltDirectorySeparatorChar}");
            if(!isValid)
                throw new Exception($"Prefab {prefabPath} are not in Resources folder");
            Debug.Log($"Prepared path {prefabPath}");
        }

        void PreparePrefab()
        {
            prefabPath = null;
            buildPrefab = prefab;
            Debug.Log($"Prepared prefab {buildPrefab.name}");
        }
#endif
    }

}
