using Shinobi.Attribute;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shinobi.Character
{
    public enum EnemyType
    {
        Rat,
        Slime
    }

    [RequireComponent(typeof(Health))]
    public class Enemy : MonoBehaviour
    {
        [SerializeField] protected float speed = 1f;
        [SerializeField] protected float damage = 10f;

        private GameObject wall = null;
        private float attackY = 0; // 벽을 공격하는 y좌표

        private const float OFFSET_Y = 0.25f; // 벽과의 y좌표 간격

        public void Init(GameObject wall)
        {
            this.wall = wall;
            attackY = wall.transform.position.y + OFFSET_Y;
        }

        private void Update()
        {
            if (CanAttack())
            {
                Attack();
            }
            else
            {
                Move();
            }
        }

        private void Move()
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);
        }

        private void Attack()
        {
            wall.GetComponent<Health>().TakeDamage(damage);
        }

        private bool CanAttack()
        {
            if (transform.position.y <= attackY)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
