using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    //need float for walking speed
    private const float WalkingSpeed = 1.5f;
    // Start is called before the first frame update

    //need voids for each direction of movement, which pop off when command executes
    public void MoveForward()
    {
        Move(Vector3.forward);
    }

    public void MoveBack()
    {
        Move(Vector3.back);
    }

    public void MoveLeft()
    {
        Move(Vector3.left);
    }

    public void MoveRight()
    {
        Move(Vector3.right);
    }

    //for efficiency in typing out the other voids:
    private void Move(Vector3 dir)
    {
        transform.Translate(dir * WalkingSpeed);
    }
}
