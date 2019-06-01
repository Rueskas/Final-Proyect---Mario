using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBody3 : MonoBehaviour
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

    void SendReadyToJump()
    {
        transform.parent.SendMessage("ReadyToJump");
    }

    void SendRevive()
    {
        transform.parent.SendMessage("Revive");
    }
}
