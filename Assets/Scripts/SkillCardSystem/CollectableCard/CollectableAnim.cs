using System;
using UnityEngine;

public class CollectableAnim : MonoBehaviour
{
    [SerializeField]private float m_moveSpeed;
    [SerializeField] private float m_upLocalPos=1;
    [SerializeField] private float m_downLocalPos=0;
    private float originPosY;
    private bool m_isMovingUp;

    private void Awake()
    {
        originPosY = transform.position.y;
    }

    void FixedUpdate()
    {
        if(m_isMovingUp)
        {
            transform.Translate(Vector3.up * m_moveSpeed * Time.fixedDeltaTime);
            if (transform.localPosition.y >= m_upLocalPos+originPosY)
            {
                m_isMovingUp = false; 
            }
        }
        else
        {
            transform.Translate(Vector3.down * m_moveSpeed * Time.fixedDeltaTime);
            if (transform.localPosition.y <= m_downLocalPos+originPosY)
            {
                m_isMovingUp = true;
            }
        }
    }
    
}
