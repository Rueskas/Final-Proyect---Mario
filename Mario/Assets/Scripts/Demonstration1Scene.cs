using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Demonstration1Scene : MonoBehaviour
{
    public GameObject enemy;
    public GameObject player;
    public MessageController messageDemonstration;
    public MessageController messageArrows;
    public MessageController messageJump;
    public MessageController messageInstruccions1;
    public MessageController messageInstruccions2;
    public MessageController messageInstruccions3;
    protected PlayerDemonstration playerScript;
    protected Enemy enemyScript;
    protected enum ScenePhase { Waiting, MovePlayer, JumpPlayer, Attack, Kill };
    protected ScenePhase scenePhase;
    // Start is called before the first frame update
    void Start()
    {
        scenePhase = ScenePhase.Waiting;
        playerScript = player.GetComponent<PlayerDemonstration>();
        enemyScript = enemy.GetComponent<Enemy>();
        playerScript.SetIdle();
        messageDemonstration.SetMessage("demonstration");
        messageArrows.SetMessage("to move use left and right arrow");
        messageJump.SetMessage("to jump use espace");
        messageInstruccions1.SetMessage("one hit flip it over");
        messageInstruccions2.SetMessage("jump up");
        messageInstruccions3.SetMessage("kick off when upside down");
        messageArrows.Draw();
        messageJump.Draw();
        messageDemonstration.Draw();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time > 4 && scenePhase == ScenePhase.Waiting)
        {
            scenePhase = ScenePhase.MovePlayer;
        }

        if (scenePhase == ScenePhase.MovePlayer)
        {
            enemy.SetActive(true);
            messageArrows.Hide();
            messageJump.Hide();
            playerScript.MoveLeft();
        }
        else if (scenePhase == ScenePhase.JumpPlayer)
        {
            if (playerScript.IsGrounded() && !enemyScript.GetStun()
                && enemy.GetComponent<Transform>().position.x < 2)
            {
                playerScript.Jump();
            }
            else if (enemyScript.GetStun())
            {
                messageInstruccions1.Draw();
                scenePhase = ScenePhase.Attack;
            }
            else
                playerScript.SetIdle();
        }
        else if (scenePhase == ScenePhase.Attack)
        {
            if (player.GetComponent<Transform>().position.x > -4.5f)
            {
                playerScript.MoveLeft();
            }
            else if (playerScript.IsGrounded())
            {
                messageInstruccions1.Hide();
                messageInstruccions2.Draw();
                playerScript.Jump();
            }
            else
            {
                scenePhase = ScenePhase.Kill;
            }
        }
        else if (enemyScript.GetStun())
        {
            messageInstruccions3.Draw();
            playerScript.MoveRight();
        }


        if (player.GetComponent<Transform>().position.x < -1f
                && scenePhase == ScenePhase.MovePlayer)
            scenePhase = ScenePhase.JumpPlayer;
    }

}

