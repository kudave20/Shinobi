using Shinobi.Combat;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Shinobi.Character
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float[] levelUpPoints;

        [Header("테스트")]
        [SerializeField] private List<Weapon> weaponPrefabs = null;

        private List<Weapon> weapons = new List<Weapon>();

        private float experience = 0;
        private int level = 0;

        public event Action<float> onGainEXP = null;
        public event Action onLevelUp = null;

        public void Init()
        {
            foreach (var weaponPrefab in weaponPrefabs)
            {
                Weapon weapon = Instantiate(weaponPrefab, transform);
                weapon.Init(gameObject, OnKill);
                AddWeapon(weapon);
            }
        }

        public void AddWeapon(Weapon weapon)
        {
            weapons.Add(weapon);
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
