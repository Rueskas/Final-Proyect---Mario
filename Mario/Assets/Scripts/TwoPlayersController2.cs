using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoPlayersController2 : PlayerController
{
    protected override void GetInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        jumpInput = Input.GetAxisRaw("Jump2");
    }

    protected override void SetPositionLives()
    {
        coordinatesLives = new Coordinates(-7.12f, 3.60f);
    }
}
