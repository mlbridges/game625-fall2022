using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour
{
    public CommandExecuter command;

    void Start()
    {
        this.gameObject.GetComponent<Rigidbody>().detectCollisions = true;
        CommandExecuter.OnRewinding += Rewind;
        CommandExecuter.OnRewindEnded += RewindDone;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("enemy hit something!");
        if (other.tag == "Player")
        {
            Debug.Log("enemy hit player!");
            command.HitPoints--;
            Debug.Log("HP: " + command.HitPoints);
        }
    }

    public void Rewind()
    {
        this.gameObject.GetComponent<Rigidbody>().detectCollisions = false;
    }

    public void RewindDone()
    {
        this.gameObject.GetComponent<Rigidbody>().detectCollisions = true;
    }
}
