/***************************************************************
* Copyright (C): 2015  LuWei
* Author       : LuWei
* Version      : V1.01
* Create       : 2017/2/27 14:56:15
* Note:        单例模式
***************************************************************/
using UnityEngine;
using System.Collections;

public abstract class Singleton<T> where T : Singleton<T>, new()
{
    private static T instance_= default(T);
    private static object locker = new object();

    public static T Instance
    {
        get
        {
            lock (locker)
            {
                if (instance_ == null)
                {
                    lock (locker)
                    {
                        instance_ = new T();
                        instance_.Initialize();
                    }
                }
                return instance_;
            }
        }
    }

    public Singleton()
    {

    }


    public virtual void Initialize()
    { }

}
