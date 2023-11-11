using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class TransformExtensions
{
    public static RotationalDirections DirectionNextTo(this Transform transform, Transform other)
    {
        if (transform == other)
        {
            return RotationalDirections.None;
        }
        return Vector3.Cross(transform.up, other.transform.up).z > 0f ? RotationalDirections.Clockwise : RotationalDirections.CounterClockwise;
    }
    public static Transform GetRootParent(this Transform transform)
    {
        if (transform.parent == null)
        {
            return transform;
        }
        else
        {
            return GetRootParent(transform.parent);
        }
    }
    public static void LookAt2D(this Transform transform, Transform target)
    {
        Vector3 dir = transform.position - target.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
public static class Vector3Extensions
{
    public static Vector3 CellToWorldPos(this Vector3Int cellPos, float prefabOffset = 0.5f)
    {
        return new Vector3(cellPos.x + prefabOffset, cellPos.y + prefabOffset);
    }

    public static Vector3Int WorldToCellPos(this Vector3 worldPos, float prefabOffset = 0.5f)
    {
        return new Vector3Int((int)(worldPos.x), (int)(worldPos.y));
    }
    public static Vector3Int ToVector3Int(this Vector3 thisV3)
    {
        return new Vector3Int((int)thisV3.x, (int)thisV3.y, (int)thisV3.z);
    }
    public static Vector3Int ToVector3IntRound(this Vector3 thisV3)
    {
        return new Vector3Int(Mathf.RoundToInt(thisV3.x), Mathf.RoundToInt(thisV3.y), Mathf.RoundToInt(thisV3.z));
    }
    public static Vector3Int ToVector3IntFloor(this Vector3 thisV3)
    {
        return new Vector3Int(Mathf.FloorToInt(thisV3.x), Mathf.FloorToInt(thisV3.y), Mathf.FloorToInt(thisV3.z));
    }
    public static Vector3Int ToVector3IntCeil(this Vector3 thisV3)
    {
        return new Vector3Int(Mathf.CeilToInt(thisV3.x), Mathf.CeilToInt(thisV3.y), Mathf.CeilToInt(thisV3.z));
    }
    public static Vector2 ToVector2(this Vector3 thisV3) => (Vector2)thisV3;
}

public static class ListExtensions
{
    public static List<T> GetDifferenceList<T>(this List<T> list1, List<T> list2)
    {
        List<T> differenceList = new List<T>();

        foreach (T item in list1)
        {
            if (!list2.Contains(item))
            {
                differenceList.Add(item);
            }
        }

        return differenceList;
    }
}
public static class IntExtentions
{
    public static int Abs(this int caller)
    {
        return Mathf.Abs(caller);
    }
}
public static class FloatExtentions
{
    public static float Abs(this float caller)
    {
        return Mathf.Abs(caller);
    }
}

