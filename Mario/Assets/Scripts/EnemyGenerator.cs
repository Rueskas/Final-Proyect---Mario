using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class EnemyGenerator : MonoBehaviour
{
    protected float generatorTimer;
    private float initialTimer;
    public int maxEnemies;

    protected AudioSource audioSource;
    public AudioClip generateEnemyAudio;

    // Start is called before the first frame update
    void Awake()
    {
        maxEnemies = System.Convert.ToInt32(SceneManager.GetActiveScene().name.Substring(
            SceneManager.GetActiveScene().name.LastIndexOf("l") + 1));
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = generateEnemyAudio;
    }

    void Start()
    {
        initialTimer = Random.Range(0, 3);
        generatorTimer = Random.Range(5, 10);
        StartGenerator();
    }

    // Update is called once per frame
    void Update()
    {
        if (maxEnemies <= 0)
            CancellGenerator();
    }

    public void SetMaxEnemies(int maxEnemies)
    {
        this.maxEnemies = maxEnemies;
    }

    public abstract void CreateStartEnemies();

    public void StartGenerator()
    {
        InvokeRepeating("CreateStartEnemies", initialTimer, generatorTimer);
    }

    public void CancellGenerator()
    {
        CancelInvoke("CreateStartEnemies");
    }

    public abstract void CreateEnemy(int level, float speed, int type);
}
