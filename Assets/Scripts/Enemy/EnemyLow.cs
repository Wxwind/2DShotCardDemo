using UnityEngine;

namespace Enemy
{
    public class EnemyLow : EnemyBase
    {
        [SerializeField] private float fixedSpeed = 2;
        private Vector2 m_cacheSpeed;
        private bool m_isInFreeze = false;

        protected override void Awake()
        {
            base.Awake();
            m_setUnHurtStateTimer.ResetTimerAndRun(m_freezeTime, () =>
            {
                m_isInHurted = false;
                m_srComp.color = m_originColor;
                m_rbComp.velocity = m_cacheSpeed;
                m_isInFreeze = false;
            });
        }

        protected override void Update()
        {
            base.Update();
            if (m_playerController != null&&!m_isInFreeze)
            {
                int isPlayerInRight = m_playerController.transform.position.x >= transform.position.x ? 1 : -1;
                m_rbComp.velocity = new Vector2(fixedSpeed * isPlayerInRight, m_rbComp.velocity.y);
            }
            else if (!m_isInFreeze)
            {
                m_rbComp.velocity = new Vector2(fixedSpeed, m_rbComp.velocity.y);
            }
        }

        protected override void ShowHurtAnim()
        {
            if (!m_isInHurted)
            {
                m_isInFreeze = true;
                m_cacheSpeed = m_rbComp.velocity;
                m_rbComp.velocity = Vector2.zero;
                m_isInHurted = true;
                m_srComp.color = m_hurtColor;
                m_setUnHurtStateTimer.ReRun();
            }
        }
    }
}