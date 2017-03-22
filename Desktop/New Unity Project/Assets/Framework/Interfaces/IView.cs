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

public interface IView
{
    /// <summary>
    ///   Mediator 操作
    /// </summary>
    void RegisterMediator(string messageName, IMediator mediator, object data);
    void RegisterMediator(string messageName, IMediator mediator);
    void RemoveMediator(string mediatorName);

    /// <summary>
    ///   Window 操作
    /// </summary>
    T CreateWindow<T>() where T : UIWindow;
    T GetWindow<T>() where T : UIWindow;
    UIWindow GetWindow(System.Type type);
    T ShowWindow<T>() where T : UIWindow;
    void HideWindow<T>() where T : UIWindow;
    void DeleteWindow(UIWindow window, bool hide_window = true);
    void DeleteAllWindow();

    /// <summary>
    ///   Depth 操作
    /// </summary>
    void BringForward(UIWindow window);
    void PushBack(UIWindow window);
    void ToggleEvent(bool on);
    void MirrorFlip();
}