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
        [ReadOnly, ShowInInspector] SkillCardBase SpareSkillCard;
        [ReadOnly, ShowInInspector] SkillCardBase MainSkillCard;

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

            if (SpareSkillCard != null)
            {
                Debug.Log("当前卡牌数量已满");
                return false;
            }

            var go = Instantiate(c.gameObject, transform);
            var skillCardComp = go.GetComponent<SkillCardBase>();
            skillCardComp.OnInit();
            SpareSkillCard = skillCardComp;
            return true;
        }

        public void ActivateCard()
        {
            if (MainSkillCard != null)
            {
                MainSkillCard.OnActivate();
            }
        }

        //主动丢弃卡牌
        public void DiscordCard()
        {
            if (MainSkillCard != null)
            {
                MainSkillCard.OnDiscord();
            }
        }

        //主动切换卡牌
        public void SwitchSkillCard()
        {
            if (MainSkillCard != null)
            {
                MainSkillCard.OnSwitch();
            }
        }

        public void OnMainCardExhausted()
        {
            MainSkillCard = SpareSkillCard;
            SpareSkillCard = null;
        }

        public void UpdateUI()
        {
        }
    }
}