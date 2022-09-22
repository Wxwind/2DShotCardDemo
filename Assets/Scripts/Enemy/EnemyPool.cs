using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyPool<T> where T:EnemyBase
    {
        private Queue<T> pool=new Queue<T>();
        private EnemyBase m_enemyPre;
        private Transform m_father;

        public EnemyPool(int size,EnemyBase enemyPre,Transform father)
        {
            m_enemyPre = enemyPre;
            m_father = father;
            for (int i = 0; i < size; i++)
            {
                var go = GameObject.Instantiate(enemyPre.gameObject,father).GetComponent<T>();
                go.gameObject.SetActive(false);
                pool.Enqueue(go);
            }
        }

        public T GetFromPool(PlayerController player,Vector3 spawnPos,bool isBringCard)
        {
            if (pool.Count==0)
            {
                var t=pool.Dequeue();
                t.gameObject.SetActive(true);
                t.OnInit(player,spawnPos,isBringCard,() => { ReturnToPool(t);});
                return t;
            }
            else
            {
                ResizePool(5);
                var t=pool.Dequeue();
                t.gameObject.SetActive(true);
                t.OnInit(player,spawnPos,isBringCard,() => { ReturnToPool(t);});
                return t;
            }
        }

        private void ReturnToPool(T go)
        {
            go.gameObject.SetActive(false);
            pool.Enqueue(go);
        }

        private void ResizePool(int addCount)
        {
            for (int i = 0; i < addCount; i++)
            {
                var go = GameObject.Instantiate(m_enemyPre.gameObject,m_father).GetComponent<T>();
                go.gameObject.SetActive(false);
                pool.Enqueue(go);
            }
        }
    }
}