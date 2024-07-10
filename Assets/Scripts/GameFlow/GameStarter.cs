using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Shinobi.GameFlow
{
    public class GameStarter : MonoBehaviour
    {
        [SerializeField] private float delayToStart = 3f;
        [SerializeField] private Image timerImage = null;
        [SerializeField] private TMP_Text timerText = null;
        [SerializeField] private GameManager gameManager = null;

        private void Start()
        {
            StartCoroutine(GameStartCoroutine());
        }

        private IEnumerator GameStartCoroutine()
        {
            timerImage.gameObject.SetActive(true);

            float timer = delayToStart;
            while (timer > 0)
            {
                timer -= Time.deltaTime;
                int timerInt = Mathf.CeilToInt(timer);
                timerText.text = timerInt.ToString();

                yield return null;
            }

            timerImage.gameObject.SetActive(false);

            gameManager.Init();
        }
    }
}
