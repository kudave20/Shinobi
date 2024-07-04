using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shinobi.Attribute
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float healthPoint = 100f;

        public void TakeDamage(float damage)
        {
            healthPoint = Mathf.Max(0, healthPoint - damage);

            if (healthPoint <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }
}
