using System;
using Player;
using UnityEngine;

namespace Enemy
{
    public abstract class EnemyBase:MonoBehaviour
    {
        [SerializeField]protected int m_maxBlood;
        [SerializeField]protected int m_attack;
        protected int m_nowBlood;

        private void Awake()
        {
            m_nowBlood = m_maxBlood;
        }

        //受到攻击时
        public virtual void OnHurt(int damege)
        {
            m_nowBlood -= damege;
            if (m_nowBlood<=0)
            {
                OnDeath();
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
                var p=other.GetComponent<PlayerHealth>();
                p.OnHurt(m_attack);
            }
        }
    }
}