using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayerLeft : Command
{
    //finding a way to refer to the player mover
    private MovePlayer movePlayer;

    public MovePlayerLeft(MovePlayer movePlayer)
    {
        this.movePlayer = movePlayer;
    }

    public override void Execute()
    {
        //when command is executed on this script, play move back function in player mover
        movePlayer.MoveLeft();
    }

    public override void Undo()
    {
        //when command is executed on this function, play move forward function in player mover
        movePlayer.MoveRight();
    }
}
