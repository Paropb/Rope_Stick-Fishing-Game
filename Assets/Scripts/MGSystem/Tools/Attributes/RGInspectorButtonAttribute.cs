using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;

namespace MyGame.MGSystem
{
    [AttributeUsage(AttributeTargets.Field)]
    public class RGInspectorButtonAttribute : PropertyAttribute
    {
        public readonly string MethodName;
        public RGInspectorButtonAttribute(string MethodName)
        {
            this.MethodName = MethodName;
        }
    }
#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(RGInspectorButtonAttribute))]
    public class InspectorButtonPropertyDrawer : PropertyDrawer
    {
        private MethodInfo _eventMethodInfo = null;
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            RGInspectorButtonAttribute inspectorButtonAttribute = (RGInspectorButtonAttribute)attribute;
            float buttonLength = position.width;
            Rect buttonRect = new Rect(position.x, position.y, buttonLength, position.height);
            GUI.skin.button.alignment = TextAnchor.MiddleLeft;
            if (GUI.Button(buttonRect, inspectorButtonAttribute.MethodName))
            {
                Type eventOwnerType = property.serializedObject.targetObject.GetType();
                string eventName = inspectorButtonAttribute.MethodName;
                if(_eventMethodInfo == null)
                {
                    _eventMethodInfo = eventOwnerType.GetMethod(eventName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic);
                }
                if(_eventMethodInfo != null)
                {
                    _eventMethodInfo.Invoke(property.serializedObject.targetObject, null);
                }
                else
                {
                    Debug.LogWarning(string.Format("InspectorButton: Unable to find method {0} in {1}", eventName, eventOwnerType));
                }
            }
        }
    }
#endif
}
