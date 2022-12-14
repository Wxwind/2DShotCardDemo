using Sirenix.OdinInspector;
using SkillCardSystem.Weapon;
using UnityEngine;
using Utils;

namespace SkillCardSystem.SkillCard
{
    /// <summary>
    /// 存储了卡片的ui以及卡片效果等信息
    /// </summary>
    public abstract class SkillCardBase:MonoBehaviour,ICard
    {
        [Title("Base基础设置")]
        public string Id;
        public string Name;
        public string Summary;
        public Sprite BackGround;
        public int BulletCapacity;
        public WeaponBase WeaponPre;
        [Title("运行时信息")]
        [ShowInInspector, ReadOnly] protected WeaponBase m_weapon;
        [ShowInInspector, ReadOnly] protected Transform m_weaponSlot;

        public WeaponBase Weapon => m_weapon;

        public virtual void OnInit()
        {
            m_weaponSlot = GameObject.Find("WeaponSlot").transform;
            if (m_weaponSlot==null)
            {
                LogHelper.LogError("WeaponSlot is null");
            }
            m_weapon = Instantiate(WeaponPre.gameObject, m_weaponSlot).GetComponent<WeaponBase>();
            m_weapon.OnInit();
        }
        public abstract void OnDestroySelf();
        public abstract void OnActivate();
        public abstract void OnDiscord();
        public abstract void OnSwitchOut();
        public abstract void OnSwitchIn();

        public virtual void OnEnterMainCardSlot()
        {
            m_weapon.gameObject.SetActive(true);
            m_weapon.SwitchToIdle();
        }
        public abstract void OnEnterSpareCardSlot();
    }
}