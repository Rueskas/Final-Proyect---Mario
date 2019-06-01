using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitialScene : MonoBehaviour
{
    public MessageController messageOnePlayer;
    public MessageController messageTwoPlayers;
    public MessageController messageScoreBoard;
    public MessageController messageLeave;
    public MessageController messageCoins;
    public MessageController messageInsertCoins;
    public MessageController messageCoinsNeeded;
    public MessageController messageHelp;
    public Transform messageTitleTransform;
    public GameObject pointer;
    public GameSceneController gameSceneController;
    private Transform pointerTransform;
    private int coins;
    private bool started;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        gameSceneController = 
            GameObject.FindGameObjectWithTag("MainCamera").
                GetComponent<GameSceneController>();
        pointerTransform = pointer.GetComponent<Transform>();
        started = false;
        coins = 0;
        timer = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (started)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow) &&
            pointerTransform.position.y > -3.5)
            {
                gameSceneController.SetQuantityPlayers("TwoPlayers");
                pointerTransform.position =
                    new Vector2(pointerTransform.position.x,
                        pointerTransform.position.y - 0.65f);
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) &&
                pointerTransform.position.y < -2.2)
            {
                pointerTransform.position =
                    new Vector2(pointerTransform.position.x,
                        pointerTransform.position.y + 0.65f);
                if(pointerTransform.position.y > -2.25)
                    gameSceneController.SetQuantityPlayers("OnePlayer");
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if(pointerTransform.position.y > -2.25 &&
                    gameSceneController.GetQuantityPlayers().Contains("One")
                        && coins >= 1)
                    gameSceneController.StartDemonstration1();
                else if (pointerTransform.position.y > -2.25 && 
                        gameSceneController.GetQuantityPlayers().Contains("One"))
                {
                    DrawWarningMessage(1 - coins);
                }
                else if (pointerTransform.position.y < -2.2 &&
                     pointerTransform.position.y > -2.5  &&
                     gameSceneController.GetQuantityPlayers().Contains("Two")
                        && coins >= 2)
                    gameSceneController.StartDemonstration1();
                else if (pointerTransform.position.y < -2.2 &&
                     pointerTransform.position.y > -2.5 && 
                     gameSceneController.GetQuantityPlayers().Contains("Two"))
                {
                    DrawWarningMessage(2 - coins);
                }
                else if (pointerTransform.position.y < -2.4 &&
                     pointerTransform.position.y > -3.1)
                {
                    gameSceneController.GoToSceneScore();
                }
                else if (pointerTransform.position.y < -3.25)
                {
                    Application.Quit();
                }
            }
            else if (Input.GetKeyDown(KeyCode.C))
            {
                SetCoins(coins+1);
            }
        }
        else
        {
            messageTitleTransform.position = new Vector2(
                messageTitleTransform.position.x,
                    messageTitleTransform.position.y - 0.05f);
            if(messageTitleTransform.position.y < 2.5)
            {
                started = true;
                Unlock();
            }
        }

        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else if (timer < 0)
        {
            messageCoinsNeeded.Hide();
            timer = 0;
        }
        
    }

    private void Unlock()
    {
        messageInsertCoins.SetMessage("press c to insert coin");
        messageLeave.SetMessage("leave ");
        messageScoreBoard.SetMessage("scoreboard");
        messageCoins.SetMessage("coins " + coins);
        messageOnePlayer.SetMessage("one player");
        messageTwoPlayers.SetMessage("two players");
        messageHelp.SetMessage("press space to start");

        messageInsertCoins.Draw();
        messageOnePlayer.Draw();
        messageTwoPlayers.Draw();
        messageScoreBoard.Draw();
        messageLeave.Draw();
        messageCoins.Draw();
        messageHelp.Draw();
    }

    public void SetCoins(int coins)
    {
        this.coins = coins;
        messageCoins.SetMessage("coins " + coins);
        messageCoins.Hide();
        messageCoins.Draw();
    }

    public void DrawWarningMessage(int coinsNeeded)
    {
        messageCoinsNeeded.SetMessage("you need " + coinsNeeded + " more coins");
        messageCoinsNeeded.Draw();
        timer = 1;
    }
    
}
