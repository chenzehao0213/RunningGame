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

public interface IMessage
{
    string Name { get; }
    object Body { get; set; }
 
    string ToString();
}