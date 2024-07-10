using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shinobi.Character
{
    public class Slime : Enemy
    {
        [SerializeField] private Slime miniPrefab = null;

        protected override void Die()
        {
            if (miniPrefab != null)
            {
                for (int i = 0; i < 3; i++)
                {
                    var mini = Instantiate(miniPrefab, transform.position + (i - 1) * Vector3.right, Quaternion.identity);
                    mini.Init(wall, onGameOver);
                }
            }

            Destroy(gameObject);
        }
    }
}
