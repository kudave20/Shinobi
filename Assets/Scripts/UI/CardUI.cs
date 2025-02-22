using Shinobi.Core;
using Shinobi.Trait;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Shinobi.UI
{
    public class CardUI : MonoBehaviour
    {
        [SerializeField] private Button clickPanel = null;
        [SerializeField] private Image weaponImage = null;
        [SerializeField] private TMP_Text statText = null;

        private CardTrait cardTrait = null;
        private TraitManager traitManager = null;

        public void Init(Action onCardSelected, TraitManager traitManager)
        {
            this.traitManager = traitManager;

            clickPanel.onClick.AddListener(() => onCardSelected?.Invoke());
            clickPanel.onClick.AddListener(OnClicked);
        }

        public void CardSetup()
        {
            cardTrait = traitManager.GetRandomTrait();

            weaponImage.color = cardTrait.TraitConfig.WeaponColor;
            statText.text = cardTrait.TraitConfig.StatText;
        }
        
        private void OnClicked()
        {
            cardTrait.Activate();
        }
    }
}
