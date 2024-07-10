using Shinobi.Character;
using Shinobi.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shinobi.Trait
{
    public class CardTrait : MonoBehaviour
    {
        [SerializeField] private TraitConfig traitConfig = null;

        public TraitConfig TraitConfig => traitConfig;

        private Player player = null;

        public void Init(Player player)
        {
            this.player = player;
        }

        public void Activate()
        {
            if (!player.IsWeaponEquipped(traitConfig.WeaponInCard.WeaponType))
            {
                player.AddWeapon(traitConfig.WeaponInCard);
            }

            var weapon = player.WeaponDic[traitConfig.WeaponInCard.WeaponType];
            weapon.IncreaseStat(traitConfig.StatToIncrease, traitConfig.IncreaseAmount);
        }
    }
}
