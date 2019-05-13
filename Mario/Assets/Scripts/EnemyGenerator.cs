using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject prefabEnemy;
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

    void CreateEnemy()
    {
        Instantiate(prefabEnemy, transform.position, Quaternion.identity);
        maxEnemies--;
    }

    public void StartGenerator()
    {
        InvokeRepeating("CreateEnemy", 0f, generatorTimer);
    }

    public void CancellGenerator()
    {
        CancelInvoke("CreateEnemy");
    }
}
