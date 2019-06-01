﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBody2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SendLevelUp()
    {
        transform.parent.SendMessage("LevelUp");
    }

    void EndTurn()
    {
        transform.parent.SendMessage("EndTurn");
    }

    void FinishedJump()
    {
        transform.parent.SendMessage("DeactiveReduceLevel");
    }
}
