using UnityEditor;
using UnityEngine;

namespace Romanchikov.GameCore.Editor
{
    [CustomPropertyDrawer(typeof(DynamicPrefab<>))]
    public class DynamicPrefabDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            
            // Draw label
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
            
            // Don't make child fields be indented
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
            
            var prefixRect = new Rect(position.x, position.y, 10, position.height);
            var toogleRect = new Rect(position.x + 15, position.y, 10, position.height);
            var valueRect = new Rect(position.x + 35, position.y, position.width - 35, position.height);
            
            EditorGUI.PropertyField(valueRect, property.FindPropertyRelative("prefab"), GUIContent.none);
            EditorGUI.LabelField(prefixRect, "D");
            var isDynamic = property.FindPropertyRelative("isDynamic").boolValue;
            isDynamic = EditorGUI.Toggle(toogleRect,  isDynamic);
            property.FindPropertyRelative("isDynamic").boolValue = isDynamic;
            
            
            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }
    }
}