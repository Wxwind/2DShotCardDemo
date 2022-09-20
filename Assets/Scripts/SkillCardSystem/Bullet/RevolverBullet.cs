using Enemy;
using UnityEngine;

namespace SkillCardSystem.Bullet
{
    public class RevolverBullet:BulletBase
    {
        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer==LayerMask.GetMask("Ground"))
            {
                OnDestroySelf();
                return;
            }
            var e = other.GetComponent<EnemyBase>();
            if (e != null)
            {
                e.OnHurt(m_attack);
                ShakeCameraManager.instance.Shake(m_rbComp.velocity);
                OnDestroySelf();
            }
        }
    }
}