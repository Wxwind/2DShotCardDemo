using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableAnim : MonoBehaviour
{
    [SerializeField]private float m_moveSpeed;
    [SerializeField] private float m_upLocalPos=1;
    [SerializeField] private float m_downLocalPos=0;
    private bool m_isMovingUp;
    
    void FixedUpdate()
    {
        if(m_isMovingUp)
        {
            transform.Translate(Vector3.up * m_moveSpeed * Time.fixedDeltaTime);
            if (transform.localPosition.y >= m_upLocalPos)
            {
                m_isMovingUp = false;
            }
        }
        else
        {
            transform.Translate(Vector3.down * m_moveSpeed * Time.fixedDeltaTime);
            if (transform.localPosition.y <= m_downLocalPos)
            {
                m_isMovingUp = true;
            }
        }
    }
    
}
