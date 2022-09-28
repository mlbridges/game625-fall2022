using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenCubeObserve : SubjectBeingObserved, Collectible
{
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("on trigger enter happens green");
        if (other.CompareTag("Player"))
        {
            Notify(gameObject, NotificationType.GreenCubeCollected);
        }
    }

    public string GetName()
    {
        return "green_cube";
    }
}
