using Shinobi.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Shinobi.UI
{
    public class ExperienceBar : MonoBehaviour
    {
        [SerializeField] private float maxWidth = 260f;
        [SerializeField] private float height = 20f;

        private Image bar = null;

        private Player player = null;

        public void Init(Player player)
        {
            this.player = player;
            player.onGainEXP += OnGainEXP;

            bar = GetComponent<Image>();
        }

        private void OnGainEXP(float experience)
        {
            var width = Mathf.Min(experience * maxWidth * 0.01f, maxWidth);

            bar.rectTransform.sizeDelta = new Vector2(width, height);
        }
    }
}
