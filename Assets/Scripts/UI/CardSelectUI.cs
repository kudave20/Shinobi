using Shinobi.Core;
using System;
using UnityEngine;

namespace Shinobi.UI
{
    public class CardSelectUI : MonoBehaviour
    {
        [SerializeField] private CardUI[] cardsUI;

        private TraitManager traitManager = null;

        public void Init(Action onCardSelected, TraitManager traitManager)
        {
            this.traitManager = traitManager;

            foreach (var cardUI in cardsUI)
            {
                cardUI.Init(onCardSelected, traitManager);
            }
        }

        public void Enable()
        {
            gameObject.SetActive(true);

            foreach (var cardUI in cardsUI)
            {
                cardUI.CardSetup();
            }
        }

        public void Disable()
        {
            gameObject.SetActive(false);

            traitManager.ClearTraitIndices();
        }
    }
}
