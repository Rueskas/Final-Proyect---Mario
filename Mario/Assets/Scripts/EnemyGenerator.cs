using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public Enemy prefabEnemy;
    public Enemy newEnemy;
    protected float generatorTimer = 5f;
    protected int maxEnemies;

    // Start is called before the first frame update
    void Start()
    {
        maxEnemies = 3;
        StartGenerator();
    }

    // Update is called once per frame
    void Update()
    {
        if (maxEnemies == 0)
            CancellGenerator();
    }

    public void CreateStartEnemies()
    {
        Instantiate(prefabEnemy, transform.position, Quaternion.identity);
        maxEnemies--;
    }

    public void StartGenerator()
    {
        InvokeRepeating("CreateStartEnemies", 0f, generatorTimer);
    }

    public void CancellGenerator()
    {
        CancelInvoke("CreateStartEnemies");
    }

    public void CreateEnemy(int level, float speed)
    {
        newEnemy.SetLevel(level);
        newEnemy.SetSpeed(speed);
        Instantiate(newEnemy, transform.position, Quaternion.identity);

    }
}
