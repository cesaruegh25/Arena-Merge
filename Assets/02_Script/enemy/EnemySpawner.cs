using System.Collections;
using Unity.Android.Gradle.Manifest;
using UnityEngine;
using static UnityEditor.Progress;

public class EnemySpawner : MonoBehaviour
{
    public EnemySpawnInfo[] enemiesToSpawn;

    public float spawnRadius = 10f;

    public float delayBetweenSpawns = 0.5f;

    public Transform player;
    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        foreach (EnemySpawnInfo enemy in enemiesToSpawn)
        {
            for (int i = 0; i < enemy.amount; i++)
            {
                SpawnEnemy(enemy);
                switch (enemy.enemyData.enemyType)
                {
                    case EnemyType.Basic:
                        delayBetweenSpawns = 2f;
                        break;
                    case EnemyType.Elite:
                        delayBetweenSpawns = 3f;
                        break;
                    case EnemyType.Boss:
                        delayBetweenSpawns = 2f;
                        break;
                }
                yield return new WaitForSeconds(
                    delayBetweenSpawns
                );
            }
        }
    }

    void SpawnEnemy(EnemySpawnInfo enemy)
    {
        Vector2 randomPos =
            (Vector2)player.position +
            Random.insideUnitCircle *
            spawnRadius;

        GameObject obj =
            Instantiate(
                enemy.prefab,
                randomPos,
                Quaternion.identity
            );

        // aplicar datos
        moveEnemy me =
            obj.GetComponent<moveEnemy>();

        if (me != null)
        {
            me.data = enemy.enemyData;
        }
        BoxCollider2D bc =
                obj.GetComponent<BoxCollider2D>();
        if (enemy.enemyData.enemyType == EnemyType.Basic)
        {
            bc.size = new Vector2(2.5f, 4f);
        }
        if (enemy.enemyData.enemyType == EnemyType.Elite)
        {
            bc.size = new Vector2(3.5f, 4f);
        }
        if (enemy.enemyData.enemyType == EnemyType.Boss)
        {
            bc.size = new Vector2(6.5f, 6.5f);
        }

    }
}