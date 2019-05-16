using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator1 : EnemyGenerator
{
    public Enemy prefabEnemy;
    public Enemy newEnemy;

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
