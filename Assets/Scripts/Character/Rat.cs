using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shinobi.Character
{
    public class Rat : Enemy
    {
        protected override void Die()
        {
            base.Die();

            Destroy(gameObject);
        }
    }
}
