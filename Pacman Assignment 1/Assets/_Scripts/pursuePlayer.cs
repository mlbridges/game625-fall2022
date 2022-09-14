using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//script that controls enemy movement (pursuit vs escaping), attached to enemy
public class pursuePlayer : MonoBehaviour
{
    //need to refer to: player transform
    public GameObject player;
    //bool for is frozen
    public bool IsPowerUp;

    //enemy waypoint declarations
    public Transform[] Waypoints;
    private Transform currentDestination;
    private int finalIndex;
    private int curIndex = 0;
    private float minDist = 0.1f;
    private NavMeshAgent enemyAgent;

    //running away waypoint
    public Transform RunaWaypoint;

    // Start is called before the first frame update
    void Start()
    {
        IsPowerUp = false;

        //subscribing to the event from character script telling us if pacman has powerups
        character.WeGotGems += RunAway;
        character.NoMoreGems += OpenSeason;

        //waypoint stuff
        //setting up a variable for the enemy's NavMesh
        enemyAgent = GetComponent<NavMeshAgent>();

        //marking the final waypoint so we can cycle through them
        finalIndex = Waypoints.Length;

        //setting the starting destination as zero/the first waypoint
        currentDestination = Waypoints[curIndex];

        ////error shit to hopefully fix navmesh from unity forum
        //GameObject go = new GameObject("Target");
        //Vector3 sourcePostion = new Vector3(-3.41f, 0, 0.05f);//The position you want to place your agent
        //NavMeshHit closestHit;
        //if (NavMesh.SamplePosition(sourcePostion, out closestHit, 500, 1))
        //{
        //    go.transform.position = closestHit.position;
        //    go.AddComponent<NavMeshAgent>();
        //    //TODO
        //}
        //else
        //{
        //    Debug.Log("...");
        //}
    }
    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        //drawing a ray from me to player (looking for player)
        if(Physics.Raycast(transform.position, player.transform.position - transform.position, out hit))
        {
            if(hit.transform == player.transform)
            {
                if(IsPowerUp == false)
                {
                    //when i hit the player AND NO POWERUP, move toward the player
                    EnemyPursue();
                }
                if(IsPowerUp == true)
                {
                    //if powerup RUN AWAY
                    StartCoroutine(EnemyEscape());
                }
            }
            else
            {
                if (IsPowerUp == true)
                {
                    //if powerup RUN AWAY
                    StartCoroutine(EnemyEscape());
                }
                if(IsPowerUp == false)
                {
                    //enemy patrols waypoints
                    enemyAgent.SetDestination(currentDestination.position);
                    EnemyPatrol();
                }
            }
        }
    }

    public void EnemyPursue()
    {
        //enemy always moves toward the player using the NavMesh agent to navigate the environment
        //transform.GetComponent<NavMeshAgent>.SetDestination(player.transform.position);
        Debug.Log("enemy sees player");
        //set destination to the player's position IF NO POWERUP
        if(IsPowerUp == false)
        {
            enemyAgent.SetDestination(player.transform.position);
        }
    }

    public void EnemyPatrol()
    {
        //enemy patrols waypoints
        //checking to see if the enemy has reached the waypoint
        if (enemyAgent.remainingDistance <= minDist)
        {
            //if they are close enough, the destination iterates to the next one
            curIndex++;

            //checking to see if the current index is bigger than the number of waypoints
            if (curIndex >= finalIndex)
            {
                //and reseting it to the beginning if it is
                curIndex = 0;
            }

            //setting the destination to the current index
            currentDestination = Waypoints[curIndex];
        }

        //sending the enemy to the current destination
        enemyAgent.SetDestination(currentDestination.position);
    }

    IEnumerator EnemyEscape()
    {
        //this only happens WHEN POWERUP IS EATEN
        if(IsPowerUp == true)
        {
            Debug.Log("enemy is escaping");
            //make the enemy run to a far away waypoint for a set amount of time, and then go back to patroling
            enemyAgent.SetDestination(RunaWaypoint.position);
            //enemy escapes for a certain amount of time - coroutine??
            yield return new WaitForSeconds(10);
            //then the powerup fades and we can go back into pursuit mode as needed
            IsPowerUp = false;
        }
    }

    //tells the enemy we got gems! run!
    public void RunAway()
    {
        IsPowerUp = true;
    }

    //tells the enemy we outta gems
    public void OpenSeason()
    {
        IsPowerUp = false;
    }
}
