#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using Object = UnityEngine.Object;

namespace Romanchikov.GameCore
{
    [InitializeOnLoad]
    public static class BuildPipelineController
    {
        private static List<int> calledObjectsHash = new List<int>();
        static BuildPipelineController()
        {
            BuildPlayerWindow.RegisterBuildPlayerHandler(OnBuild);
        }

        private static void OnBuild(BuildPlayerOptions options)
        { 
            var prebuildersGUIDS = AssetDatabase.FindAssets($"t:{typeof(Object)}");

            foreach (var guid in prebuildersGUIDS)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                var asset = AssetDatabase.LoadAssetAtPath<Object>(path);
                CallPreBuildRecursively(asset); 
            }

            calledObjectsHash.Clear();
          // BuildPipeline.BuildPlayer(options);
        }

        static void CallPreBuildRecursively(object obj)
        {
            if(obj == null)
                return;
            
            if(calledObjectsHash.Contains(obj.GetHashCode()))
                return;
            
            calledObjectsHash.Add(obj.GetHashCode());
            
            var t = obj.GetType();
            var methods = (from a in t.GetCustomAttributes(true)
                where a is PreBuildAttribute
                select t.GetMethod(((PreBuildAttribute)a).FunctionName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
                .Where(mi => mi != null);
                        
            foreach (var method in methods) 
                method.Invoke(obj, null);

            foreach (var field in t.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
                CallPreBuildRecursively(field.GetValue(obj));
        }
        
    }
}
#endif