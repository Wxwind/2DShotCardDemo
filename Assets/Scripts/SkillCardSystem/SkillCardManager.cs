using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SkillCardSystem
{
    //管理当前角色拥有的卡牌和UI信息
    public class SkillCardManager : MonoBehaviour
    {
        public static SkillCardManager instance;
        //[Title("设置")] 

        [Title("当前拥有的卡牌")]
        [ReadOnly, ShowInInspector] SkillCardBase m_spareSkillCard;
        [ReadOnly, ShowInInspector] SkillCardBase m_mainSkillCard;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;
        }

        /// <summary>
        /// 玩家获得卡牌
        /// </summary>
        /// <param name="cardId">卡牌id</param>
        /// <returns>如果卡牌id不存在或者卡牌已满则返回false，否则为true</returns>
        public bool AddCard(string cardId)
        {
            if (!CardLibrary.instance.TryGetCard(cardId, out var c))
            {
                Debug.LogError($"不存在id为{cardId}的卡片");
                return false;
            }
            var go = Instantiate(c.gameObject, transform);
            var skillCardComp = go.GetComponent<SkillCardBase>();
            skillCardComp.OnInit();
            
            if (m_mainSkillCard==null)
            {
                skillCardComp.OnEnterMainCardSlot();
                m_mainSkillCard = skillCardComp;
                return true;
            }
            if (m_spareSkillCard == null)
            {
                skillCardComp.OnEnterSpareCardSlot();
                m_spareSkillCard = skillCardComp;
                return true;
            }
            Debug.Log("当前卡牌数量已满");
            return false;
        }

        //主动激活卡牌，触发激活效果
        public void ActivateCard()
        {
            if (m_mainSkillCard != null)
            {
                m_mainSkillCard.OnActivate();
            }
        }

        //主动丢弃卡牌并触发弃牌效果
        public void DiscordCard()
        {
            if (m_mainSkillCard != null)
            {
                m_mainSkillCard.OnDiscord();
            }
            OnMainCardExhausted();
        }

        //主动切换卡牌
        public void SwitchSkillCard()
        {
            if (m_mainSkillCard != null&&m_spareSkillCard!=null)
            {
                m_mainSkillCard.OnSwitchOut();
                m_spareSkillCard.OnSwitchIn();
            }
        }

        //捡起卡牌并替换当前的备用卡牌
        public void ReplaceSpareCard(string cardId)
        {
            if (m_spareSkillCard!=null)
            {
                m_spareSkillCard.OnDestroySelf();
            }
            AddCard(cardId);
        }

        /// <summary>
        /// 销毁MainCard，切换至SpareCard(如果有的话)
        /// </summary>
        public void OnMainCardExhausted()
        {
            m_mainSkillCard.OnDestroySelf();
            if (m_spareSkillCard!=null)
            {
                m_spareSkillCard.OnSwitchIn();
                m_mainSkillCard = m_spareSkillCard;
                m_spareSkillCard = null;
            }
        }

        public bool HasMaxCards()
        {
            return m_spareSkillCard != null;
        }

        public void UpdateUI()
        {
        }
    }
}