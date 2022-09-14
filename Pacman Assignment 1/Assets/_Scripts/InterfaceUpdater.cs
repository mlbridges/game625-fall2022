using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//this script is attached to UI text and changes the text as items are picked up
public class InterfaceUpdater : MonoBehaviour
{
    public PointHolder points;
    public Text FoodPoints;
    public Text PowerUpPoints;

    // Start is called before the first frame update
    void Start()
    {
        
        PointHolder.OnFoodEaten += FoodThingIncrease;
        PointHolder.OnPowerUpEaten += PowerUpThingIncrease;
    }

    public void FoodThingIncrease()
    {
        FoodPoints.text = "Food: " + points.FoodAmount;
    }

    public void PowerUpThingIncrease()
    {
        PowerUpPoints.text = "Powerups: " + points.PowerUpAmount;
    }
}
