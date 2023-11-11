using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace MyGame.MGEntity
{
    [CreateAssetMenu(menuName = ("MyGame/Data/Player/PlayerDataSO"), fileName = ("PlayerData"))]
    public class PlayerDataSO : ScriptableObject
    {
        [TabGroup("Move")] public float RunSpeed;
        [TabGroup("Move")] public float RunAccel;
        [TabGroup("Move")] public float RunDec;

        [TabGroup("Rotation")] public float MoveRotationDegree = 15f;
        [TabGroup("Rotation")] public float RotationDuration;
    }
}
