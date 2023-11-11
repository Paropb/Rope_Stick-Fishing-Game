using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace MyGame.MGSystem
{
    public class RGInformationAttribute : PropertyAttribute
    {
        public enum InformationType { Error, Info, None, Warning }
#if UNITY_EDITOR
        public string Message;
        public MessageType Type;
        public bool MessageAfterProperty;


        public RGInformationAttribute(string message, InformationType type, bool messageAfterProperty)
        {
            this.Message = message;
            if (type == InformationType.Error) { this.Type = UnityEditor.MessageType.Error; }
            if (type == InformationType.Info) { this.Type = UnityEditor.MessageType.Info; }
            if (type == InformationType.Warning) { this.Type = UnityEditor.MessageType.Warning; }
            if (type == InformationType.None) { this.Type = UnityEditor.MessageType.None; }
            this.MessageAfterProperty = messageAfterProperty;
        }
#else
        public MMInformationAttribute(string message, InformationType type, bool messageAfterProperty)
		{

		}
#endif
    }
}
