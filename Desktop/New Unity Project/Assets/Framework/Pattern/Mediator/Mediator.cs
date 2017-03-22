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

public class Mediator : Notifier, IMediator
{
    /// <summary>
    ///   
    /// </summary>
    public Mediator(object viewComponent)
    {
        this.element_ = viewComponent;
    }

    /// <summary>
    ///   
    /// </summary>
    private object element_;

    /// <summary>
    ///   
    /// </summary>
    public virtual void HandleNotification(IMessage message)
    {
        
    }

    /// <summary>
    ///   
    /// </summary>
    public object Element
    {
        get
        {
            return element_;
        }
    }


}