using Sirenix.OdinInspector;
using SkillCardSystem.Weapon;
using UnityEngine;

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

        public WeaponBase Weapon => m_weapon;

        public abstract void OnInit();
        public abstract void OnDestroySelf();
        public abstract void OnActivate();
        public abstract void OnDiscord();
        public abstract void OnSwitchOut();
        public abstract void OnSwitchIn();
        public abstract void OnEnterMainCardSlot();
        public abstract void OnEnterSpareCardSlot();
    }
}