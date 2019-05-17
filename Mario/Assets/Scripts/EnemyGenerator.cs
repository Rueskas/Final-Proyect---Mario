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
        initialTimer = Random.Range(0, 3);
        generatorTimer = Random.Range(5, 10);
        StartGenerator();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        print("max " + maxEnemies);
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
        print("empezado");
        InvokeRepeating("CreateStartEnemies", initialTimer, generatorTimer);
    }

    public void CancellGenerator()
    {
        print("cancelado");
        CancelInvoke("CreateStartEnemies");
    }

    public abstract void CreateEnemy(int level, float speed);
}
