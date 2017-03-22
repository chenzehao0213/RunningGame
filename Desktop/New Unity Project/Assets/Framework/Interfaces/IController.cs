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

public interface IController
{
    void RegisterCommand(string messageName, Type commandType);

    void ExecuteCommand(IMessage message);

    void RemoveCommand(string messageName);
}