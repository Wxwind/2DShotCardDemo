using Player;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Enemy
{
    public abstract class EnemyBase : MonoBehaviour
    {
        [Title("基础设置")]
        [SerializeField] protected int m_maxBlood;
        [SerializeField] protected int m_attack;
        [Title("运行时信息")]
        [ShowInInspector, ReadOnly] protected SpriteRenderer m_srComp;
        [ShowInInspector, ReadOnly] protected Color m_originColor;
        [ShowInInspector, ReadOnly] protected Color m_hurtColor = Color.white;
        [ShowInInspector,ReadOnly]  protected int m_nowBlood;
        private bool m_isInHurted=false;
        private Timer m_setUnHurtStateTimer;
        

        private void Awake()
        {
            m_nowBlood = m_maxBlood;
            m_srComp = GetComponent<SpriteRenderer>();
            m_originColor = m_srComp.color;
            m_setUnHurtStateTimer = new Timer(0.2f, () =>
            {
                m_isInHurted = false;
                m_srComp.color = m_originColor;
            }, false);
        }

        //受到攻击时
        public virtual void OnHurt(int damege)
        {
            m_nowBlood -= damege;
            ShowHurtAnim();
            if (m_nowBlood <= 0)
            {
                OnDeath();
            }
        }

        protected void Update()
        {
            m_setUnHurtStateTimer.Tick(Time.deltaTime);
        }

        protected virtual void ShowHurtAnim()
        {
            if (!m_isInHurted)
            {
                m_isInHurted = true;
                m_srComp.color= m_hurtColor;
                m_setUnHurtStateTimer.ReRun();
            }
        }

        protected virtual void OnDeath()
        {
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                var p = other.GetComponent<PlayerHealth>();
                p.OnHurt(m_attack);
            }
        }
    }
}