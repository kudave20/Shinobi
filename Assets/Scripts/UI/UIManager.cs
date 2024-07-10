using Shinobi.Character;
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

        public ExperienceBar ExperienceBar => experienceBar;
        public CardSelectUI CardSelectUI => cardSelectUI;
        public SpeedUpUI SpeedUpUI => speedUpUI;

        public void Init(Player player, Action onCardSelected)
        {
            experienceBar.Init(player);
            cardSelectUI.Init(onCardSelected);
            speedUpUI.Init();
        }
    }
}
