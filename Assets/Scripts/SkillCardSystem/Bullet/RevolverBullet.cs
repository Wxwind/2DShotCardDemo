using UnityEngine;

namespace SkillCardSystem.Bullet
{
    public class RevolverBullet:BulletBase
    {
        public override void Set(int attack, float speed, Vector2 direction)
        {
            m_attack = attack;
            m_speed = speed;
            m_rbComp.velocity = direction * m_speed;
        }
    }
}