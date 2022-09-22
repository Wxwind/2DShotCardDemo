using Animancer;
using Sirenix.OdinInspector;
using SkillCardSystem.Bullet;
using UnityEngine;

namespace SkillCardSystem.Weapon
{
    /// <summary>
    /// 武器只是单纯的表演动画和子弹设置，具体逻辑执行都在SkillCardBase类中
    /// </summary>
    [RequireComponent(typeof(AnimancerComponent))]
    public abstract class WeaponBase : MonoBehaviour
    {
        [Title("Base子弹设置")] 
        [SerializeField] protected BulletBase bulletPre;
        [SerializeField] protected float m_shotCD = 1.0f;
        [SerializeField] protected int m_bulletCapacity;

        [Title("音频设置")] 
        [SerializeField]protected string s_shootAudioName = "Weapon_Shoot_Default";

        [Title("动画设置")] 
        [SerializeField] protected ClipTransition m_weaponAnim_Jump;
        //[SerializeField] protected ClipTransition m_weaponAnim_OnSwitchIn;
        [SerializeField] protected ClipTransition m_weaponAnim_Shot;
        [SerializeField] protected ClipTransition m_weaponAnim_Idle;
        
        [Title("运行时信息")] 
        [ReadOnly, ShowInInspector] protected int m_nowBulletCount;
        [ShowInInspector, ReadOnly] protected bool m_isCanShot = true;
        [ShowInInspector,ReadOnly] protected Timer m_shotTimer;
        [ShowInInspector,ReadOnly] protected GameObject m_bulletParent;
        [ShowInInspector, ReadOnly] protected PlayerController m_player;
        [ShowInInspector, ReadOnly] protected AnimancerComponent m_animComp;
        private bool m_isHasFirstShotRest=true;

        public int BulletCapacity => m_bulletCapacity;
        public int NowBulletCount => m_nowBulletCount;
        
        public virtual void OnInit()
        {
            m_nowBulletCount = m_bulletCapacity;
            m_shotTimer = new Timer(m_shotCD, () => { m_isCanShot = true; },true);
            m_bulletParent = GameObject.Find("BulletParentTrans");
            m_player = GameObject.Find("Player").GetComponent<PlayerController>();
            m_animComp = GetComponent<AnimancerComponent>();
            m_weaponAnim_Jump.Events.OnEnd = SwitchToIdle;
            m_weaponAnim_Shot.Events.OnEnd = SwitchToIdle;
        }

        public void SwitchToIdle()
        {
            m_animComp.Play(m_weaponAnim_Idle);
        }

        protected virtual void Update()
        {
            m_shotTimer.Tick(Time.deltaTime);
            if (!m_isHasFirstShotRest && Input.GetKeyUp(InputKeyManager.instance.attackKey))
            {
                m_isHasFirstShotRest = true;
            }
        }

        public virtual void Shot()
        {
            if (m_isCanShot&&m_isHasFirstShotRest)
            {
                if (!m_animComp.IsPlaying(m_weaponAnim_Shot.Clip))
                {
                    m_animComp.Play(m_weaponAnim_Shot).Time = 0;
                }
                m_isCanShot = false;
                m_shotTimer.ReRun();
                var go=Instantiate(bulletPre,transform.position,Quaternion.identity,m_bulletParent.transform);
                var b=go.GetComponent<BulletBase>();
                b.Set( m_player.FaceDir);
                m_nowBulletCount-=1;
                AudioManager.instance.PlaySFXAudio(s_shootAudioName);
                if (m_nowBulletCount<=0)
                {
                    SkillCardManager.instance.OnMainCardExhausted();
                }
            }
        }

        public virtual void OnPlayerJump()
        {
            if (!m_animComp.IsPlaying(m_weaponAnim_Jump.Clip))
            {
                m_animComp.Play(m_weaponAnim_Jump).Time=0;
            }
        }

        public virtual void OnDestroySelf()
        {
            Destroy(gameObject);
        }

        //若因为持续射击切换至当前卡片时，应当停止继续开火
        public virtual void OnSwitchIn()
        {
            m_animComp.Play(m_weaponAnim_Idle);
            if (Input.GetKey(InputKeyManager.instance.attackKey))
            {
                m_isHasFirstShotRest = false;
            }
            //m_animComp.Play(m_weaponAnim_OnSwitchIn);
        }

        public virtual void ReEnterCD()
        {
            m_isCanShot = false;
            m_shotTimer.ReRun();
        }
    }
}