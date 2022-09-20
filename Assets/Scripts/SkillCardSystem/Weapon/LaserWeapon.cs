using System;
using Sirenix.OdinInspector;
using SkillCardSystem.Bullet;
using UnityEngine;

namespace SkillCardSystem.Weapon
{
    public class LaserWeapon:WeaponBase
    {
        public override void Shot()
        {
            if (m_isCanShot)
            {
                m_isCanShot = false;
                m_shotTimer.ReRun();
                var go=Instantiate(bulletPre,transform.position,Quaternion.identity,m_bulletParent.transform);
                var b=go.GetComponent<BulletBase>();
                b.Set(m_bulletAttack,m_bulletSpeed, m_player.FaceDir);
                m_nowBulletCount-=1;
                AudioManager.instance.PlaySFXAudio(s_shootAudioName);
                if (m_nowBulletCount<=0)
                {
                    SkillCardManager.instance.OnMainCardExhausted();
                }
            }
        }

        public void Set(float bulletSpeed)
        {
            this.m_bulletSpeed = bulletSpeed;
        }

        public void ReLoad()
        {
            m_nowBulletCount = m_bulletCapacity;
        }
    }
}