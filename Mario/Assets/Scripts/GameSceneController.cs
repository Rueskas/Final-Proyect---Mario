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
    public PlayerController player;
    private GameObject[] generators;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        generators = GameObject.FindGameObjectsWithTag("Generator");
        foreach (GameObject generator in generators)
        {
            generator.GetComponent<EnemyGenerator>().SetMaxEnemies(level);
        }
        enemies = generators.Length * level;
        enemiesDeath = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (player.GetLives() == 0)
        {
            //SceneManager.LoadScene("ScoreScene"); TO DO
        }
        if (enemies == enemiesDeath)
        {
            level++;
            SceneManager.LoadScene("SceneOnePlayerLevel" + level);
            Restart();
        }
    }

    public static void EnemyKilled(int punctuation)
    {
        score += punctuation;
        enemiesDeath++;
    }

    public void Restart()
    {
        generators = GameObject.FindGameObjectsWithTag("Generator");
        foreach (GameObject generator in generators)
        {
            generator.GetComponent<EnemyGenerator>().SetMaxEnemies(level);
        }
        enemies = generators.Length * level;
        enemiesDeath = 0;
    }
}
