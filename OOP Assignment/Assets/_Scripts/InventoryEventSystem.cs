using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryEventSystem : MonoBehaviour
{
    private Text AchievementText;
    // Start is called before the first frame update
    void Start()
    {
        GreenCubeEventCollection.OnGreenCubeCollected += CubeIsCollect;
        AchievementText = gameObject.GetComponent<Text>();
    }

    private void CubeIsCollect(string cube, int score)
    {
        Debug.Log("you collected a " + cube + "!");
        AchievementText.text = "Your " + cube + " was worth " + score + " points!";
    }

    //if this gameObject were ever destroyed, we would need to unsubscribe to the event to make sure we don't get weird errors
}
