using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandExecuter : MonoBehaviour
{
    public delegate void DoSomething();
    public static event DoSomething OnRewinding;
    public static event DoSomething OnRewindEnded;
    //first of all, we need to refer to the player
    public MovePlayer player;

    public float HitPoints = 5;

    //we name the command that pops off the move buttons here
    private Command buttonW;
    private Command buttonA;
    private Command buttonS;
    private Command buttonD;

    //recording start position for replay
    private Vector3 startPosition;

    //starting NOT in replay mode
    private bool rewind = false;

    //replaying time
    private const float RewindTime = 0.1f;

    //for replay functionality: a stack that stores all of our moves!
    private Stack<Command> undoCommands = new Stack<Command>();

    // Start is called before the first frame update
    void Start()
    {
        //we ASSIGN the buttons to the command function here
        buttonW = new MovePlyaerForward(player);
        buttonA = new MovePlayerLeft(player);
        buttonS = new MovePlayerBack(player);
        buttonD = new MovePlayerRight(player);
        //recording player's start position in case we die
        startPosition = player.transform.position;
}

    // Update is called once per frame
    void Update()
    {
        if (rewind)
        {
            return;
        }
        //when keys pressed, do their corresponding command
        
        if (Input.GetKeyDown(KeyCode.W))
        {
            ExecuteNewCommand(buttonW);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            ExecuteNewCommand(buttonA);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            ExecuteNewCommand(buttonS);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            ExecuteNewCommand(buttonD);
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("return key pressed");
            StartCoroutine(Rewind());
            rewind = true;
        }

        if(HitPoints == 0)
        {
            Debug.Log("you died! start again");
            StartCoroutine(Rewind());
            rewind = true;
        }
    }

    private IEnumerator Rewind()
    {
        Debug.Log("rewind beginning");
        OnRewinding();
        //convert the undo stack into an array
        Command[] oldCommands = undoCommands.ToArray();
        
        //since the array is inverted, we play it in the order it's already in
        for(int i = 0; i <= oldCommands.Length - 1; i++)
        {
            Debug.Log("rewinding");
            Command currentCommand = oldCommands[i];
            currentCommand.Undo();

            yield return new WaitForSeconds(RewindTime);
        }

        OnRewindEnded();
        Debug.Log("rewind ended");
        HitPoints = 5;
        Debug.Log("new HP: " + HitPoints);
        //clear undo commands stack because we rewound all the way?
        undoCommands.Clear();

        rewind = false;
    }

    //Will execute the command and do stuff to the list to make the replay, undo, redo system work
    private void ExecuteNewCommand(Command commandButton)
    {
        commandButton.Execute();

        //Add the new command to the last position in the list
        undoCommands.Push(commandButton);
    }
}
