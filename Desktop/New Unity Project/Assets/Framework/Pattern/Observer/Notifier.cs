using UnityEngine;
using System.Collections;
using System;

public class Notifier : INotifier
{
    /// <summary>
    ///   
    /// </summary>
    private ObserverTabel observer_tabel_ = ObserverTabel.Instance;

    /// <summary>
    ///   
    /// </summary>
    public void SendNotification(string messageName)
    {
        observer_tabel_.NotifyObservers(new Message(messageName));
    }

    /// <summary>
    ///   
    /// </summary>
    public void SendNotification(string messageName, object body)
    {
        observer_tabel_.NotifyObservers(new Message(messageName, body));
    }
}