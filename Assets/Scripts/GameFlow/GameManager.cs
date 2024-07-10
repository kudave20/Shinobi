using Shinobi.Character;
using Shinobi.Core;
using Shinobi.UI;
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
        [SerializeField] private UIManager uiManager = null;

        [SerializeField] private GameObject wall = null;

        [Header("세팅")]
        [SerializeField] private WaveConfig waveConfig = null;
        [SerializeField] private Vector2 playerPosition;
        [SerializeField] private Vector2 enemySpawnPointLeftLimit;
        [SerializeField] private Vector2 enemySpawnPointRightLimit;

        private bool isCardSelected = false;
        private List<Enemy> enemies = new List<Enemy>();

        public void Start()
        {
            Player player = Instantiate(playerPrefab, playerPosition, Quaternion.identity);
            player.Init();

            player.onLevelUp += () => StartCoroutine(OnPlayerLevelUp());

            uiManager.Init(player, OnCardSelected);

            StartCoroutine(GameFlow());
        }

        private void OnCardSelected()
        {
            isCardSelected = true;
        }

        private IEnumerator OnPlayerLevelUp()
        {
            float originalTimeScale = Time.timeScale;

            Time.timeScale = 0;

            uiManager.CardSelectUI.Enable();

            yield return new WaitUntil(() => isCardSelected);

            isCardSelected = false;

            uiManager.CardSelectUI.Disable();

            Time.timeScale = originalTimeScale;
        }

        private IEnumerator GameFlow()
        {
            yield return SpawnEnemies();

            yield return new WaitUntil(() => enemies.Count == 0);

            GameOver(true);
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
                enemy.Init(wall, () => GameOver(false), x => enemies.Remove(x));

                if (enemy is Slime slime)
                {
                    slime.onMiniSpawned += x => enemies.Add(x);
                }

                enemies.Add(enemy);

                --enemySpawnCountDic[enemyType];

                if (enemySpawnCountDic[enemyType] == 0)
                {
                    enemySpawnCountDic.Remove(enemyType);
                }

                yield return new WaitForSeconds(waveConfig.SpawnDelay);
            }
        }

        private void GameOver(bool hasClear)
        {
            Time.timeScale = 0;

            if (hasClear)
            {
                uiManager.ClearUI.gameObject.SetActive(true);
            }
            else
            {
                uiManager.OverUI.gameObject.SetActive(true);
            }
        }
    }
}
