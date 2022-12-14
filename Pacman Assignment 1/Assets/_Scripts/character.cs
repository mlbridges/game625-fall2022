using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//the script that controls character movement + tracks collision of character w other gameobjects, attached to player
//this script sends messages to: pursue player
public class character : MonoBehaviour
{
    private CharacterController characterController;
    public int Speed = 2;
    private int powerUp;
    private int enemyNumber = 3;

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

        //subscribe to the powerup eaten event
        PointHolder.OnPowerUpEaten += ActivateRunning;
    }

    // Update is called once per frame
    void Update()

    {   //create a vector3 to change the position of the character
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //move the character according to the vector3 you just set up
        characterController.Move(move * Time.deltaTime * Speed);
    }

    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("collided with something");
        if(collider.gameObject.tag == "enemy")
        {
            Debug.Log("collided with enemy");
            if(powerUp <= 0)
            {
                //go to lose screen
                SceneManager.LoadScene(3);
                //tell the enemy no more powerups
                if(NoMoreGems != null)
                {
                    NoMoreGems();
                }
            }

            if(powerUp > 0)
            {
                //destroy the enemy
                Destroy(collider.gameObject);
                enemyNumber--;
                //load win scene if all enemies destroyed
                if(enemyNumber <= 0)
                {
                    SceneManager.LoadScene(2);
                }
                //decrease the number of powerups by 1
                powerUp--;
                Debug.Log("powerUp = " + powerUp);
                if(powerUp <= 0)
                {
                    if(NoMoreGems != null)
                    {
                        NoMoreGems();
                    }
                }
            }
        }
    }

    public void ActivateRunning()
    {
        Debug.Log("collided with powerup");
        //increase the number of powerups by 1
        powerUp += 1;
        Debug.Log("powerUp = " + powerUp);
        //SEND MESSAGE TO ENEMY TO RUN AWAY!!
        if (WeGotGems != null)
        {
            WeGotGems();
        }
    }
}
