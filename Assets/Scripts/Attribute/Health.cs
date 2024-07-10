using System;
using UnityEngine;

namespace Shinobi.Attribute
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float healthPoint = 100f;

        private float experiencePoint = 0;
        private event Action onDie = null;

        public void Init(float experiencePoint, Action onDie)
        {
            this.experiencePoint = experiencePoint;
            this.onDie = onDie;
        }

        /// <summary>
        /// 데미지 받는 함수
        /// </summary>
        public void TakeDamage(float damage, GameObject instigator, Action<float> onKilled)
        {
            healthPoint = Mathf.Max(0, healthPoint - damage);

            if (healthPoint <= 0)
            {
                Die(onKilled);
            }
        }

        /// <summary>
        /// 사망
        /// </summary>
        private void Die(Action<float> onKilled)
        {
            onKilled?.Invoke(experiencePoint);

            onDie?.Invoke();
        }

        public bool IsDead()
        {
            return healthPoint <= 0;
        }
    }
}
