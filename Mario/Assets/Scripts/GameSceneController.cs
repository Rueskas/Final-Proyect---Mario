using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameSceneController : MonoBehaviour
{
    private static int score = 0;
    private static int level = 1;
    private static int enemiesDeath;
    private int enemies;
    private string quantityPlayers;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        quantityPlayers = 
            GameObject.FindGameObjectsWithTag("Player").Length == 1 ? 
            "OnePlayer" : "TwoPlayers";
        enemies = 0;
        enemiesDeath = -1;
    }

    // Update is called once per frame

    void Update()
    {
        print(level);
        if (enemies == enemiesDeath)
        {
            if (level <= 3 &&
                SceneManager.GetActiveScene().name.StartsWith("Scene"))
            {
                enemiesDeath = -1;
                enemies = 0;
                SceneManager.LoadScene("Demonstration" + 1);
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
        }
    }

    public static void EnemyKilled(int punctuation)
    {
        score += punctuation;
        enemiesDeath++;
    }

    public void GoToSceneScore()
    {
        SceneManager.LoadScene("SceneScore");
    }
}
