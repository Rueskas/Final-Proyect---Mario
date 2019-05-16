using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator2 : EnemyGenerator
{
    public Enemy2 prefabEnemy;
    public Enemy2 newEnemy;

    public override void CreateEnemy(int level, float speed)
    {
        newEnemy.SetSpeed(speed);
        newEnemy.SetLevel(level);
        Instantiate(newEnemy, transform.position, Quaternion.identity);
    }

    public override void CreateStartEnemies()
    {
        Instantiate(prefabEnemy, transform.position, Quaternion.identity);
        maxEnemies--;
    }
}
