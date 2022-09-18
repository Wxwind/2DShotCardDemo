using System;
using Sirenix.OdinInspector;
using SkillCardSystem;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [Title("基础参数")]
    [SerializeField] private float LRMoveSpeed;
    [SerializeField] private float LRAirMoveSpeed;
    [SerializeField] private float jumpSpeed;
    [ShowInInspector,ReadOnly] private int xInput = 0;
    [ShowInInspector,ReadOnly] private int faceDir = 1;

    [Title("角色状态")]
    [ShowInInspector, ReadOnly] private bool m_isCanMove=true;
    [ShowInInspector, ReadOnly] private bool m_isCanJump=true;
    [ShowInInspector, ReadOnly] private bool m_isCanActivateCard=true;
    [ShowInInspector, ReadOnly] private bool m_isCanDiscordCard=true;
    [ShowInInspector, ReadOnly] private bool m_isCanSwitchCard=true;

    //[Title("卡牌能力")]

    private Rigidbody2D m_rb;
    private CollDetection m_collDect;

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_collDect = GetComponent<CollDetection>();
    }

    private void Update()
    {
        xInput = 0;
        if (Input.GetKey(InputKeyManager.instance.leftKey))
        {
            xInput -= 1;
        }

        if (Input.GetKey(InputKeyManager.instance.rightKey))
        {
            xInput += 1;
        }

        if (xInput != 0)
        {
            if (Input.GetKey(InputKeyManager.instance.leftKey))
            {
                faceDir = -1;
            }

            if (Input.GetKey(InputKeyManager.instance.rightKey))
            {
                faceDir = 1;
            }
        }

        LRMove();
        Jump();
        ActivateCard();
        DiscordCard();
        SwitchSkillCard();
    }

    private void LRMove()
    {
        if (!m_isCanMove) return;

        if (m_collDect.OnGround) //地面移动
        {
            if (xInput == 0)
            {
                m_rb.velocity = new Vector2(0, m_rb.velocity.y);
            }
            else
            {
                m_rb.velocity = new Vector2(xInput * LRMoveSpeed, m_rb.velocity.y);
            }
        }
        else //空中左右移动
        {
            m_rb.velocity = new Vector2(xInput * LRAirMoveSpeed, m_rb.velocity.y);
        }
    }

    private void Jump()
    {
        if (!m_isCanJump) return;
        if (Input.GetKey(KeyCode.J) && m_collDect.OnGround)
        {
            m_rb.velocity = new Vector2(m_rb.velocity.x, jumpSpeed);
        }
    }

    private void ActivateCard()
    {
        if (!m_isCanActivateCard) return;
        if (Input.GetKey(KeyCode.K))
        {
            SkillCardManager.instance.ActivateCard();
        }
    }

    private void DiscordCard()
    {
        if (!m_isCanDiscordCard) return;
        if (Input.GetKeyDown(KeyCode.L))
        {
            SkillCardManager.instance.DiscordCard();
        }
    }

    private void SwitchSkillCard()
    {
        if (!m_isCanSwitchCard) return;
        if (Input.GetKeyDown(KeyCode.E))
        {
            SkillCardManager.instance.SwitchSkillCard();
        }
    }
}