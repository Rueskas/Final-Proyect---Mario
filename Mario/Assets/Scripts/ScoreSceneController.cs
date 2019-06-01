using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSceneController : MonoBehaviour
{
    public MessageController[] keyboard;
    public MessageController[] scoreBoardMessages;
    public MessageController nameMessage;
    public MessageController finalMessage;
    public MessageController tooLongMessage;
    public GameObject pointer;
    private ScoreBoard scoreBoard;
    private int pointerPosX;
    private int pointerPosY;
    private string newName;
    private bool finished;
    private float timer;
    // Start is called before the first frame update
    void Awake()
    {
        if(GameSceneController.score > 0)
        {
            finished = false;
        }
        else
        {
            finished = true;
        }
        timer = 0;
    }
    void Start()
    {
        pointerPosX = pointerPosY = 0;
        newName = "";

        finalMessage.SetMessage("press space to go back");
        if( finished)
        {
            finalMessage.Draw();
        }
        tooLongMessage.SetMessage("name too long");

        LoadKeyboard();

        scoreBoard = new ScoreBoard("scoreboard.txt");

        DrawScoreboard();
        

    }

    // Update is called once per frame
    void Update()
    {
        if (finished && Input.GetKeyDown(KeyCode.Space))
        {
                GameObject.FindGameObjectWithTag("MainCamera").
                    GetComponent<GameSceneController>().GoBack();
        }
        else if (!finished)
        {
            GetHorizontalInput();
            GetVerticalInput();
            GetModifyInput();
        }
        
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else if (timer < 0)
        {
            tooLongMessage.Hide();
            timer = 0;
        }
    }

    public void HideScoreboard()
    {
        for (int i = 0; i < scoreBoardMessages.Length; i++)
        {
            scoreBoardMessages[i].Hide();
        }
    }

    public void GetHorizontalInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            pointerPosX--;
            pointer.transform.position = new Vector2(pointer.transform.position.x - 0.9f,
                pointer.transform.position.y);
            if (pointerPosX < 0)
            {
                pointerPosX = 9;
                pointer.transform.position = new Vector2(4.15f,
                    pointer.transform.position.y);
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            pointerPosX++;
            pointer.transform.position = new Vector2(pointer.transform.position.x + 0.9f,
                pointer.transform.position.y);
            if (pointerPosX > 9)
            {
                pointerPosX = 0;
                pointer.transform.position = new Vector2(-3.99f,
                    pointer.transform.position.y);
            }
        }
    }

    public void GetVerticalInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            pointerPosY--;
            pointer.transform.position = new Vector2(pointer.transform.position.x,
                pointer.transform.position.y + 1);
            if (pointerPosY < 0)
            {
                pointerPosY = 2;
                pointer.transform.position = new Vector2(pointer.transform.position.x,
                    1);
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            pointerPosY++;
            pointer.transform.position = new Vector2(pointer.transform.position.x,
                pointer.transform.position.y - 1);
            if (pointerPosY > 2)
            {
                pointerPosY = 0;
                pointer.transform.position = new Vector2(
                    pointer.transform.position.x, 3);
            }
        }
    }

    public void GetModifyInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            WriteName();
        }
        else if (Input.GetKeyDown(KeyCode.Backspace))
        {
            DeleteFromName();
        }
    }

    public void LoadKeyboard()
    {

        for (int i = 1; i < keyboard.Length; i++)
        {
            keyboard[i].transform.position = new Vector2(keyboard[0].transform.position.x,
                keyboard[i - 1].transform.position.y - 1);
        }

        keyboard[0].SetMessage("a b c d e f g h i j");
        keyboard[1].SetMessage("k l m n o p q r s t");
        keyboard[2].SetMessage("u v w x y z 1 2 3 end");

        for (int i = 0; i < keyboard.Length; i++)
        {
            keyboard[i].Draw();
        }
    }

    public void DrawScoreboard()
    {
        List<string> namesSorted = scoreBoard.GetNamesSorted();
        Dictionary<string, int> scores = scoreBoard.GetScores();
        for (int i = 0; i < scores.Count; i++)
        {
            if(i > 0)
                scoreBoardMessages[i].transform.position =
                    new Vector2(scoreBoardMessages[0].transform.position.x,
                        scoreBoardMessages[i - 1].transform.position.y - 0.7f);
            switch (i)
            {
                case 0:
                    scoreBoardMessages[i].SetMessage("1st   " +
                        namesSorted[i].PadRight(9, ' ') + 
                            scores[namesSorted[i]]);
                    break;
                case 1:
                    scoreBoardMessages[i].SetMessage("2nd   " +
                         namesSorted[i].PadRight(9, ' ') + 
                            scores[namesSorted[i]]);
                    break;
                case 2:
                    scoreBoardMessages[i].SetMessage("3rd   " +
                         namesSorted[i].PadRight(9, ' ') + 
                            scores[namesSorted[i]]);
                    break;
                default:
                    scoreBoardMessages[i].SetMessage((i+1) + "th   " +
                         namesSorted[i].PadRight(9, ' ') + 
                            scores[namesSorted[i]]);
                    break;
            }
            scoreBoardMessages[i].Draw();
        }
    }

    public void WriteName()
    {
        int posLetter = 0;
        if (pointerPosY != 2 || pointerPosX != 9)
        {
            if (newName.Length < 7)
            {
                foreach (char c in keyboard[pointerPosY].GetMessage())
                {
                    if (c != ' ')
                    {
                        if (posLetter == pointerPosX && (pointerPosY != 2 || pointerPosX != 9))
                        {
                            newName += c;
                            nameMessage.SetMessage(newName);
                            nameMessage.Hide();
                            nameMessage.Draw();
                        }
                        posLetter++;
                    }
                }
            }
            else
            {
                tooLongMessage.Draw();
                timer = 2;
            }
        }
        else
        {
            bool added = 
                scoreBoard.Add(newName, GameSceneController.score);
            if (added)
            {
                finished = true;
                HideScoreboard();
                nameMessage.Hide();
                DrawScoreboard();
                finalMessage.Draw();
                scoreBoard.Save();
            }
        }
    }

    public void DeleteFromName()
    {
        if(newName.Length > 0)
        {
            newName = newName.Substring(0, newName.Length - 1);
            nameMessage.SetMessage(newName);
            nameMessage.Hide();
            nameMessage.Draw();
        }
    }
    
}
