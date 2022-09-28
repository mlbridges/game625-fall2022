using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GreenCubeScore : MonoBehaviour, Observer
{
    //i can't figure out why this works in the inventory script but not here
    private List<GameObject> _greenCubes = new List<GameObject>();
    private Text greenCubeText;
    void Start()
    {
        greenCubeText = gameObject.GetComponent<Text>();
    }

    public void OnNotify(GameObject obj, NotificationType notificationType)
    {
        Debug.Log("green cube score script notified");
        if (notificationType == NotificationType.GreenCubeCollected)
        {
            Debug.Log("green cube notif happening");
            _greenCubes.Add(obj);
            greenCubeText.text = "Green Cubes: " + _greenCubes.Count;
        }
    }
}
