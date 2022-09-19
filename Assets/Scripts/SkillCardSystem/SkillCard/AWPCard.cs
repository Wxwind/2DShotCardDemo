using Sirenix.OdinInspector;
using SkillCardSystem.Weapon;
using UnityEngine;

namespace SkillCardSystem.SkillCard
{
    public class AWPCard:SkillCardBase
    {
        [Title("运行时信息")]
        [ShowInInspector, ReadOnly] private PlayerController m_player;
        
        public override void OnInit()
        {
            m_player = GameObject.Find("Player").GetComponent<PlayerController>();
            m_weapon = Instantiate(WeaponPre.gameObject, m_player.transform).GetComponent<WeaponBase>();
            m_weapon.OnInit();
        }

        public override void OnDestroySelf()
        {
            Destroy(gameObject);
            m_weapon.OnDestroySelf();
        }

        public override void OnActivate()
        {
            m_weapon.Shot();
        }

        public override void OnDiscord()
        {
            OnDestroySelf();
        }

        public override void OnSwitchOut()
        {
            m_weapon.gameObject.SetActive(false);
        }

        public override void OnSwitchIn()
        {
            m_weapon.ReEnterCD();
            m_weapon.gameObject.SetActive(true);
        }

        public override void OnEnterMainCardSlot()
        {
            m_weapon.gameObject.SetActive(true);
        }

        public override void OnEnterSpareCardSlot()
        {
            m_weapon.gameObject.SetActive(false);
        }
    }
}