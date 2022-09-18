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
        [Title("设置")] public int MaxCardCount = 2;
        [Title("当前拥有的卡牌")] [ReadOnly] public LinkedList<SkillCardBase> CardDeck;
        public SkillCardBase MainSkillCard => CardDeck.First.Value;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;
        }


        public void AddCard(string cardId)
        {
            if (!CardLibrary.instance.TryGetCard(cardId, out var c))
            {
                Debug.LogError($"不存在id为{cardId}的卡片");
            }

            if (CardDeck.Count >= MaxCardCount)
            {
                Debug.Log("当前卡牌数量已满");
                return;
            }

            var go = Instantiate(c.gameObject, transform);
            var skillCardComp = go.GetComponent<SkillCardBase>();
            skillCardComp.OnInit();
            CardDeck.AddLast(skillCardComp);
        }

        public void ActivateCard()
        {
            if (MainSkillCard != null)
            {
                MainSkillCard.OnActivate();
            }
        }

        public void DiscordCard()
        {
            if (MainSkillCard != null)
            {
                MainSkillCard.OnDiscord();
            }
        }

        public void SwitchSkillCard()
        {
            if (MainSkillCard != null)
            {
                MainSkillCard.OnSwitch();
            }
        }
    }
}