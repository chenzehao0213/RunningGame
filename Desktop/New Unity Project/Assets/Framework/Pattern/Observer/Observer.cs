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
using System.Reflection;

public class Observer : IObserver 
{
    /// <summary>
    ///   通知对象
    /// </summary>
    private object notify_object_;

    /// <summary>
    ///   通知名字
    /// </summary>
    private string notify_method_;

    /// <summary>
    ///   
    /// </summary>
    private object notify_data_;

    /// <summary>
    ///   
    /// </summary>
    public Observer(string notifyMethod, object notifyContext)
        : this(notifyMethod, notifyContext, null)
    {

    }

    /// <summary>
    ///   
    /// </summary>
    public Observer(string notifyMethod,object notifyContext,object notify_data)
    {
        this.notify_method_ = notifyMethod;
        this.notify_object_ = notifyContext;
        this.notify_data_ = notify_data;
    }

    /// <summary>
    ///   
    /// </summary>
    public void NotifyObserver(IMessage message)
    {
        string method = notify_method_;
        object context = notify_object_;
        if (notify_data_ != null) 
            message.Body = notify_data_;

        BindingFlags f = BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase;
        Type type = context.GetType();
        MethodInfo mi = type.GetMethod(method, f);
        
        mi.Invoke(context, new object[] { message });
    }

    /// <summary>
    ///   
    /// </summary>
    public bool EqualsContext(object obj)
    {
        return notify_object_.Equals(obj);
    }

    /// <summary>
    ///   
    /// </summary>
    public object NotifyContext
    {
        get { return this.notify_object_; }
    }

    /// <summary>
    ///   
    /// </summary>
    public string NotifyMethod
    {
        get { return this.notify_method_; }
    }
}