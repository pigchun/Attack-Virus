using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;    // 要生成的敌人预制体
    public float spawnInterval = 3f;  // 生成间隔时间
    public Transform[] spawnPoints;   // 生成点位置数组

    private void Start()
    {
        // 开始重复调用生成敌人的函数
        StartCoroutine(SpawnEnemies());
    }


    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            // 检查 spawnPoints 是否为空或没有元素
            if (spawnPoints == null || spawnPoints.Length == 0)
            {
                Debug.LogError("No spawn points assigned in EnemySpawner.");
                yield break;  // 退出协程，避免继续生成
            }

            // 随机选择一个生成点
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // 生成敌人
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);

            // 等待一段时间再生成下一个敌人
            yield return new WaitForSeconds(spawnInterval);
        }
    }

}
