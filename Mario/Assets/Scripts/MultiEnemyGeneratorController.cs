using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiEnemyGeneratorController : EnemyGenerator
{
    public Enemy prefabEnemy1;
    public Enemy2 prefabEnemy2;
    public Enemy3 prefabEnemy3;

    public override void CreateEnemy(int level, float speed, int type)
    {
        audioSource.Play();
        if (type == 1)
        {
            Enemy enemyCreated =
                Instantiate(prefabEnemy1, transform.position, Quaternion.identity);
            enemyCreated.SetSpeed(speed);
            enemyCreated.SetLevel(level);
        }
        else if (type == 2)
        {
            Enemy2 enemyCreated =
                Instantiate(prefabEnemy2, transform.position, Quaternion.identity);
            enemyCreated.SetSpeed(speed);
            enemyCreated.SetLevel(level);
        }
        else
        {
            Enemy3 enemyCreated =
                Instantiate(prefabEnemy3, transform.position, Quaternion.identity);
            enemyCreated.SetSpeed(speed);
            enemyCreated.SetLevel(level);
        }

    }

    public override void CreateStartEnemies()
    {
        audioSource.Play();
        int type = Random.Range(1, 4);
        if(type == 1)
        {
            Instantiate(prefabEnemy1, transform.position, Quaternion.identity);
        }
        else if (type == 2)
        {
            Instantiate(prefabEnemy2, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(prefabEnemy3, transform.position, Quaternion.identity);
        }
        maxEnemies--;
    }
    
}
