using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GreenCubeScore : MonoBehaviour, Observer
{
    //i can't figure out why the notify void works in the inventory script but not here
    private List<GameObject> _greenCubes = new List<GameObject>();
    private Text greenCubeText;
    void Start()
    {
        greenCubeText = gameObject.GetComponent<Text>();
        greenCubeText.text = "blorb";

        foreach (SubjectBeingObserved subject in FindObjectsOfType<SubjectBeingObserved>())
        {
            subject.AddObserver(this);
        }
    }

    public void OnNotify(GameObject obj, NotificationType notificationType)
    {
        //Debug.Log("green cube score script notified");
        if (notificationType == NotificationType.GreenCubeCollected)
        {
            //Debug.Log("green cube specifically collected");
            _greenCubes.Add(obj);
            greenCubeText.text = "Green Cubes: " + _greenCubes.Count;
        }
    }
}
