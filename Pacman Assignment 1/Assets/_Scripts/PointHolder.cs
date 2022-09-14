using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Timeline.Actions.MenuPriority;

public class PointHolder : MonoBehaviour
{
    public Dictionary<string, int> FoodInv;

    //events for the UI to subscribe to
    public delegate void DoSomething();
    public static event DoSomething OnFoodEaten;
    public static event DoSomething OnPowerUpEaten;

    public int FoodAmount = 0;
    public int PowerUpAmount = 0;

    public int currentAmount;

    // Start is called before the first frame update
    void Start()
    {
        //on start we create the dictionary new every time so there's no hold over from last play
        FoodInv = new Dictionary<string, int>();

        //we also subscribe the dictionary to the objectcollection script's messages
        FoodCollection.OnPlayerColl += AddGem;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //function which is fired whenever this script receives the player collision message
    public void AddGem(string _tag)
    {
        //right now the amount of gems added is always one, but I may restructure later if we foresee a case in which that would be different
        int amount = 1;

        //currentAmount will check and see how many of each item we already possess
        currentAmount = 0;

        //if this function goes to the dictionary and finds no entry named for the gem we just picked up
        if (!FoodInv.TryGetValue(_tag, out currentAmount))
        {
            //it'll assume we have none currently in our inventory
            currentAmount = 0;
        }

        //if the currentAmount and the amount we're adding (right now 1) add up to more than zero
        //which they always will, but we are playing it safe
        //then we are adding the amount to the currentAmount
        if (currentAmount + amount > 0)
        {
            currentAmount += amount;
        }
        //if they add up to zero or less than zero, we always set currentAmount back to zero
        //this doesn't look useful now but it will help us when we need to remove stuff from the inventory
        else
        {
            currentAmount = 0;
        }

        //this removes the entry in the dictionary for this type of gem
        FoodInv.Remove(_tag);

        //and then if currentAmount is greater than 0 (i.e. we have adding something, and not removed it)
        if (currentAmount > 0)
        {
            //it puts the entry back into the dictionary with the same name and the increased quantity
            FoodInv.Add(_tag, currentAmount);

            //from molly: if the current amount is greater than zero then the player should also be able to eat the gem
            //we should have an event system in this script, which the player then subscribes to
            //this is the spot where the bat signal to turn on gem eating gets sent to the player:
            if (_tag == "food")
            {
                //send a message to the UI saying food is picked up
                OnFoodEaten();
                FoodAmount++;
            }

            if (_tag == "powerUp")
            {
                //send a message to the UI saying power up is picked up
                OnPowerUpEaten();
                PowerUpAmount++;
            }
        }
    }
}
