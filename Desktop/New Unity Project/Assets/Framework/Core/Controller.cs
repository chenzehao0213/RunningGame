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

public class Controller : IController
{
    /// <summary>
    ///   
    /// </summary>
    private ObserverTabel observer_tabel_ = ObserverTabel.Instance;

    /// <summary>
    ///   
    /// </summary>
    private Dictionary<string, Type> command_map_ = new Dictionary<string, Type>();

    /// <summary>
    ///   需要的时候才实例化
    /// </summary>
    public void ExecuteCommand(IMessage message)
    {
        Type commandType = null;

        if (!command_map_.ContainsKey(message.Name)) return;
        commandType = command_map_[message.Name];

        object commandInstance = Activator.CreateInstance(commandType);

        if (commandInstance is ICommand)
        {
            ((ICommand)commandInstance).Execute(message);
        }
    }

    /// <summary>
    ///   
    /// </summary>
    public void RegisterCommand(string messageName, Type commandType)
    {
        if (!command_map_.ContainsKey(messageName))
        {
            observer_tabel_.RegisterObserver(messageName, new Observer("ExecuteCommand", this));
        }
        command_map_[messageName] = commandType;
    }

    /// <summary>
    ///   
    /// </summary>
    public void RemoveCommand(string messageName)
    {
        observer_tabel_.RemoveObserver(messageName);
    }
}