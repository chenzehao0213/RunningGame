/***************************************************************
* Copyright (C): 2015  LuWei
* Author       : LuWei
* Version      : V1.01
* Create       : 2017/2/27 14:56:15
* Note:        外观框架入口
***************************************************************/
using UnityEngine;
using System;
using Pomelo;
using System.Collections.Generic;

public class Facade : Singleton<Facade>
{
    /// <summary>
    ///   
    /// </summary>
    protected IController controller_;

    /// <summary>
    ///   
    /// </summary>
    protected IView view_;

    /// <summary>
    ///   
    /// </summary>
    protected NetworkManager Network;

    /// <summary>
    ///   
    /// 
    /// </summary>
    private ObserverTabel observer_tabel_ = ObserverTabel.Instance;

    

    #region Mediator

    /// <summary>
    ///   
    /// </summary>
    public void RegisterMediator(string messageName, IMediator mediator)
    {
        view_.RegisterMediator(messageName, mediator);
    }

    /// <summary>
    ///   
    /// </summary>
    public void RegisterMediator(string messageName, IMediator mediator, object data)
    {
        view_.RegisterMediator(messageName, mediator, data);
    }

    /// <summary>
    ///   
    /// </summary>
    public void RemoveMediator(string mediatorName)
    {
        view_.RemoveMediator(mediatorName);
    }
    #endregion

    #region UI

    /// <summary>
    ///   
    /// </summary>
    public void ToggleEvent(bool on)
    {
        view_.ToggleEvent(on);
    }

    /// <summary>
    ///   放到最后
    /// </summary> 
    public void PushBack(UIWindow window)
    {
        view_.PushBack(window);
    }

    /// <summary>
    ///   拿到前面
    /// </summary>
    public void BringForward(UIWindow window)
    {
        view_.BringForward(window);
    }

    /// <summary>
    ///   
    /// </summary>
    public T ShowWindow<T>() where T : UIWindow
    {
        return view_.ShowWindow<T>();
    }

    /// <summary>
    ///   
    /// </summary>
    public void HideWindow<T>() where T : UIWindow
    {
        view_.HideWindow<T>();
    }

    /// <summary>
    ///   
    /// </summary>
    public T GetWindow<T>() where T : UIWindow
    {
        return view_.GetWindow<T>();
    }

    /// <summary>
    ///   
    /// </summary>
    public UIWindow GetWindow(System.Type type)
    {
        return view_.GetWindow(type);
    }

    /// <summary>
    ///   
    /// </summary>
    public void DeleteWindow(UIWindow window, bool hide_window = true)
    {
        view_.DeleteWindow(window, hide_window);
    }

    /// <summary>
    ///   
    /// </summary>
    public void DeleteWindow<T>() where T : UIWindow
    {
        DeleteWindow(GetWindow<T>());
    }

    /// <summary>
    ///   
    /// </summary>
    public void DeleteAllWindow()
    {
        view_.DeleteAllWindow();
    }

    /// <summary>
    ///   
    /// </summary>
    public T CreateWindow<T>() where T : UIWindow
    {
        return view_.CreateWindow<T>();
    }

    /// <summary>
    ///   通知View镜像翻转
    /// </summary>
    public void NotifyViewMirrorFlip()
    {
        view_.MirrorFlip();
    }

   

    #endregion

    #region  Command

    /// <summary>
    ///   
    /// </summary>
    public void RegisterCommand(string messageName, Type commandType)
    {
        controller_.RegisterCommand(messageName, commandType);
    }

    /// <summary>
    ///   
    /// </summary>
    public void RemoveCommand(string messageName)
    {
        controller_.RemoveCommand(messageName);
    }

    #endregion

    #region  Observer

    /// <summary>
    ///   
    /// </summary>
    public void SendNotification(string messageName)
    {
        observer_tabel_.NotifyObservers(new Message(messageName));
    }

    /// <summary>
    ///   
    /// </summary>
    public void SendNotification(string messageName, object body)
    {
        observer_tabel_.NotifyObservers(new Message(messageName, body));
    }

    #endregion

    #region Network

    /// <summary>
    ///   
    /// </summary>
    public void AddNetworkModule(NetworkIOModule module)
    {
        Network.AddNetworkModule(module);
    }

    /// <summary>
    ///   
    /// </summary>
    public void AddNetworkModule<T>()
            where T : NetworkIOModule
    {
        Network.AddNetworkModule<T>();
    }

    /// <summary>
    ///   
    /// </summary>
    public void SendRequest<T>(string method)
          where T : NetworkIOModule
    {
        Network.SendRequest<T>(method);
    }

    /// <summary>
    ///   
    /// </summary>
    public void SendRequest<T>(string method, object[] obj)
        where T : NetworkIOModule
    {
        Network.SendRequest<T>(method, obj);
    }

    #endregion

    #region Initialize

    /// <summary>
    ///   
    /// </summary>
    private void InitializeContorller()
    {
        controller_ = new Controller();
        RegisterCommand(MessageConst.START_UP, typeof(StartUpCommand));
    }

    /// <summary>
    ///   
    /// </summary>
    private void InitializeView(GameObject[] layer, UICamera camera)
    {
        view_ = new View(layer, camera);
    }

    /// <summary>
    ///   
    /// </summary>
    private void InitializeNetwork()
    {
        Network = new NetworkManager();
    }

    #endregion

    #region Manager Where MonoBehaviour

    /// <summary>
    ///   
    /// </summary>
    private GameObject game_manager_;

    /// <summary>
    ///   
    /// </summary>
    private Dictionary<string, object> managers_ = new Dictionary<string, object>();

    /// <summary>
    ///   
    /// </summary>
    public GameObject GameManager
    {
        get
        {
            game_manager_ = GameObject.Find("GameManger");
            return game_manager_;
        }
    }

    /// <summary>
    ///   
    /// </summary>
    public T AddManager<T>()
        where T : MonoBehaviour
    {
        string name = typeof(T).ToString();
        object obj = null;
        managers_.TryGetValue(name, out obj);
        if (obj != null)
            return (T)obj;

        T temp = GameManager.AddComponent<T>();
        managers_.Add(name, temp);
        return temp;
    }

    /// <summary>
    ///   
    /// </summary>
    public T GetManager<T>()
        where T : MonoBehaviour
    {
        string name = typeof(T).ToString();
        if (!managers_.ContainsKey(name))
        {
            Debug.Log(name + " not add");
            return default(T);
        }
             
        object manager = null;
        managers_.TryGetValue(name, out manager);
        return (T)manager;
    }

    /// <summary>
    ///   
    /// </summary>
    public void RemoveManager<T>()
    {
        string name = typeof(T).ToString();

        if (!managers_.ContainsKey(name))
        {
            return;
        }
        object manager = null;
        managers_.TryGetValue(name, out manager);
        Type type = manager.GetType();
        if (type.IsSubclassOf(typeof(MonoBehaviour)))
        {
            GameObject.Destroy((Component)manager);
        }
        managers_.Remove(name);
    }
 
    #endregion

    /// <summary>
    ///   启动框架  
    /// </summary>
    public void StartUp(object startProgram, GameObject[] layer, UICamera camera)
    {
        //注意此处，view的初始化比命令的注册要晚
        InitializeContorller();
        InitializeView(layer, camera);
        InitializeNetwork();

        SendNotification(MessageConst.START_UP);
    }
}