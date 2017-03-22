using UnityEngine;
using System.Collections;

public interface INotifier
{
    void SendNotification(string messageName);

    void SendNotification(string messageName, object body);
}