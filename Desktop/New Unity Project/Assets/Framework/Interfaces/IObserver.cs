using UnityEngine;
using System.Collections;

public interface IObserver
{
    string NotifyMethod { get; }

    object NotifyContext { get; }

    void NotifyObserver(IMessage message);

    bool EqualsContext(object obj);
}