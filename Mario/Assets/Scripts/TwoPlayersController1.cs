using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoPlayersController1 : PlayerController
{
    protected override void GetInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal2");
        jumpInput = Input.GetAxisRaw("Jump");
    }

    protected override void SetPositionLives()
    {
        coordinatesLives = new Coordinates(-7.12f, 4.09f);
    }
}
