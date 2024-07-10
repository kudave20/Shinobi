using Shinobi.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shinobi.Trait
{
    [CreateAssetMenu(fileName = "Trait", menuName = "Traits/New Trait", order = 0)]
    public class TraitConfig : ScriptableObject
    {
        [SerializeField] private Weapon weaponInCard = null;
        [SerializeField] private WeaponStat statToIncrease;
        [SerializeField] private float increaseAmount = 0;
        [SerializeField] private Color weaponColor;
        [SerializeField] private string statText;

        public Weapon WeaponInCard => weaponInCard;
        public WeaponStat StatToIncrease => statToIncrease;
        public float IncreaseAmount => increaseAmount;
        public Color WeaponColor => weaponColor;
        public string StatText => statText;
    }
}
