using Shinobi.Attribute;
using System.Collections;
using UnityEngine;

namespace Shinobi.Projectile
{
    public abstract class Bullet : MonoBehaviour
    {
        private float damage = 0;
        private float speed = 0;
        private bool isPiercing = false;

        private const float RADIUS = 0.1f;

        public void Init(float damage, float speed, bool isPiercing)
        {
            this.damage = damage;
            this.speed = speed;
            this.isPiercing = isPiercing;
        }

        public virtual void Launch(GameObject target)
        {
            Vector2 direction = (target.transform.position - transform.position).normalized;

            StartCoroutine(TranslateBullet(direction));
        }

        private IEnumerator TranslateBullet(Vector2 direction)
        {
            int enemyLayer = 1 << LayerMask.NameToLayer("Enemy");

            while (IsInScreen())
            {
                transform.Translate(direction * speed * Time.deltaTime);

                var target = Physics2D.CircleCast(transform.position, RADIUS, Vector2.zero, 0, enemyLayer);

                var health = target.collider?.GetComponent<Health>();
                if (health != null)
                {
                    health.TakeDamage(damage);

                    if (!isPiercing)
                    {
                        break;
                    }
                }

                yield return null;
            }

            Destroy(gameObject);
        }

        private bool IsInScreen()
        {
            Vector2 screenPoint = Camera.main.WorldToScreenPoint(transform.position);

            return screenPoint.x >= 0 && screenPoint.x <= Screen.width && screenPoint.y >= 0 && screenPoint.y <= Screen.height;
        }
    }
}
