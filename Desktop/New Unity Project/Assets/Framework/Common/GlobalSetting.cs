/***************************************************************
* Copyright (C): 2015  LuWei
* Author       : LuWei
* Version      : V1.01
* Create       : 2017/2/27 14:56:15
* Note: 
***************************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

public class GlobalSetting : MonoBehaviour
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
    void Awake()
    {
        //设置帧率
        Application.targetFrameRate = 45;
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

    // Update is called once per frame
    void Update()
    {

    }
 
    void TxtToProto()
    {
        //PlayerInfoExcelSerializer ex = new PlayerInfoExcelSerializer();
        //PlayerInfoDatas.Instance.Serializer = ex;
        //PlayerInfoDatas.Instance.DeserializeFromFile("E:/UnityProject/CeS/Assets/Resources/Excel/PlayerInfoModel.txt");

        //ProtoSerializer<PlayerInfModelContainer> proto = new ProtoSerializer<PlayerInfModelContainer>();
        //PlayerInfoDatas.Instance.Serializer = proto;
        //PlayerInfoDatas.Instance.SerializeToFile("E:/UnityProject/CeS/Assets/Resources/Data/PlayerInfoModel.bytes");
    }

    /// <summary>
    ///   屏幕分辨率自适应
    /// </summary>
    public void DoSceenResolutionAdaptive()
    {
        UIRoot root = GetComponentInParent<UIRoot>();
        if (root == null)
            return;
#if UNITY_IPHONE || UNITY_ANDROID
        root.scalingStyle = UIRoot.Scaling.ConstrainedOnMobiles;
#else
        root.scalingStyle = UIRoot.Scaling.Constrained;
#endif
    }

}
