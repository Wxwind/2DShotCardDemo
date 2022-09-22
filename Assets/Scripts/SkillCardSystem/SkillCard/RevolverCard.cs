using SkillCardSystem.Weapon;
using UnityEngine;

namespace SkillCardSystem.SkillCard
{
    public class RevolverCard : SkillCardBase
    {
        public override void OnInit()
        {
            base.OnInit();
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
            if (m_weapon is RevolverWeapon t)
            {
                t.ShotDicardBullet();
                SkillCardManager.instance.OnMainCardExhausted();
            }
        }

        public override void OnSwitchOut()
        {
            m_weapon.gameObject.SetActive(false);
        }

        public override void OnSwitchIn()
        {
            m_weapon.OnSwitchIn();
            m_weapon.gameObject.SetActive(true);
        }

        public override void OnEnterMainCardSlot()
        {
            base.OnEnterMainCardSlot();
        }

        public override void OnEnterSpareCardSlot()
        {
            m_weapon.gameObject.SetActive(false);
        }
    }
}