using Sirenix.OdinInspector;
using SkillCardSystem.Bullet;
using UnityEngine;

namespace SkillCardSystem.Weapon
{
    public abstract class WeaponBase : MonoBehaviour
    {
        [Title("Base子弹设置")] 
        public BulletBase bulletPre;
        [SerializeField] protected float m_shotCD = 1.0f;
        [SerializeField] protected float m_bulletSpeed;
        [SerializeField] protected int m_bulletAttack;
        [SerializeField] protected int m_bulletCapacity;

        [Title("运行时信息")] 
        [ReadOnly, ShowInInspector] protected int m_nowBulletCount;
        [ShowInInspector, ReadOnly] protected bool m_isCanShot = true;
        [ShowInInspector,ReadOnly] protected Timer m_shotTimer;
        [ShowInInspector,ReadOnly] protected GameObject m_bulletParent;
        [ShowInInspector, ReadOnly] protected PlayerController m_player;
        
        [Title("音频设置")] 
        [SerializeField]protected string s_shootAudioName = "Weapon_Shoot_Default";

        public int BulletCapacity => m_bulletCapacity;
        public int NowBulletCount => m_nowBulletCount;
        public virtual void OnInit()
        {
            Debug.Log("WeaponBase Awake");
            m_nowBulletCount = m_bulletCapacity;
            m_shotTimer = new Timer(m_shotCD, () => { m_isCanShot = true; },true);
            m_bulletParent = GameObject.Find("BulletParentTrans");
            m_player = GameObject.Find("Player").GetComponent<PlayerController>();
        }
        
        protected virtual void Update()
        {
            m_shotTimer.Tick(Time.deltaTime);
        }

        public virtual void Shot()
        {
            
        }

        public virtual void OnDestroySelf()
        {
            Destroy(gameObject);
        }

        public virtual void ReEnterCD()
        {
            m_isCanShot = false;
            m_shotTimer.ReRun();
        }
    }
}