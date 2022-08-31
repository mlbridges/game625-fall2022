using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character : MonoBehaviour
{
    private CharacterController characterController;
    public int Speed = 2;
    private int powerUp;
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
            }

            if(powerUp > 0)
            {
                //destroy the enemy
                Destroy(collision.gameObject);
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
        }
    }
}
