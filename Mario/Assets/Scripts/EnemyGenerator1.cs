using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator1 : EnemyGenerator
{
    public Enemy prefabEnemy;

    public override void CreateEnemy(int level, float speed, int type)
    {
        audioSource.Play();
        Enemy enemyCreated = 
            Instantiate(prefabEnemy, transform.position, Quaternion.identity);

        enemyCreated.SetSpeed(speed);
        enemyCreated.SetLevel(level);
    }

    public override void CreateStartEnemies()
    {
        audioSource.Play();
        Instantiate(prefabEnemy, transform.position, Quaternion.identity);
        maxEnemies--;
    }
}
