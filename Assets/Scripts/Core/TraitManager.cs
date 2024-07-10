using Shinobi.Character;
using Shinobi.Trait;
using System.Collections.Generic;
using UnityEngine;

namespace Shinobi.Core
{
    public class TraitManager : MonoBehaviour
    {
        [SerializeField] private CardTrait[] cardTraits;

        private List<float> traitIndices = new List<float>();

        public void Init(Player player)
        {
            foreach (var cardTrait in cardTraits)
            {
                cardTrait.Init(player);
            }
        }

        public CardTrait GetRandomTrait()
        {
            int randomIndex = Random.Range(0, cardTraits.Length);

            while (traitIndices.Contains(randomIndex))
            {
                randomIndex = Random.Range(0, cardTraits.Length);
            }

            traitIndices.Add(randomIndex);

            return cardTraits[randomIndex];
        }

        public void ClearTraitIndices()
        {
            traitIndices.Clear();
        }
    }
}
