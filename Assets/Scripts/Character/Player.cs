using Shinobi.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shinobi.Character
{
    public class Player : MonoBehaviour
    {
        [Header("테스트")]
        [SerializeField] private List<Weapon> weaponPrefabs = null;

        private List<Weapon> weapons = new List<Weapon>();

        public void Init()
        {
            foreach (var weaponPrefab in weaponPrefabs)
            {
                Weapon weapon = Instantiate(weaponPrefab, transform);
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
    }
}
