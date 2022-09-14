using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//the script that controls character movement + tracks collision of character w other gameobjects, attached to player
public class character : MonoBehaviour
{
    private CharacterController characterController;
    public int Speed = 2;
    private int powerUp;

    //event to tell enemy when powerup is or is not obtained
    public delegate void Placeholder();
    public static event Placeholder WeGotGems;
    public static event Placeholder NoMoreGems;

    // Start is called before the first frame update
    void Start()
    {
        //find and assign the character controller from the unity object
        characterController = GetComponent<CharacterController>();
        powerUp = 0;
    }

    // Update is called once per frame
    void Update()

    {   //create a vector3 to change the position of the character
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //move the character according to the vector3 you just set up
        characterController.Move(move * Time.deltaTime * Speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collided with something");
        if(collision.gameObject.tag == "enemy")
        {
            Debug.Log("collided with enemy");
            if(powerUp <= 0)
            {
                //destroy the player
                Destroy(gameObject);
                //tell the enemy no more powerups
                if(NoMoreGems != null)
                {
                    NoMoreGems();
                }
            }

            if(powerUp > 0)
            {
                //destroy the enemy
                Destroy(collision.gameObject);
                //don't destroy, but freeze enemy?
                //decrease the number of powerups by 1
                powerUp -= 1;
                Debug.Log("powerUp = " + powerUp);
            }
        }

        //when the player collides with a power up
        if(collision.gameObject.tag == "powerUp")
        {
            Debug.Log("collided with powerup");
            //increase the number of powerups by 1
            powerUp += 1;
            Debug.Log("powerUp = " + powerUp);
            //destroy the powerup
            Destroy(collision.gameObject);
            //SEND MESSAGE TO ENEMY TO RUN AWAY!!
            if(WeGotGems != null)
            {
                WeGotGems();
            }
        }
    }
}
