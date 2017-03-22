/***************************************************************
* Copyright (C): 2015  LuWei
* Author       : LuWei
* Version      : V1.01
* Create       : 2017/2/27 14:56:15
* Note:         Mono集合
***************************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ListedMonoBehaviour<Type> : MonoBehaviour
    where Type: ListedMonoBehaviour<Type>
{
    /// <summary>
    ///   
    /// </summary>
    private static List<Type> all_;

    /// <summary>
    ///   
    /// </summary>
    public virtual void Initialization()
    {
        this.Awake();
    }

    /// <summary>
    /// 
    /// </summary>
    static ListedMonoBehaviour()
    {
         all_ = new List<Type>();
    }

    /// <summary>
    ///   
    /// </summary>
    private void Awake()
    {
        if (all_ == null) 
            NewList();
        if (!all_.Contains((Type)this))
        {
            all_.Add((Type)this);
        }
    }

    /// <summary>
    ///   
    /// </summary>
    private static void NewList()
    {
        all_ = new List<Type>();
    }

    /// <summary>
    ///   
    /// </summary>
    public virtual void OnDestroy()
    {
        if (ListedMonoBehaviour<Type>.all_ != null)
        {
            ListedMonoBehaviour<Type>.all_.Remove((Type)this);
            if (ListedMonoBehaviour<Type>.all_.Count == 0)
            {
                ListedMonoBehaviour<Type>.NewList();
            }
        }
    }

    /// <summary>
    ///   
    /// </summary>
    public static List<Type> All
    {
        get
        {
            return all_;
        }
    } 
}
