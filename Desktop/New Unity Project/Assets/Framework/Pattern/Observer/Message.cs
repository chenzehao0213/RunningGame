/***************************************************************
* Copyright (C): 2015  LuWei
* Author       : LuWei
* Version      : V1.01
* Create       : 2017/2/27 14:56:15
* Note:        消息包
***************************************************************/
using UnityEngine;
using System.Collections;
using System;

public class Message : IMessage
{
    private string name_;
    private object body_;

    public Message(string name)
        : this(name, null)
    { }

    public Message(string name,object body)
    {
        this.name_ = name;
        this.body_ = body; 
    }

    public object Body
    {
        get { return body_; }
        set { body_ = value; }
    }

    public string Name { get { return name_; } }

    public override string ToString()
    {
        string msg = "Notification Name: " + Name;
        msg += "\nBody:" + ((Body == null) ? "null" : Body.ToString());
        return msg;
    }
}