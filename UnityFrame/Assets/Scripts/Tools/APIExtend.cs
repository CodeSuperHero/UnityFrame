using UnityEngine;
using System.Collections;

/// <summary>
/// 拓展原有mono库 
/// </summary>
public static class APIExtend
{
    /// <summary>
    /// 去掉三维向量的Y轴，把向量投射到xz平面。
    /// </summary>
    /// <param name="vector3"></param>
    /// <returns></returns>
    public static Vector2 IgnoreYAxis(this Vector3 vector3)
    {
        return new Vector2(vector3.x, vector3.z);
    }

    /// <summary>
    /// 求点到直线的距离，采用数学公式Ax+By+C = 0; d = A*p.x + B * p.y + C / sqrt(A^2 + B ^ 2)
    /// 此算法忽略掉三维向量的Y轴，只在XZ平面进行计算，适用于一般3D游戏。
    /// </summary>
    /// <param name="startPoint">向量起点</param>
    /// <param name="endPoint">向量终点</param>
    /// <param name="point">待求距离的点</param>
    /// <returns></returns>
    public static float DistanceOfPointToVector(Vector3 startPoint, Vector3 endPoint, Vector3 point)
    {
        Vector2 startVe2 = startPoint.IgnoreYAxis();
        
        Vector2 endVe2 = endPoint.IgnoreYAxis();
        
        float A = endVe2.y - startVe2.y;

        float B = startVe2.x - endVe2.x;

        float C = endVe2.x * startVe2.y - startVe2.x * endVe2.y;

        float denominator = Mathf.Sqrt(A * A + B * B);
        
        Vector2 pointVe2 = point.IgnoreYAxis();
        
        return Mathf.Abs((A * pointVe2.x + B * pointVe2.y + C) / denominator);;
    }

    /// <summary>
    /// 判断目标点是否位于向量的左边
    /// </summary>
    /// <param name="startPoint">向量起点</param>
    /// <param name="endPoint">向量终点</param>
    /// <param name="point">目标点</param>
    /// <returns>True is on left, false is on right</returns>
    public static bool PointOnLeftSideOfVector(this Vector3 vector3, Vector3 originPoint, Vector3 point)
    {
        Vector2 originVec2 = originPoint.IgnoreYAxis();

        Vector2 pointVec2 = (point.IgnoreYAxis() - originVec2).normalized;
        
        Vector2 vector2 = vector3.IgnoreYAxis();
        
        float verticalX = originVec2.x;
        
        float verticalY = (-verticalX * vector2.x) / vector2.y;
        
        Vector2 norVertical = (new Vector2(verticalX, verticalY)).normalized;
        
        float dotValue = Vector2.Dot(norVertical, pointVec2);
        
        return dotValue < 0f;
    }

    /// <summary>
    /// 求出2D向量的垂直向量
    /// </summary>
    /// <param name="vector2">2D向量</param>
    /// <returns></returns>
    public static Vector2 VerticalVector(this Vector2 vector2)
    {
        float verticalX = vector2.x;
        float verticalY = (-verticalX * verticalX) / vector2.y;
        return new Vector2(verticalX, verticalY);
    }

}
