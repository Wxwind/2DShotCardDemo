using System;
using Sirenix.OdinInspector;
using SkillCardSystem.Bullet;
using UnityEngine;

namespace SkillCardSystem.Weapon
{
    public abstract class WeaponBase:MonoBehaviour
    {
        [Title("设置")]
        public BulletBase bulletPre;

        public abstract void Shot();

        public virtual void OnDestroySelf()
        {
            Destroy(gameObject);
        }
    }
}