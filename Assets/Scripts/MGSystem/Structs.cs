using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;


public struct AttackDetails
{
    public Vector2 Position { get; private set; }
    public float DamageAmount { get; private set; }

    public AttackDetails(Vector2 position, float damageAmount)
    {
        Position = position;
        DamageAmount = damageAmount;
    }
}

[Serializable]
public struct AttackTypeData : IComparable<AttackTypeData>
{
    public string attackName;
    [field: SerializeReference, HideInInspector] public int Index { get; set; }
    [EnumToggleButtons] public AttackTypes attackType;
    public AttackTypeData(string attackName, AttackTypes attackType, int index = -1)
    {
        this.attackName = attackName;
        this.attackType = attackType;
        Index = index;
    }
    public int CompareTo(AttackTypeData other)
    {
        if ((int)attackType > (int)other.attackType)
            return 1;
        else if ((int)attackType == (int)other.attackType)
        {
            return attackName.CompareTo(other.attackName);
        }
        else
            return -1;
    }
}


