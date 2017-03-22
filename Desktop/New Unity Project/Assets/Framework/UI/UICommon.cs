using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UICommon
{
    /// <summary>
    ///   窗口类型
    /// </summary>
    public enum emWindowType
    {
        Game,                        //
        Normal,                      //普通窗口
        Top,                         //置顶
        Modal,                       //模态
        Tooltip,                     //提示信息

        Max
    }

    /// <summary>
    ///   关闭策略
    /// </summary>
    public enum emWindowHidePlan
    {
        Hide,           // 隐藏
        Delete,         // 删除
        OutSide,        // 移出边界
    }


    /// <summary>
    ///   界面常量
    /// </summary>
    public static class Constant
    {
        /// <summary>
        /// Outside偏移量
        /// </summary>
        public const int WINDOW_OUTSIDE_OFFSET = 10000;

        /// <summary>
        ///   路径
        /// </summary>
        public const string WINDOW_PATH = "GUI/";

        /// <summary>
        ///   不同窗口Depth
        /// </summary>
        public static readonly int[] WindowLayerDepthList =
        {
            -1000,
            1,
            1000,
            2000,
            3000,
        };
    }

    /// <summary>
    ///   组件路径 file_path[0] + "/" +
    /// </summary>
    public static string BuildPath<T>()
    {
        string typeName = typeof(T).Name;
        return Constant.WINDOW_PATH +  typeName;
    }

    /// <summary>
    ///   加载window prefab
    /// </summary>
    public static UIWindow Load<T>()
    {
        string path = BuildPath<T>();
        GameObject prefab = Resources.Load<GameObject>(path);
        if (prefab == null)
        {
            Debug.LogError("该界面路径错误!" + path);
            return null;
        }
        GameObject obj = UnityEngine.Object.Instantiate(prefab) as GameObject;
        if (obj == null)
        {
            Debug.LogError("Can't instantiate window's prefab, window type is " + path);
            return null;
        }

        UIWindow win = obj.GetComponent<UIWindow>();
        if (win == null) 
        {
            Debug.LogError("没有界面脚本!");
            return null;
        }

        return win;
    }

    /// <summary>
    ///   获得一个对象的指定子组件
    /// </summary>
    public static List<T> GetChildComponents<T>(GameObject go)
        where T : Component
    {
        List<T> result = new List<T>();

        if (go != null)
        {
            T[] objs = go.GetComponentsInChildren<T>();
            result.AddRange(objs);

            //剔除掉父对象中的组件
            T parent = go.GetComponent<T>();
            if (parent != null)
                result.Remove(parent);
        }

        return result;
    }

    /// <summary>
    /// 获得或者创建一个对象的组件
    /// </summary>
    public static T GetOrAddComponent<T>(GameObject go)
        where T : Component
    {
        if (go == null)
            return null;

        T com = go.GetComponent<T>();
        return com != null ? com : go.AddComponent<T>();
    }

}