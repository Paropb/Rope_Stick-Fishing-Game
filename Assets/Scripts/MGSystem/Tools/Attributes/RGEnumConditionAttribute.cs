using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.MGSystem
{
    /// <summary>
    /// An attribute to conditionnally hide fields based on the current selection in an enum.
    /// Usage :  [RGEnumCondition("rotationMode", (int)RotationMode.LookAtTarget, (int)RotationMode.RotateToAngles)]
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Struct, Inherited = true)]
    public class RGEnumConditionAttribute : PropertyAttribute
    {
        public string ConditionEnum = "";
        public bool Hidden = false;
        BitArray bitArray = new BitArray(32);

        public bool ContainsBitFlag(int enumValue)
        {
            Debug.Log("RGEnumConditionAttribute-----ContainsBitFlag-----1");
            return bitArray.Get(enumValue);
        }
        public RGEnumConditionAttribute(string conditionBoolean, params int[] enumValues)
        {
            Debug.Log("RGEnumConditionAttribute-----RGEnumConditionAttribute-----1");

        }


    }
}
