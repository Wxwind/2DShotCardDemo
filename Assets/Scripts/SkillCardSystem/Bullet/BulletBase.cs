using System;
using Enemy;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SkillCardSystem.Bullet
{
    public abstract class BulletBase:MonoBehaviour
    {
        [Title("设置")] 
        [SerializeField] protected int m_attack;
        [SerializeField] protected float m_speed;
        [SerializeField] protected float m_lifeTime;
        [ShowInInspector, ReadOnly] protected Rigidbody2D m_rbComp;
        
        private Timer m_lifeTimer;
        

        protected void Awake()
        {
            m_rbComp = GetComponent<Rigidbody2D>();
            m_lifeTimer = new Timer(m_lifeTime, OnDestroySelf,true);
        }

        protected void Update()
        {
           m_lifeTimer.Tick(Time.deltaTime);
        }

        public abstract void Set(int attack,float speed,Vector2 direction);

        protected virtual void OnDestroySelf()
        {
            Destroy(gameObject);
        }

        protected void OnTriggerEnter2D(Collider2D other)
        {
            var e = other.GetComponent<EnemyBase>();
            if (e != null)
            {
                e.OnHurt(m_attack);
                OnDestroySelf();
            }
        }
    }
}