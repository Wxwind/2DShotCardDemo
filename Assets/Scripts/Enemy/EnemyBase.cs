using System;
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
        [SerializeField] protected float m_freezeTime=0.2f;
        [Title("运行时信息")]
        [ShowInInspector, ReadOnly] protected Rigidbody2D m_rbComp;
        [ShowInInspector, ReadOnly] protected SpriteRenderer m_srComp;
        [ShowInInspector, ReadOnly] protected Color m_originColor;
        [ShowInInspector, ReadOnly] protected Color m_hurtColor = Color.white;
        [ShowInInspector, ReadOnly] protected int m_nowBlood;
        [ShowInInspector, ReadOnly] protected PlayerController m_playerController;
        protected bool m_isInHurted=false;
        protected Timer m_setUnHurtStateTimer;

        public void OnInit(PlayerController player)
        {
            m_playerController = player;
        }


        protected virtual void Awake()
        {
            m_nowBlood = m_maxBlood;
            m_srComp = GetComponent<SpriteRenderer>();
            m_rbComp = GetComponent<Rigidbody2D>();
            m_originColor = m_srComp.color;
            m_setUnHurtStateTimer = new Timer(m_freezeTime, () =>
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

        protected virtual void Update()
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