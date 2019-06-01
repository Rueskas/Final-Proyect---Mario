using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameSceneController : MonoBehaviour
{
    public static int score = 0;
    public static int level = 1;
    public static int enemiesDeath;
    public static int playersDeath;

    private int enemies;
    private string quantityPlayers;
    protected MultiEnemyGeneratorController[] multiEnemyGenerators;

    public MessageController topScoreMessage;
    public MessageController actualScoreMessage;
    public GameObject topSprite;

    public AudioClip startScene;
    protected AudioSource audioSource;
    
    private static GameSceneController instance = null;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        quantityPlayers = "OnePlayer";
        enemies = 0;
        enemiesDeath = -1;
        playersDeath = 0;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame

    void Update()
    {
        if (enemies == enemiesDeath)
        {
            ChangeScene();
        }
        
        SetActiveScoreMessage();
        
        if (SceneManager.GetActiveScene().name.Contains("Level"))
        {
            if (quantityPlayers == "OnePlayer" && playersDeath == 1)
            {
                GoToSceneScore();
            }
            else if (quantityPlayers == "TwoPlayers" &&
                    playersDeath == 2)
            {
                GoToSceneScore();
            }
        }
        
        //If press Q you can advance to the next Scene
        if (Input.GetKeyDown(KeyCode.Q))
        {
            enemiesDeath = enemies;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

    }

    public void GoToSceneScore()
    {
        SceneManager.LoadScene("SceneScore");
    }

    public static void EnemyKilled(int punctuation)
    {
        if(score < 99999)
            score += punctuation;
        enemiesDeath++;
    }

    public static void PlayerDeath()
    {
        playersDeath++;
    }

    public void ChangeScene()
    {
        if (level <= 3 &&
                SceneManager.GetActiveScene().name.StartsWith("Scene"))
        {
            enemiesDeath = -1;
            enemies = 0;
            SceneManager.LoadScene("Demonstration" + level);
        }
        else if (SceneManager.GetActiveScene().name.
            StartsWith("Demonstration"))
        {
            enemies = 2 * level;
            enemiesDeath = 0;
            SceneManager.LoadScene("Scene" + quantityPlayers +
                "Level" + level);
            level++;
        }
        else
        {
            enemies = 2 * level;
            enemiesDeath = 0;
            SceneManager.LoadScene("Scene" + quantityPlayers +
                "Level4");
            multiEnemyGenerators = 
                FindObjectsOfType<MultiEnemyGeneratorController>();
            for (int i = 0; i < multiEnemyGenerators.Length; i++)
            {
                multiEnemyGenerators[i].SetMaxEnemies(level);
            }
        }
        audioSource.clip = startScene;
        audioSource.Play();
    }

    public void SetActiveScoreMessage()
    {
        if (SceneManager.GetActiveScene().name.Contains("Level"))
        {
            LoadTopScore();
            UpdateScoreMessage();
            topSprite.SetActive(true);
        }
        else
        {
            topScoreMessage.Hide();
            actualScoreMessage.Hide();
            topSprite.SetActive(false);
        }
    }

    public void GoBack()
    {
        SceneManager.LoadScene("InitialScene");
        score = 0;
        level = 1;
        quantityPlayers = "OnePlayer";
        enemies = 0;
        playersDeath = 0;
        enemiesDeath = -1;
    }

    public void SetQuantityPlayers(string quantityPlayers)
    {
        this.quantityPlayers = quantityPlayers;
    }

    public void StartDemonstration1()
    {
        SceneManager.LoadScene("Demonstration1");
    }

    public string GetQuantityPlayers()
    {
        return quantityPlayers;
    }

    public void LoadTopScore()
    {
        ScoreBoard sc = new ScoreBoard("scoreboard.txt");
        int topScore = sc.GetScore(sc.GetNamesSorted()[0]);
        topScoreMessage.SetMessage(System.Convert.ToString(topScore));
        topScoreMessage.Draw();
    }
    
    public void UpdateScoreMessage()
    {
        actualScoreMessage.Hide();
        actualScoreMessage.SetMessage("score " + 
            System.Convert.ToString(score));
        actualScoreMessage.Draw();
    }

}
