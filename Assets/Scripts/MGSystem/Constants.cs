using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGame.MGEntity;



public static class Constants
{
    #region SaveFileNames
    public const string LEVEL_PATH = "Level/";
    public const string ROOM_PATH = "Rooms/";
    public const string ROOM_FILENAME = "newRoom.data";
    #endregion
    #region Player
    public static Player player;
    public const float STEP = 0.05f;
    public const float DEVIATION = 0.02f;
    public const float CORRECTIONCOOLDER = 0.1f;
    #endregion
    #region Physics
    public const float GRAVITY = 40f;
    public const float MAXFALL = -16f;
    public const float HORIZONTALDEC = 40F;
    public const float RUNDEC = 120f;
    //追踪一个目标时，若相距距离小于此值则判断到达目标
    public const float SEEKDISTANCEIGNORER = 0.2f;
    #endregion
    #region Time
    public const float FAST = 0.1f;
    public const float FAST2 = 0.2f;
    #endregion
    #region Namespace
    public const string PROJECT_NAMESPACE = "MyGame";
    #endregion
    #region Level Editor
    #endregion
    public static void InitializeConstants(Player player)
    {
        Constants.player = player;
    }
}

