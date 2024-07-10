using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Shinobi.UI
{
    public class SpeedUpUI : MonoBehaviour
    {
        [SerializeField] private float speedUpMultiplier = 2f;

        private Button speedUpButton = null;

        public void Init()
        {
            speedUpButton = GetComponent<Button>();
            speedUpButton.onClick.AddListener(OnSpeedUp);
        }

        private void OnSpeedUp()
        {
            if (Time.timeScale == 0) return;

            Time.timeScale *= speedUpMultiplier;

            speedUpMultiplier = 1f / speedUpMultiplier;
        }
    }
}
