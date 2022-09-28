using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Observer
{
    //when notified, share info about a collectible obj + information about notif
    void OnNotify(GameObject obj, NotificationType notificationType);
}

public enum NotificationType
{
    GreenCubeCollected,
    RedCubeCollected,
    OrangeCubeCollected
}