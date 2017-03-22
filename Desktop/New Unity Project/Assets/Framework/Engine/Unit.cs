/***************************************************************
* Copyright (C): 2015  LuWei
* Author       : LuWei
* Version      : V1.01
* Create       : 2017/2/27 14:56:15
* Note:         untiy 算法类
***************************************************************/
using UnityEngine;
using System.Collections;

public static class Unit
{

    //要1个投影矩阵
    public static Matrix4x4 projectionMatrix;

    /// <summary>
    ///   上下左右翻转
    /// </summary>
    public static void MirrorFlipCameraXY(Camera camera)
    {
        MirrorFlipCamera(camera, -1, -1);
    }

    /// <summary>
    ///   上下翻转
    /// </summary>
    public static void MirrorFlipCameraY(Camera camera)
    {
        MirrorFlipCamera(camera, 1, -1);
    }

    /// <summary>
    ///   左右翻转
    /// </summary>
    public static void MirrorFlipCameraX(Camera camera)
    {
        MirrorFlipCamera(camera, -1, 1);
    }

    /// <summary>
    ///   
    /// </summary>
    private static void MirrorFlipCamera(Camera camera, int x, int y)
    {
        Matrix4x4 mat = camera.projectionMatrix;
        mat *= Matrix4x4.Scale(new Vector3(x, y, 1));
        projectionMatrix = camera.projectionMatrix;
        camera.projectionMatrix = mat;
    }

    /// <summary>
    ///   设置父亲
    /// </summary>
    public static void SetParent(GameObject owner, Transform parent)
    {
        owner.transform.parent = parent;
        owner.transform.localPosition = Vector3.zero;
        owner.transform.rotation = Quaternion.identity;
        owner.transform.localScale = Vector3.one;
    }
}