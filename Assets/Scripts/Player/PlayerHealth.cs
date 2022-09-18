using System;
using UnityEngine;

namespace Player
{
    public class PlayerHealth:MonoBehaviour
    {
        [SerializeField]private int m_maxBlood;
        private int m_nowBlood;

        private void Awake()
        {
            m_nowBlood = m_maxBlood;
        }

        public virtual void OnHurt(int damege)
        {
            m_nowBlood -= damege;
            if (m_nowBlood<0)
            {
                OnDeath();
            }
        }
        
        private void OnDeath()
        {
            Debug.Log("Player death");
        }
    }
}