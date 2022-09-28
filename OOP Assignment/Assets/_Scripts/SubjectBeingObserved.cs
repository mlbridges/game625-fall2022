using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubjectBeingObserved : MonoBehaviour
{
    //keeps a list of things observing SBO
    private List<Observer> _observers = new List<Observer>();

    public void AddObserver(Observer observer)
    {
        _observers.Add(observer);
    }

    public void RemoveObserver(Observer observer)
    {
        _observers.Remove(observer);
    }

    public void Notify(GameObject obj, NotificationType notificationType)
    {
        foreach(Observer observer in _observers)
        {
            observer.OnNotify(obj, notificationType);
        }
    }
}
