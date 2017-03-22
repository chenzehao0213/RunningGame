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

public interface IMediator
{
    void HandleNotification(IMessage message);

    object Element { get; }
}