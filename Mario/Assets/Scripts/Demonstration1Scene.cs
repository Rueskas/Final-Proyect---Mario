using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demonstration1Scene : MonoBehaviour
{
    public GameObject enemy;
    public GameObject player;
    public GameObject messageDemonstration;
    protected PlayerDemonstration playerScript;
    protected EnemyDemonstration enemyScript;
    protected enum ScenePhase { MovePlayer, JumpPlayer, Attack, Kill };
    protected ScenePhase scenePhase = ScenePhase.MovePlayer;
    // Start is called before the first frame update
    void Start()
    {
        playerScript = player.GetComponent<PlayerDemonstration>();
        enemyScript = enemy.GetComponent<EnemyDemonstration>();
    }

    // Update is called once per frame
    void Update()
    {
        if (scenePhase == ScenePhase.MovePlayer)
        {
            playerScript.MoveLeft();
        }
        else if (scenePhase == ScenePhase.JumpPlayer)
        {
            if (playerScript.IsGrounded() && !enemyScript.IsStunned())
            {
                playerScript.Jump();
            }
            else if (enemyScript.IsStunned())
            {
                scenePhase = ScenePhase.Attack;
            }
        }
        else if (scenePhase == ScenePhase.Attack)
        {
            if (player.GetComponent<Transform>().position.x > -4.5f)
            {
                playerScript.MoveLeft();
            }
            else if (playerScript.IsGrounded())
            {
                playerScript.Jump();
            }
            else
            {
                scenePhase = ScenePhase.Kill;
            }
        }
        else if (enemyScript.IsStunned())
        {
            playerScript.MoveRight();
        }
        else
            playerScript.SetIdle();


        if (player.GetComponent<Transform>().position.x < -1f
                && scenePhase == ScenePhase.MovePlayer)
        scenePhase = ScenePhase.JumpPlayer;
    }

    private void ShowMessage(GameObject message)
    {
        message.SetActive(true); // TO DO
    }

    private void HideMessage(GameObject message)
    {
        message.SetActive(true);//TO DO
    }
}
