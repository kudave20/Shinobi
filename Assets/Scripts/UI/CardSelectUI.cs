using System;
using UnityEngine;

namespace Shinobi.UI
{
    public class CardSelectUI : MonoBehaviour
    {
        [SerializeField] private CardUI[] cardsUI;

        public void Init(Action onCardSelected)
        {
            foreach (var cardUI in cardsUI)
            {
                cardUI.Init(onCardSelected);
            }
        }

        public void Enable()
        {
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}
