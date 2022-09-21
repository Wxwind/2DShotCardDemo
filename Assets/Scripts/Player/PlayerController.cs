using Cinemachine;
using Sirenix.OdinInspector;
using SkillCardSystem;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Title("基础参数")]
    [SerializeField] private float m_lrMoveSpeed;
    [SerializeField] private float m_lrAirMoveSpeed;
    [SerializeField] private float m_jumpSpeed;
    [SerializeField] private float m_wallSlideSpeed;
    
    [Title("相机设置")]
    [SerializeField] private Transform m_leftFocusTrans;
    [SerializeField] private Transform m_rightFocusTrans;
    [SerializeField] private CinemachineVirtualCamera m_vCamera;
    
    [Title("运行时信息")]
    [ShowInInspector, ReadOnly] private int xInput = 0;
    [ShowInInspector, ReadOnly] private Vector2Int faceDir = new Vector2Int(1, 0);
    
    public Vector2Int FaceDir => faceDir;

    [Title("角色状态")]
    [ShowInInspector, ReadOnly] private bool m_isCanMove = true;
    [ShowInInspector, ReadOnly] private bool m_isCanJump = true;
    [ShowInInspector, ReadOnly] private bool m_isCanActivateCard = true;
    [ShowInInspector, ReadOnly] private bool m_isCanDiscordCard = true;
    [ShowInInspector, ReadOnly] private bool m_isCanSwitchCard = true;
    public CinemachineVirtualCamera vCamera;

    //[Title("卡牌能力")]

    private Rigidbody2D m_rbComp;
    private CollDetection m_collDectComp;

    private void Awake()
    {
        m_rbComp = GetComponent<Rigidbody2D>();
        m_collDectComp = GetComponent<CollDetection>();
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
                faceDir.x = -1;
            }

            if (Input.GetKey(InputKeyManager.instance.rightKey))
            {
                faceDir.x = 1;
            }
        }

        var rotateY = faceDir.x == 1 ? 0 : 180;
        transform.rotation=Quaternion.Euler(0,rotateY,0);
        FoucusCamera();
        LRMove();
        Jump();
        ActivateCard();
        DiscardCard();
        SwitchSkillCard();
        WallSlide();
    }

    private void FoucusCamera()
    {
        switch (xInput)
        {
            case 0:
                m_vCamera.Follow = transform;
                break;
            case -1:
                m_vCamera.Follow = m_leftFocusTrans;
                break;
            case 1:
                m_vCamera.Follow = m_rightFocusTrans;
                break;
        }
    }
    
    private void LRMove()
    {
        if (!m_isCanMove) return;

        if (m_collDectComp.OnGround) //地面移动
        {
            if (xInput == 0)
            {
                m_rbComp.velocity = new Vector2(0, m_rbComp.velocity.y);
            }
            else
            {
                m_rbComp.velocity = new Vector2(xInput * m_lrMoveSpeed, m_rbComp.velocity.y);
            }
        }
        else //空中左右移动
        {
            m_rbComp.velocity = new Vector2(xInput * m_lrAirMoveSpeed, m_rbComp.velocity.y);
        }
    }

    private void Jump()
    {
        if (!m_isCanJump) return;
        if (Input.GetKeyDown(InputKeyManager.instance.jumpKey) && m_collDectComp.OnGround)
        {
            m_rbComp.velocity = new Vector2(m_rbComp.velocity.x, m_jumpSpeed);
            if (SkillCardManager.instance.NowWeapon!=null)
            {
                SkillCardManager.instance.NowWeapon.OnPlayerJump();
            }
            
            AudioManager.instance.PlaySFXAudio("Player_Jump");
        }
    }

    private void ActivateCard()
    {
        if (!m_isCanActivateCard) return;
        if (Input.GetKey(InputKeyManager.instance.attackKey))
        {
            SkillCardManager.instance.ActivateCard();
        }
    }

    private void DiscardCard()
    {
        if (!m_isCanDiscordCard) return;
        if (Input.GetKeyDown(InputKeyManager.instance.discardKey))
        {
            SkillCardManager.instance.DiscardCard();
        }
    }

    private void SwitchSkillCard()
    {
        if (!m_isCanSwitchCard) return;
        if (Input.GetKeyDown(InputKeyManager.instance.interactKey))
        {
            SkillCardManager.instance.SwitchSkillCard();
        }
    }

    private void WallSlide()
    {
        if (m_collDectComp.OnGround)
        {
            ResetAllAbility(true);
            return;
        }
        if ((m_collDectComp.OnLeftWall&&Input.GetKey(InputKeyManager.instance.leftKey))||
            m_collDectComp.OnRightWall&&Input.GetKey(InputKeyManager.instance.rightKey))
        {
            ResetAllAbility(false);
            //防止靠墙跳跃的时候瞬间落下
            float vY = m_rbComp.velocity.y;
            if (vY<=0)
            {
                m_rbComp.velocity = new Vector2(m_rbComp.velocity.x,m_wallSlideSpeed);
            }
        }
    }

    private void ResetAllAbility(bool open)
    {
        m_isCanJump = open;
        m_isCanActivateCard = open;
        m_isCanDiscordCard = open;
        m_isCanSwitchCard = open;
    }
}