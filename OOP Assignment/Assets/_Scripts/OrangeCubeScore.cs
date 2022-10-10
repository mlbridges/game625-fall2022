using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrangeCubeScore : MonoBehaviour, Observer
{
    //i can't figure out why this works in the inventory script but not here
    private List<GameObject> _orangeCubes = new List<GameObject>();
    private Text orangeCubeText;

    private void Start()
    {
        orangeCubeText = gameObject.GetComponent<Text>();

        foreach (SubjectBeingObserved subject in FindObjectsOfType<SubjectBeingObserved>())
        {
            subject.AddObserver(this);
        }
    }

    public void OnNotify(GameObject obj, NotificationType notificationType)
    {
        //Debug.Log("orange cube score script notified");
        if (notificationType == NotificationType.OrangeCubeCollected)
        {
            Debug.Log("orange cube notif happening");
            _orangeCubes.Add(obj);
            orangeCubeText.text = "Orange Cubes: " + _orangeCubes.Count;
        }
    }
}
