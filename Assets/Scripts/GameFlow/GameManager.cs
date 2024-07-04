using Shinobi.Character;
using Shinobi.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Shinobi.GameFlow
{
    public class GameManager : MonoBehaviour
    {
        [Header("프리팹")]
        [SerializeField] private Player playerPrefab = null;

        [SerializeField] private GameObject wall = null;

        [Header("세팅")]
        [SerializeField] private WaveConfig waveConfig = null;
        [SerializeField] private Vector2 playerPosition;
        [SerializeField] private Vector2 enemySpawnPointLeftLimit;
        [SerializeField] private Vector2 enemySpawnPointRightLimit;

        public void Start()
        {
            Player player = Instantiate(playerPrefab, playerPosition, Quaternion.identity);
            player.Init();

            StartCoroutine(GameFlow());
        }

        private IEnumerator GameFlow()
        {
            yield return SpawnEnemies();
        }

        private IEnumerator SpawnEnemies()
        {
            int totalSpawnCount = 0;
            Dictionary<EnemyType, int> enemySpawnCountDic = new Dictionary<EnemyType, int>();

            foreach (var spawnSetting in waveConfig.SpawnSettings)
            {
                totalSpawnCount += spawnSetting.spawnCount;
                enemySpawnCountDic.Add(spawnSetting.enemyType, spawnSetting.spawnCount);
            }

            for (int i = 0; i < totalSpawnCount; i++)
            {
                int randomInt = Random.Range(0, enemySpawnCountDic.Count);
                var enemyType = enemySpawnCountDic.Keys.ToArray()[randomInt];
                float delta = enemySpawnPointRightLimit.x - enemySpawnPointLeftLimit.x;
                Vector2 randomPoint = enemySpawnPointLeftLimit + new Vector2(Random.Range(0, delta), 0);

                var enemy = Instantiate(waveConfig.PrefabSettings[enemyType], randomPoint, Quaternion.identity);
                enemy.Init(wall);

                --enemySpawnCountDic[enemyType];

                if (enemySpawnCountDic[enemyType] == 0)
                {
                    enemySpawnCountDic.Remove(enemyType);
                }

                yield return new WaitForSeconds(waveConfig.SpawnDelay);
            }
        }
    }
}
