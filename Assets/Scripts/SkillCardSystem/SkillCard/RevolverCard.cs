using Sirenix.OdinInspector;
using UnityEngine;

namespace SkillCardSystem.SkillCard
{
    public class RevolverCard : SkillCardBase
    {
        [Title("设置")] [SerializeField] private float m_shotCD = 1.0f;

        [Title("运行时信息")] [ShowInInspector, ReadOnly]
        private bool m_isCanShot = true;

        [ShowInInspector, ReadOnly] private GameObject m_player;
        [ShowInInspector, ReadOnly] private GameObject m_weapon;
        [ShowInInspector, ReadOnly] private int m_bulletCount;

        private Timer m_shotTimer;


        private void Awake()
        {
            m_player = GameObject.Find("Player");
            m_shotTimer = new Timer(m_shotCD, () => { m_isCanShot = true; },true);
            m_weapon = Instantiate(WeaponPre.gameObject, m_player.transform);
            m_bulletCount = 0;
        }

        private void Update()
        {
            m_shotTimer.Tick(Time.deltaTime);
        }

        public override void OnInit()
        {
            
        }

        public override void OnDestroySelf()
        {
            
        }

        public override void OnActivate()
        {
            if (m_isCanShot)
            {
                m_shotTimer.ReRun();
            }
        }

        public override void OnDiscord()
        {
            throw new System.NotImplementedException();
        }

        public override void OnSwitch()
        {
            throw new System.NotImplementedException();
        }

        public override void OnEquip()
        {
            throw new System.NotImplementedException();
        }
    }
}