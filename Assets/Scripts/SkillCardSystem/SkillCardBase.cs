using Sirenix.OdinInspector;
using SkillCardSystem.Weapon;
using TMPro;
using UnityEngine;

namespace SkillCardSystem
{
    /// <summary>
    /// 存储了卡片的ui以及卡片效果等信息
    /// </summary>
    public abstract class SkillCardBase:MonoBehaviour,ICard
    {
        [Title("基础设置")]
        public string Id;
        public string Name;
        public string Summary;
        public Sprite CardUI;
        public int BulletCapacity;
        public WeaponBase WeaponPre;

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