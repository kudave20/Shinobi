using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shinobi.Character
{
    public class Slime : Enemy
    {
        [SerializeField] private Slime miniPrefab = null;

        public event Action<Slime> onMiniSpawned = null;

        protected override void Die()
        {
            base.Die();

            if (miniPrefab != null)
            {
                for (int i = 0; i < 3; i++)
                {
                    var mini = Instantiate(miniPrefab, transform.position + (i - 1) * Vector3.right * 0.5f, Quaternion.identity);
                    mini.Init(wall, onGameOver, onDie);

                    onMiniSpawned?.Invoke(mini);
                }
            }

            Destroy(gameObject);
        }
    }
}
