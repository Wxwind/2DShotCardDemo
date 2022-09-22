using System;
using Animancer;
using Enemy;
using Sirenix.OdinInspector;
using SkillCardSystem.Bullet;
using UnityEngine;

public class RevolverDiscardBullet : BulletBase
{
    [Title("动画设置")]
    [SerializeField] private ClipTransition m_baseAnim;
    private AnimancerComponent m_animComp;

    protected override void Awake()
    {
        base.Awake();
        m_animComp = GetComponent<AnimancerComponent>();
    }

    private void Start()
    {
        m_animComp.Play(m_baseAnim).Time = 0;
    }
    
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        var e = other.GetComponent<EnemyBase>();
        if (e != null)
        {
            e.OnHurt(m_attack);
            ShakeCameraManager.instance.Shake(m_rbComp.velocity);
        }
    }
}