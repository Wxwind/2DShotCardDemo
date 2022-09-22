using Sirenix.OdinInspector;
using SkillCardSystem.Bullet;
using UnityEngine;

namespace SkillCardSystem.Weapon
{
    public class RevolverWeapon:WeaponBase
    {
        [Title("子弹设置")]
        [SerializeField] protected RevolverDiscardBullet RevolverDiscardBulletPre;

        public void ShotDicardBullet()
        {
            var go=Instantiate(RevolverDiscardBulletPre,transform.position,Quaternion.identity,m_bulletParent.transform);
            var b=go.GetComponent<BulletBase>();
            b.Set(m_player.FaceDir);
        }
        
        public void ReLoad()
        {
            m_nowBulletCount = m_bulletCapacity;
        }
    }
}