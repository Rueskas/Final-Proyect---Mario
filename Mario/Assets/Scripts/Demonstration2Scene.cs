using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demonstration2Scene : MonoBehaviour
{
    public GameObject enemy;
    public GameObject player;
    public MessageController messageDemonstration;
    public MessageController messageInstruccions1;
    public MessageController messageInstruccions2;
    public MessageController messageInstruccions3;
    protected PlayerDemonstration playerScript;
    protected Enemy2 enemyScript;
    protected enum ScenePhase {Waiting, Level2, Level1, MovePlayer, Kill};
    protected ScenePhase scenePhase;
    // Start is called before the first frame update
    void Awake()
    {
        scenePhase = ScenePhase.Waiting;
    }
    void Start()
    {
        playerScript = player.GetComponent<PlayerDemonstration>();
        enemyScript = enemy.GetComponent<Enemy2>();
        messageDemonstration.SetMessage("demonstration");
        messageInstruccions1.SetMessage("first hit makes it mad");
        messageInstruccions2.SetMessage("second hit makes it over");
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
        if (scenePhase == ScenePhase.Level2)
        {
            enemy.SetActive(true);
            if(posXEnemy < 4)
            {
                playerScript.Jump();
                messageInstruccions1.Draw();
            }
        }
        else if (scenePhase == ScenePhase.MovePlayer)
        {
            playerScript.MoveLeft();
        }
        else if (scenePhase == ScenePhase.Level1)
        {
            messageInstruccions1.Hide();
            messageInstruccions2.Draw();
            playerScript.Jump();
        }
        else if (scenePhase == ScenePhase.Kill)
        {
            if(posYPlayer  < -1.5)
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
            scenePhase = ScenePhase.Level2;
        }
        else if (enemyScript.GetLevel() < 2 && scenePhase == ScenePhase.Level2)
        {
            scenePhase = ScenePhase.MovePlayer;
        }
        else if (posXPlayer < 0 && posXPlayer > -0.6 &&
                        scenePhase == ScenePhase.MovePlayer)
        {
            scenePhase = ScenePhase.Level1;
        }
        else if (enemyScript.GetLevel() < 1 && scenePhase == ScenePhase.Level1)
        {
            scenePhase = ScenePhase.MovePlayer;
        }
        else if (posXPlayer < -4)
        {
            scenePhase = ScenePhase.Kill;
        }

    }
}
