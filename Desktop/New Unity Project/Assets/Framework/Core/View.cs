/***************************************************************
* Copyright (C): 2015  LuWei
* Author       : LuWei
* Version      : V1.01
* Create       : 2017/2/27 14:56:15
* Note: 
***************************************************************/
using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class View :IView
{
    /// <summary>
    ///   
    /// </summary>
    private ObserverTabel observer_tabel_ = ObserverTabel.Instance;

    /// <summary>
    ///   
    /// </summary>
    private Dictionary<Type, UIWindow> window_tabel_;

    /// <summary>
    ///   层级
    /// </summary>
    private GameObject[] layer_ = new GameObject[(int)UICommon.emWindowType.Max];

    /// <summary>
    /// 
    /// </summary>
    private UICamera ui_camera_ = null;

    /// <summary>
    ///   窗体默认缩放
    /// </summary>
    private Vector3 scale_ = Vector3.one;

    /// <summary>
    ///   
    /// </summary>
    public View(GameObject[] gameobjects, UICamera ui_camera)
    {
        window_tabel_ = new Dictionary<Type, UIWindow>();
        layer_ = gameobjects;
        ui_camera_ = ui_camera;
        InitializeWindowLayerDepth();
    }

    #region Window 

    /// <summary>
    ///   
    /// </summary>
    public T CreateWindow<T>() where T : UIWindow
    {
        T win = GetWindow<T>();
        if (win != null)
            return win;

        win = UICommon.Load<T>() as T;
        if (win == null)
            return default(T);

        GameObject obj = win.gameObject;
      
        obj.SetActive(false);
        Vector3 position = obj.transform.localPosition;

        GameObject parent = layer_[(int)win.WindowType]; //TODO
        obj.transform.parent = parent.transform;
        obj.gameObject.layer = parent.layer;
        obj.transform.localScale = scale_;
        win.Position = position;

        System.Type type = win.GetType();
        if (!window_tabel_.ContainsKey(type)) 
            window_tabel_.Add(type, win);
        return win;
    }

    /// <summary>
    /// 删除界面
    /// </summary>
    public void DeleteWindow(UIWindow window, bool hide_window = true)
    {
        if (window != null)
        {
            if (hide_window)
            {
                window.Hide();
                if (window.HidePlan != UICommon.emWindowHidePlan.Delete)
                    return;
            }

            System.Type key = window.GetType();
            if (window_tabel_.ContainsKey(key))
                window_tabel_.Remove(key);

            UnityEngine.Object.Destroy(window.gameObject);
        }
    }

    /// <summary>
    ///   
    /// </summary>
    public void DeleteAllWindow()
    {
        var copy = new Dictionary<System.Type, UIWindow>(window_tabel_);
        var itr = copy.Values.GetEnumerator();
        while (itr.MoveNext())
        {
            var copy_list = itr.Current;
            DeleteWindow(copy_list);      
        }
        itr.Dispose();

        window_tabel_.Clear();
    }

    /// <summary>
    ///   
    /// </summary>
    public T GetWindow<T>() where T : UIWindow
    {
        UIWindow list;
        window_tabel_.TryGetValue(typeof(T), out list);
        if (list == null)
            return default(T);
 
        return list as T;
    }

    public UIWindow GetWindow(System.Type type)
    {
        UIWindow list;
        window_tabel_.TryGetValue(type, out list);
        if (list == null)
            return null;
        return list;
    }

    /// <summary>
    ///   
    /// </summary>
    public void HideWindow<T>() where T : UIWindow
    {
        HideWindow(GetWindow<T>());
    }

    /// <summary>
    ///   隐藏一个界面，没有则无视
    /// </summary>
    private void HideWindow<T>(T window) where T : UIWindow
    {
        if (window != null)
        {
            if (window.HidePlan == UICommon.emWindowHidePlan.Delete)
                DeleteWindow(window);
            else
                window.Hide();
        }
    }

    /// <summary>
    ///   
    /// </summary>
    public T ShowWindow<T>() where T : UIWindow
    {
        T win = GetWindow<T>();
        if (win == null)
            win = CreateWindow<T>();

        ShowWindow(win);
        return win;
    }

    /// <summary>
    ///   
    /// </summary>
    private void ShowWindow<T>(T window) where T : UIWindow
    {
        if (window != null)
            window.Show();
    }

    /// <summary>
    ///  通知镜像翻转
    /// </summary>
    public void MirrorFlip()
    {
        this.scale_ = new Vector3(-1, -1, 1);
    }

    #endregion

    #region  Depth 操作

    public void BringForward(UIWindow window)
    {
        if (window.transform.parent == null)
        {
            Debug.LogError("Can't find parent's UIPanel.");
            return;
        }
        UIPanel parent = window.transform.parent.GetComponentInParent<UIPanel>();
        if (parent == null)
        {
            Debug.LogError("Can't find parent's UIPanel.");
            return;
        }

        int max_depth = 0;
        UIPanel[] panels = parent.gameObject.GetComponentsInChildren<UIPanel>();
        for (int i = 0; i < panels.Length; ++i)
        {
            if (max_depth < panels[i].depth)
                max_depth = panels[i].depth;
        }

        //设置新的depth
        window.Depth = max_depth + 1;
        //调整窗口的depth
        AdjustPanelDepth((UIPanel)window);
        //调整所有depth
        AdjustPanelDepth(parent);
    }

    /// <summary>
    /// 界面弹至最后
    /// </summary>
    public void PushBack(UIWindow window)
    {
        if (window.transform.parent == null)
        {
            Debug.LogError("Can't find parent's UIPanel.");
            return;
        }
        UIPanel parent = window.transform.parent.GetComponentInParent<UIPanel>();
        if (parent == null)
        {
            Debug.LogError("Can't find parent's UIPanel.");
            return;
        }

        int min_depth = 0;
        UIPanel[] panels = parent.GetComponentsInChildren<UIPanel>();
        for (int i = 0; i < panels.Length; ++i)
        {
            if (min_depth > panels[i].depth)
                min_depth = panels[i].depth;
        }

        //设置新的depth
        window.Depth = min_depth - 1;

        //调整所有depth
        AdjustPanelDepth(parent);
    }

    /// <summary>
    /// 调整Panel's Depth
    /// </summary>
    private void AdjustPanelDepth(UIPanel parent)
    {
        //获得所有子物体
        List<UIPanel> panels = UICommon.GetChildComponents<UIPanel>(parent.gameObject);
        int size = panels.Count;
        if (size > 0)
        {
            int current = parent.depth;
            panels.Sort(UIPanel.CompareFunc);
            for (int i = 0; i < size; ++i)
            {
                UIPanel w = panels[i];
                w.depth = ++current;
            }
        }
    }

    #endregion
 
    /// <summary>
    /// 开关界面输入设备事件响应
    /// </summary>
    public void ToggleEvent(bool on)
    {
        if (ui_camera_)
        {
            ui_camera_.useMouse = on;
            ui_camera_.useTouch = on;
            ui_camera_.useKeyboard = on;
            ui_camera_.useController = on;
        }
    }

    /// <summary>
    ///   初始化winddow 的depth
    /// </summary>
    void InitializeWindowLayerDepth()
    {
        var count = (int)UICommon.emWindowType.Max;
        for (int i = 0; i < count; ++i)
        {
            if (layer_[i] != null)
            {
                UIPanel layer = layer_[i].GetComponent<UIPanel>();
                if (layer != null)
                    layer.depth = UICommon.Constant.WindowLayerDepthList[i];
            }
        }
    }

    #region Mediator

    /// <summary>
    ///   
    /// </summary>
    public void RegisterMediator(string messageName, IMediator mediator, object data)
    {
        Observer observer = new Observer("handleNotification", mediator, data);
        observer_tabel_.RegisterObserver(messageName, observer);
    }

    /// <summary>
    ///   
    /// </summary>
    public void RegisterMediator(string messageName, IMediator mediator)
    {
        RegisterMediator(messageName, mediator, null);
    }

    /// <summary>
    ///   
    /// </summary>
    public void RemoveMediator(string mediatorName)
    {
        observer_tabel_.RemoveObserver(mediatorName);
    }


    #endregion
}