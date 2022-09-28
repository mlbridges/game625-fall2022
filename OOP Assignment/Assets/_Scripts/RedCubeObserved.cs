using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedCubeObserved : SubjectBeingObserved, Collectible
{
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("on trigger enter happens red");
        if (other.CompareTag("Player"))
        {
            Notify(gameObject, NotificationType.RedCubeCollected);
        }
    }

    public string GetName()
    {
        return "red_cube";
    }
}
