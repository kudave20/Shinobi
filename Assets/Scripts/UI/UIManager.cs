using Shinobi.Character;
using Shinobi.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shinobi.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private ExperienceBar experienceBar = null;
        [SerializeField] private CardSelectUI cardSelectUI = null;
        [SerializeField] private SpeedUpUI speedUpUI = null;
        [SerializeField] private GameObject clearUI = null;
        [SerializeField] private GameObject overUI = null;

        public ExperienceBar ExperienceBar => experienceBar;
        public CardSelectUI CardSelectUI => cardSelectUI;
        public SpeedUpUI SpeedUpUI => speedUpUI;
        public GameObject ClearUI => clearUI;
        public GameObject OverUI => overUI;

        public void Init(Player player, Action onCardSelected, TraitManager traitManager)
        {
            experienceBar.Init(player);
            cardSelectUI.Init(onCardSelected, traitManager);
            speedUpUI.Init();
        }
    }
}
