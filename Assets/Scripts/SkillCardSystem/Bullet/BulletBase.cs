using Enemy;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SkillCardSystem.Bullet
{
    public abstract class BulletBase:MonoBehaviour
    {
        [Title("Base基础设置")] 
        [SerializeField] protected int m_attack;
        [SerializeField] protected float m_speed;
        [SerializeField] protected float m_lifeTime;
        [ShowInInspector, ReadOnly] protected Rigidbody2D m_rbComp;
        
        private Timer m_lifeTimer;
        

        protected virtual void Awake()
        {
            m_rbComp = GetComponent<Rigidbody2D>();
            m_lifeTimer = new Timer(m_lifeTime, OnDestroySelf,true);
        }

        protected virtual void Update()
        {
           m_lifeTimer.Tick(Time.deltaTime);
        }

        public virtual void Set(Vector2 direction)
        {
            m_rbComp.velocity = direction * m_speed;
        }

        protected virtual void OnDestroySelf()
        {
            Destroy(gameObject);
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            var e = other.GetComponent<EnemyBase>();
            if (e != null)
            {
                e.OnHurt(m_attack);
                ShakeCameraManager.instance.Shake(m_rbComp.velocity);
                OnDestroySelf();
            }
        }
    }
}