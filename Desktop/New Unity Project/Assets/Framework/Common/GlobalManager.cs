/***************************************************************
* Copyright (C): 2015  LuWei
* Author       : LuWei
* Version      : V1.01
* Create       : 2017/2/27 14:56:15
* Note:         全局设置管理器
***************************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

public class GlobalManager: MonoBehaviour
{
    /// <summary>
    ///   
    /// </summary>
    public GameObject[] Layer = new GameObject[(int)UICommon.emWindowType.Max];

    /// <summary>
    ///   
    /// </summary>
    public UICamera ViewCamera;

    /// <summary>
    ///   
    /// </summary>
    public UIRoot Root;

    /// <summary>
    /// 移动区域
    /// </summary>
    public static Rect MoveArea;  

    /// <summary>
    ///   
    /// </summary>
    void Awake()
    {
        //设置帧率
        Application.targetFrameRate = 45;
        //初始化移动区域
        MoveArea = new Rect(-Root.manualWidth / 2, -Root.manualHeight / 2, Root.manualWidth, Root.manualHeight);
        DoSceenResolutionAdaptive();
        Facade.Instance.StartUp(this, Layer, ViewCamera);
    }

    /// <summary>
    ///   
    /// </summary>
    void Start()
    {
        //此层次下的所有对象禁止被删除
        DontDestroyOnLoad(transform.gameObject);
    }

    /// <summary>
    ///   屏幕分辨率自适应
    /// </summary>
    public void DoSceenResolutionAdaptive()
    {
        if (Root == null)
            return;
#if UNITY_IPHONE || UNITY_ANDROID
        Root.scalingStyle = UIRoot.Scaling.ConstrainedOnMobiles;
#else
        Root.scalingStyle = UIRoot.Scaling.Constrained;
#endif
    }

}
