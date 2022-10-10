using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GreenCubeEventCollection : MonoBehaviour
{
    public static event Action<string, int> OnGreenCubeCollected;
    // Start is called before the first frame update

    private int score = 2;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            OnGreenCubeCollected(gameObject.name, score);
        }
    }
}
