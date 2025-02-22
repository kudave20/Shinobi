using Shinobi.Projectile;
using System;
using UnityEngine;

namespace Shinobi.Combat
{
    public enum WeaponStat
    {
        Damage,
        Range,
        Rapidity
    };

    public enum WeaponType
    {
        Kunai,
        IceJavelin,
        Fireball
    }

    public class Weapon : MonoBehaviour
    {
        [SerializeField] private Bullet bulletPrefab = null;

        [Header("무기 스탯")]
        [SerializeField] private float rapidity = 1f;
        [SerializeField] private float damage = 10f;
        [SerializeField] private float speed = 5f;
        [SerializeField] private float range = 10f;
        [SerializeField] private bool isPiercing = false;
        [SerializeField] private WeaponType weaponType;

        public WeaponType WeaponType => weaponType;

        private float timer = 0;
        private Bullet bullet = null;
        private GameObject shooter = null;
        private Action<float> onKill = null;

        public void Init(GameObject shooter, Action<float> onKill)
        {
            this.shooter = shooter;
            this.onKill = onKill;
        }

        /// <summary>
        /// 공격
        /// </summary>
        public void Attack()
        {
            int enemyLayer = 1 << LayerMask.NameToLayer("Enemy");
            var targets = Physics2D.CircleCastAll(transform.position, range, Vector2.zero, 0, enemyLayer);

            float minDistance = range;
            GameObject attackTarget = null;
            foreach (var target in targets)
            {
                float distance = Vector2.Distance(target.transform.position, transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    attackTarget = target.collider.gameObject;
                }
            }

            if (attackTarget != null)
            {
                bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                bullet.Init(damage, speed, isPiercing, shooter, onKill);
                bullet.Launch(attackTarget);
            }
        }

        /// <summary>
        /// 공격 가능한지 판정
        /// </summary>
        /// <returns></returns>
        public bool CanAttack()
        {
            timer += Time.deltaTime;

            if (timer >= rapidity)
            {
                timer = 0;
                return true;
            }

            return false;
        }

        public void IncreaseStat(WeaponStat statToIncrease, float increaseAmount)
        {
            switch (statToIncrease)
            {
                case WeaponStat.Damage:
                    damage *= (1 + increaseAmount);
                    break;
                case WeaponStat.Range:
                    range *= (1 + increaseAmount);
                    break;
                case WeaponStat.Rapidity:
                    rapidity *= (1 - increaseAmount);
                    break;
                default:
                    break;
            }
        }
    }
}
