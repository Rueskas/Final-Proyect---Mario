using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyGenerator : MonoBehaviour
{
    private float generatorTimer;
    private float initialTimer;
    protected int maxEnemies;

    // Start is called before the first frame update
    void Awake()
    {
    }

    void Start()
    {
        maxEnemies = 3;
        initialTimer = Random.Range(0, 3);
        generatorTimer = Random.Range(5, 15);
        StartGenerator();
    }

    // Update is called once per frame
    void Update()
    {
        if (maxEnemies == 0)
            CancellGenerator();
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

    public abstract void CreateEnemy(int level, float speed);
}
