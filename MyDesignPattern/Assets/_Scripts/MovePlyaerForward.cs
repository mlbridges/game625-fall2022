using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlyaerForward : Command
{
    //finding a way to refer to the player mover
    private MovePlayer movePlayer;

    public MovePlyaerForward(MovePlayer movePlayer)
    {
        this.movePlayer = movePlayer;
    }

    public override void Execute()
    {
        //when command is executed on this script, play move back function in player mover
        movePlayer.MoveForward();
    }

    public override void Undo()
    {
        //when command is executed on this function, play move forward function in player mover
        movePlayer.MoveBack();
    }
}
