using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RedCubeScore : MonoBehaviour, Observer
{
    //i can't figure out why this works in the inventory script but not here
    private Text redCubeText;
    private List<GameObject> _redCubes = new List<GameObject>();
    private void Start()
    {
        redCubeText = gameObject.GetComponent<Text>();
    }

    public void OnNotify(GameObject obj, NotificationType notificationType)
    {
        Debug.Log("red cube score script notified");
        if (notificationType == NotificationType.RedCubeCollected)
        {
            Debug.Log("red cube notif happening");
            _redCubes.Add(obj);
            redCubeText.text = "Red Cubes: " + _redCubes.Count;
        }
    }
}
