using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnePlayerController : PlayerController
{
    protected override void GetInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        jumpInput = Input.GetAxisRaw("Jump");
    }

    protected override void SetPositionLives()
    {
        coordinatesLives = new Coordinates(-7.12f, 4.09f);
    }
}
