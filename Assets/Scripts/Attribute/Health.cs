using System;
using UnityEngine;

namespace Shinobi.Attribute
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float healthPoint = 100f;

        private float experiencePoint = 0;

        public void Init(float experiencePoint)
        {
            this.experiencePoint = experiencePoint;
        }

        public void TakeDamage(float damage, GameObject instigator, Action<float> onDie)
        {
            healthPoint = Mathf.Max(0, healthPoint - damage);

            if (healthPoint <= 0)
            {
                Die(onDie);
            }
        }

        private void Die(Action<float> onDie)
        {
            onDie?.Invoke(experiencePoint);

            Destroy(gameObject);
        }

        public bool IsDead()
        {
            return healthPoint <= 0;
        }
    }
}
