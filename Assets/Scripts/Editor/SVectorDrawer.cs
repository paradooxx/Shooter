using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(SVector))]
public class SVectorDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        SerializedProperty xProp = property.FindPropertyRelative("x");
        SerializedProperty yProp = property.FindPropertyRelative("y");

        Rect labelRect = new Rect(position.x, position.y, EditorGUIUtility.labelWidth, position.height);
        Rect xRect = new Rect(position.x + EditorGUIUtility.labelWidth, position.y, (position.width - EditorGUIUtility.labelWidth) / 2, position.height);
        Rect yRect = new Rect(position.x + EditorGUIUtility.labelWidth + (position.width - EditorGUIUtility.labelWidth) / 2, position.y, (position.width - EditorGUIUtility.labelWidth) / 2, position.height);

        EditorGUI.LabelField(labelRect, label);

        EditorGUI.PropertyField(xRect, xProp, GUIContent.none);
        EditorGUI.PropertyField(yRect, yProp, GUIContent.none);

        EditorGUI.EndProperty();
    }
}
