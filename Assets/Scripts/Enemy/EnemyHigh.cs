
using UnityEngine;
using Utils;

namespace Enemy
{
    public class EnemyHigh : EnemyBase
    {
        [SerializeField] private Vector2 jumpSpeed;
        [SerializeField] private float jumpInternal=2f;
        private Vector2 m_cacheSpeed;
        private bool m_isInFreeze = false;
        private bool m_isCanJump = false;
        private Timer m_jumpTimer;

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
            m_jumpTimer = new Timer(jumpInternal, () =>
            {
                m_isCanJump = true;
            },true);
        }

        protected override void Update()
        {
            base.Update();
            m_jumpTimer.Tick(Time.deltaTime);
            if (m_playerController != null && !m_isInFreeze&&m_isCanJump)
            {
                int isPlayerInRight = m_playerController.transform.position.x >= transform.position.x ? 1 : -1;
                m_rbComp.velocity = new Vector2(jumpSpeed.x* isPlayerInRight,jumpSpeed.y);
                LogHelper.LogInfo("enemy jump");
                m_isCanJump = false;
                m_jumpTimer.ReRun();
            }
            else if (!m_isInFreeze&&m_isCanJump)
            {
                m_rbComp.velocity = jumpSpeed;
                LogHelper.LogInfo("enemy jump");
                m_isCanJump = false;
                m_jumpTimer.ReRun();
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
