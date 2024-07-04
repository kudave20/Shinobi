using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Shinobi.UI
{
    public class CardUI : MonoBehaviour
    {
        [SerializeField] private Button clickPanel;

        public void Init(Action onCardSelected)
        {
            clickPanel.onClick.AddListener(() => onCardSelected?.Invoke());
        }
    }
}
