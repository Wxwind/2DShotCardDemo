using System;
using Sirenix.OdinInspector;
using SkillCardSystem.Bullet;
using UnityEngine;

namespace SkillCardSystem.Weapon
{
    public class RevolverWeapon:WeaponBase
    {
        [Title("子弹设置")]
        [SerializeField]private float m_shotCD = 1.0f;
        [SerializeField]private float m_bulletSpeed;
        [SerializeField]private int m_bulletAttack;
        [SerializeField]private int m_bulletCapacity;
        
        [Title("运行时信息")]
        [ShowInInspector, ReadOnly] private bool m_isCanShot = true;
        [ReadOnly,ShowInInspector] private int m_nowBulletCount;
        [ShowInInspector,ReadOnly] private Timer m_shotTimer;
        [ShowInInspector,ReadOnly] private GameObject m_bulletParent;
        [ShowInInspector, ReadOnly] private PlayerController m_player;

        private void Awake()
        {
            m_nowBulletCount = m_bulletCapacity;
            m_shotTimer = new Timer(m_shotCD, () => { m_isCanShot = true; },true);
            m_bulletParent = GameObject.Find("BulletParentTrans");
            m_player = GameObject.Find("Player").GetComponent<PlayerController>();
           
        }
        
        private void Update()
        {
            m_shotTimer.Tick(Time.deltaTime);
        }

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