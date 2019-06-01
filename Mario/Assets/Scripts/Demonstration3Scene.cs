using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demonstration3Scene : MonoBehaviour
{
    public GameObject enemy;
    public GameObject player;
    public MessageController messageDemonstration;
    public MessageController messageInstruccions1;
    public MessageController messageInstruccions2;
    public MessageController messageInstruccions3;
    protected PlayerDemonstration playerScript;
    protected Enemy3 enemyScript;
    protected enum ScenePhase { Waiting, Jump, MovePlayer, Stunned, Kill };
    protected ScenePhase scenePhase;
    protected bool changePhase;

    void Awake()
    {
        scenePhase = ScenePhase.Waiting;
    }
    // Start is called before the first frame update
    void Start()
    {
        changePhase = false;
        playerScript = player.GetComponent<PlayerDemonstration>();
        enemyScript = enemy.GetComponent<Enemy3>();
        messageDemonstration.SetMessage("demonstration");
        messageInstruccions1.SetMessage("one hit flip it over");
        messageInstruccions2.SetMessage("only when touching floor");
        messageInstruccions3.SetMessage("kick off when upside down");
        playerScript.SetIdle();
        messageDemonstration.Draw();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ControllePlayer();

        ChangeScenePhase();
    }

    private void ControllePlayer()
    {
        float posXEnemy = enemy.GetComponent<Transform>().position.x;
        float posYPlayer = player.GetComponent<Transform>().position.y;
        float posXPlayer = player.GetComponent<Transform>().position.x;
        if (scenePhase == ScenePhase.Jump)
        {
            if (posXEnemy < 2.3)
            {
                if(posXEnemy > 2)
                    playerScript.Jump();
                if(posXEnemy < 1)
                    changePhase = true;
            }
        }
        else if (scenePhase == ScenePhase.MovePlayer)
        {
            if (posXEnemy < -1.2)
            {
                playerScript.Jump();
                messageInstruccions1.Draw();
                messageInstruccions2.Draw();
                if(enemy.GetComponent<Enemy3>().GetStun())
                    changePhase = true;
            }
            else if (posXEnemy > -0.7)
            {
                playerScript.MoveLeft();
            }
            else
                playerScript.SetIdle();
        }
        else if(scenePhase == ScenePhase.Stunned)
        {
            if(posXPlayer > -5)
            {
                playerScript.MoveLeft();
            }
            else
            {
                changePhase = true;
            }
        }
        else if (scenePhase == ScenePhase.Kill)
        {
            if (posYPlayer < -1.5)
            {
                playerScript.Jump();
                messageInstruccions2.Hide();
                messageInstruccions3.Draw();
            }
            playerScript.MoveRight();
        }
    }

    private void ChangeScenePhase()
    {
        float posXPlayer = player.GetComponent<Transform>().position.x;

        if (scenePhase == ScenePhase.Waiting)
        {
            enemy.SetActive(true);
            scenePhase = ScenePhase.Jump;
        }
        else if(scenePhase == ScenePhase.Jump && changePhase)
        {
            scenePhase = ScenePhase.MovePlayer;
            changePhase = false;
        }
        else if(scenePhase == ScenePhase.MovePlayer && changePhase)
        {
            scenePhase = ScenePhase.Stunned;
            changePhase = false;
        }
        else if(scenePhase == ScenePhase.Stunned && changePhase)
        {
            scenePhase = ScenePhase.Kill;
            changePhase = false;
        }
    }
}
