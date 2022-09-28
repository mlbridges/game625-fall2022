using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeCubeObserved : SubjectBeingObserved, Collectible
{
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("on trigger enter happens orange");
        if (other.CompareTag("Player"))
        {
            Notify(gameObject, NotificationType.OrangeCubeCollected);
        }
    }

    public string GetName()
    {
        return "orange_cube";
    }
}
