﻿using Sirenix.OdinInspector;
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
            OnDestroySelf();
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
            m_weapon.gameObject.SetActive(true);
        }

        public override void OnEnterSpareCardSlot()
        {
            m_weapon.gameObject.SetActive(false);
        }
    }
}