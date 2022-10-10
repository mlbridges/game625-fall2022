using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour, Observer
{
    //private List<GameObject> _orangeCubes = new List<GameObject>();
    //public Text orangeCubeText;

    //public Text redCubeText;
    //private List<GameObject> _redCubes = new List<GameObject>();

    //private List<GameObject> _greenCubes = new List<GameObject>();
    //public Text greenCubeText;

    public void OnNotify(GameObject obj, NotificationType notificationType)
    {
        //Debug.Log("general notifications are happening");

        //if (notificationType == NotificationType.OrangeCubeCollected)
        //{
        //    Debug.Log("orange cube notif happening");
        //    _orangeCubes.Add(obj);
        //    orangeCubeText.text = "Orange Cubes: " + _orangeCubes.Count;
        //}

        //if (notificationType == NotificationType.RedCubeCollected)
        //{
        //    Debug.Log("red cube notif happening");
        //    _redCubes.Add(obj);
        //    redCubeText.text = "Red Cubes: " + _redCubes.Count;
        //}

        //if (notificationType == NotificationType.GreenCubeCollected)
        //{
        //    Debug.Log("green cube notif happening");
        //    _greenCubes.Add(obj);
        //    greenCubeText.text = "Green Cubes: " + _greenCubes.Count;
        //}
    }

    // Start is called before the first frame update
    void Start()
    {
        //foreach (SubjectBeingObserved subject in FindObjectsOfType<SubjectBeingObserved>())
        //{
        //    subject.AddObserver(this);
        //}
    }
}
