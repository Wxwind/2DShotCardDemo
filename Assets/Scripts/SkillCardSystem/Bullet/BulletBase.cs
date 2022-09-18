using System;
using Enemy;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SkillCardSystem.Bullet
{
    public class BulletBase:MonoBehaviour
    {
        [Title("设置")] 
        [SerializeField] private int m_attack;
        [ShowInInspector, ReadOnly] private Rigidbody2D m_rbComp;
        [ShowInInspector, ReadOnly] private float m_lifeTime;
        private Timer lifeTimer;
        

        public void Awake()
        {
            m_rbComp = GetComponent<Rigidbody2D>();
            lifeTimer = new Timer(m_lifeTime, OnDestroySelf,true);
        }

        public void OnSpawn(int attack)
        {
            
        }

        private void OnDestroySelf()
        {
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var e = other.GetComponent<EnemyBase>();
            if (e != null)
            {
                e.OnHurt(m_attack);
            }
        }
    }
}