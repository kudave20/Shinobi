using Shinobi.Combat;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Shinobi.Character
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float[] levelUpPoints;

        [Header("테스트")]
        [SerializeField] private List<Weapon> weaponPrefabs = null;

        private List<Weapon> weapons = new List<Weapon>();

        public Dictionary<WeaponType, Weapon> WeaponDic => weapons.ToDictionary(x => x.WeaponType);

        private float experience = 0;
        private int level = 0;

        public event Action<float> onGainEXP = null;
        public event Action onLevelUp = null;

        public void Init()
        {
            foreach (var weaponPrefab in weaponPrefabs)
            {
                AddWeapon(weaponPrefab);
            }
        }

        /// <summary>
        /// 무기 장착
        /// </summary>
        public void AddWeapon(Weapon weaponPrefab)
        {
            var weapon = Instantiate(weaponPrefab, transform);
            weapons.Add(weapon);
            weapon.Init(gameObject, OnKill);
            weapon.transform.position = transform.position;
        }

        /// <summary>
        /// 무기 장착되어 있는지 확인
        /// </summary>
        public bool IsWeaponEquipped(WeaponType weaponType)
        {
            return weapons.Select(x => x.WeaponType).Contains(weaponType);
        }

        private void Update()
        {
            foreach (var weapon in weapons)
            {
                if (weapon.CanAttack())
                {
                    weapon.Attack();
                }
            }
        }

        /// <summary>
        /// 적 사살 시 호출
        /// </summary>
        private void OnKill(float experiencePoint)
        {
            IncreaseExperience(experiencePoint);
        }

        private void IncreaseExperience(float experiencePoint)
        {
            experience += experiencePoint;

            if (experience > levelUpPoints[level])
            {
                ++level;
                experience = 0;
                onLevelUp?.Invoke();
            }

            onGainEXP?.Invoke(experience);
        }
    }
}
