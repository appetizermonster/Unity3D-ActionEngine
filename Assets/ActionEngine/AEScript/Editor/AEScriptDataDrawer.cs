using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

namespace ActionEngine {

	[CustomPropertyDrawer(typeof(AEScriptData))]
	public class AEScriptDataDrawer : PropertyDrawer {
		private const int TEXT_HEIGHT = 18;

		public override void OnGUI (Rect position, SerializedProperty property, GUIContent label) {
			EditorGUI.BeginProperty(position, label, property);

			var labelPosition = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

			var oldIndent = EditorGUI.indentLevel;
			EditorGUI.indentLevel = 0;

			const int INDENT = 30;
			const int LABEL_WIDTH = 50;

			var keyRect = new Rect(position.x + INDENT + LABEL_WIDTH, labelPosition.y + TEXT_HEIGHT, position.width - INDENT - LABEL_WIDTH, TEXT_HEIGHT);
			var typeRect = keyRect; typeRect.y += TEXT_HEIGHT;
			var valueRect = typeRect; valueRect.y += TEXT_HEIGHT;

			var keyLabelRect = new Rect(position.x + INDENT, keyRect.y, LABEL_WIDTH, TEXT_HEIGHT);
			var typeLabelRect = keyLabelRect; typeLabelRect.y += TEXT_HEIGHT;
			var valueLabelRect = typeLabelRect; valueLabelRect.y += TEXT_HEIGHT;

			var keyProp = property.FindPropertyRelative("key");
			var typeProp = property.FindPropertyRelative("type");

			EditorGUI.PrefixLabel(keyLabelRect, new GUIContent("Key"));
			EditorGUI.PropertyField(keyRect, keyProp, GUIContent.none);

			EditorGUI.PrefixLabel(typeLabelRect, new GUIContent("Type"));
			EditorGUI.PropertyField(typeRect, typeProp, GUIContent.none);

			try {
				var typeEnumString = typeProp.enumNames[typeProp.enumValueIndex];
				// Unity strips '@' prefix for naming variables, so we don't need to add '@' prefix
				var valueVariableName = typeEnumString.ToLowerInvariant();

				var valueProp = property.FindPropertyRelative(valueVariableName);
				EditorGUI.PrefixLabel(valueLabelRect, new GUIContent("Value"));
				EditorGUI.PropertyField(valueRect, valueProp, GUIContent.none, true);
			} catch (Exception ex) {
				Debug.LogException(ex);
			}

			EditorGUI.indentLevel = oldIndent;
			EditorGUI.EndProperty();
		}

		public override float GetPropertyHeight (SerializedProperty property, GUIContent label) {
			return base.GetPropertyHeight(property, label) + TEXT_HEIGHT * 3;
		}
	}
}
