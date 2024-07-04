using Shinobi.Character;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Shinobi.Core
{
    [CreateAssetMenu(fileName = "Wave", menuName = "Waves/New Wave", order = 0)]
    public class WaveConfig : ScriptableObject
    {
        [SerializeField] private float spawnDelay = 0.5f;
        [SerializeField] private List<SpawnSetting> spawnSettings = null;
        [SerializeField] private List<PrefabSetting> prefabSettings = null;

        public float SpawnDelay => spawnDelay;
        public List<SpawnSetting> SpawnSettings => spawnSettings;
        public Dictionary<EnemyType, Enemy> PrefabSettings => prefabSettings.ToDictionary(x => x.enemyType, x => x.prefab);

        private Dictionary<EnemyType, int> spawnSettingDic = null;

        [System.Serializable]
        public class SpawnSetting
        {
            public EnemyType enemyType;
            public int spawnCount;
        }

        [System.Serializable]
        public class PrefabSetting
        {
            public EnemyType enemyType;
            public Enemy prefab;
        }

        public int GetSpawnCount(EnemyType enemyType)
        {
            BuildLookup();

            return spawnSettingDic[enemyType];
        }

        private void BuildLookup()
        {
            if (spawnSettingDic != null) return;

            spawnSettingDic = new Dictionary<EnemyType, int>();

            foreach (var spawnSetting in spawnSettings)
            {
                spawnSettingDic[spawnSetting.enemyType] = spawnSetting.spawnCount;
            }
        }
    }
}
