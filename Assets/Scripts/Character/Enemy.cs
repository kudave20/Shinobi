using Shinobi.Attribute;
using System;
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
    public abstract class Enemy : MonoBehaviour
    {
        [SerializeField] private float speed = 1f;
        [SerializeField] private float damage = 10f;
        [SerializeField] private float experiencePoint = 10f;
        [SerializeField] private float attackRapidity = 1.5f;

        protected GameObject wall = null;

        protected Action onGameOver = null;

        private Health health = null;
        private float attackY = 0; // 벽을 공격하는 y좌표

        private const float OFFSET_Y = 0.25f; // 벽과의 y좌표 간격

        private float attackTimer = 0;

        public void Init(GameObject wall, Action onGameOver)
        {
            this.wall = wall;
            attackY = wall.transform.position.y + OFFSET_Y;

            health = GetComponent<Health>();
            health.Init(experiencePoint, Die);
            
            this.onGameOver = onGameOver;
        }

        private void Update()
        {
            if (CanAttack())
            {
                Attack();
            }
            else if (CanMove())
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
            wall.GetComponent<Health>().TakeDamage(damage, gameObject, (_) => onGameOver?.Invoke());
        }

        private bool CanAttack()
        {
            attackTimer += Time.deltaTime;

            if (!CanMove() && attackTimer >= attackRapidity)
            {
                attackTimer = 0;
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CanMove()
        {
            return transform.position.y > attackY;
        }

        protected abstract void Die();
    }
}
