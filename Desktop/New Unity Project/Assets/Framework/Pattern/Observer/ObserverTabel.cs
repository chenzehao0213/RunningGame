/***************************************************************
* Copyright (C): 2015  LuWei
* Author       : LuWei
* Version      : V1.01
* Create       : 2017/2/27 14:56:15
* Note:         消息表
***************************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObserverTabel : Singleton<ObserverTabel>
{
    /// <summary>
    ///  
    /// </summary>
    private Dictionary<string, List<IObserver>> observer_map_ = new Dictionary<string, List<IObserver>>();

    /// <summary>
    ///   
    /// </summary>
    public void NotifyObservers(IMessage message)
    {      
        List<IObserver> observers = null;

        if (observer_map_.ContainsKey(message.Name)) 
        {
            observers = observer_map_[message.Name];
        }

        if (observers != null) 
        {
            for (int i = 0; i < observers.Count; i++) 
            {
                IObserver observer = observers[i];
                observer.NotifyObserver(message);
            }
        }
    }

    /// <summary>
    ///   
    /// </summary>
    public void RegisterObserver(string messageName, IObserver observer)
    {
        if (!observer_map_.ContainsKey(messageName))
        {
            observer_map_.Add(messageName, new List<IObserver>());
        }
        observer_map_[messageName].Add(observer);
    }

    /// <summary>
    ///   
    /// </summary>
    public void RemoveObserver(string messageName)
    {
        if (observer_map_.ContainsKey(messageName))
        {                    
            observer_map_.Remove(messageName);          
        }
    }
}