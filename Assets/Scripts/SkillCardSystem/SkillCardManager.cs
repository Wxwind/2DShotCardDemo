using Sirenix.OdinInspector;
using SkillCardSystem.SkillCard;
using SkillCardSystem.View;
using SkillCardSystem.Weapon;
using UnityEngine;
using Utils;

namespace SkillCardSystem
{
    //管理当前角色拥有的卡牌和UI信息
    public class SkillCardManager : MonoBehaviour
    {
        public static SkillCardManager instance;
        //[Title("设置")] 
        [SerializeField]private SkillCardsView m_skillCardsView;
        [Title("当前拥有的卡牌")]
        [ReadOnly, ShowInInspector]private SkillCardBase m_mainSkillCard;
        [ReadOnly, ShowInInspector]private SkillCardBase m_spareSkillCard;

        public SkillCardBase MainSkillCard => m_mainSkillCard;
        public SkillCardBase SpareSkillCard => m_spareSkillCard;

        public WeaponBase NowWeapon
        {
            get
            {
                if (m_mainSkillCard != null)
                {
                    return m_mainSkillCard.Weapon;
                }
                return null;
            }
        }

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;
            UpdateUI();
        }

        /// <summary>
        /// 玩家获得卡牌，并刷新UI
        /// </summary>
        /// <param name="cardId">卡牌id</param>
        /// <returns>如果卡牌id不存在或者卡牌已满则返回false，否则为true</returns>
        public bool AddCard(string cardId)
        {
            if (!CardLibrary.instance.TryGetCard(cardId, out var c))
            {
                LogHelper.LogError($"SkillCardManager:不存在id为{cardId}的卡片 in CardLibrary");
                return false;
            }

            if (m_mainSkillCard!=null&&m_spareSkillCard!=null)
            {
                LogHelper.LogInfo("SkillCardManager:当前卡牌数量已满");
                return false;
            }
            
            var go = Instantiate(c.gameObject, transform);
            var skillCardComp = go.GetComponent<SkillCardBase>();
            skillCardComp.OnInit();
            
            if (m_mainSkillCard == null)
            {
                skillCardComp.OnEnterMainCardSlot();
                m_mainSkillCard = skillCardComp;
                LogHelper.LogInfo($"SkillCardManager:捡到卡牌Id:{cardId}，存入主卡牌中");
                UpdateUI();
                return true;
            }
            if (m_spareSkillCard == null)
            {
                skillCardComp.OnEnterSpareCardSlot();
                m_spareSkillCard = skillCardComp;
                LogHelper.LogInfo($"SkillCardManager:捡到卡牌Id:{cardId}，存入副卡牌中");
                UpdateUI();
                return true;
            }
            
            return false;
        }

        /// <summary>
        /// 主动激活卡牌，触发激活效果,并刷新UI
        /// </summary>
        public void ActivateCard()
        {
            if (m_mainSkillCard != null)
            {
                m_mainSkillCard.OnActivate();
                UpdateUI();
            }
        }

        /// <summary>
        /// 主动丢弃卡牌并触发弃牌效果，并在OnMainCardExhausted中刷新UI
        /// </summary>
        public void DiscardCard()
        {
            if (m_mainSkillCard != null)
            {
                LogHelper.LogInfo($"SkillCardManager:触发{m_mainSkillCard.Name}弃牌效果");
                m_mainSkillCard.OnDiscord();
            }
            UpdateUI();
        }

        /// <summary>
        /// 主动切换卡牌
        /// </summary>
        public void SwitchSkillCard()
        {
            if (m_mainSkillCard != null&&m_spareSkillCard!=null)
            {
                m_mainSkillCard.OnSwitchOut();
                m_spareSkillCard.OnSwitchIn();
                (m_mainSkillCard, m_spareSkillCard) = (m_spareSkillCard, m_mainSkillCard);
                LogHelper.LogInfo($"SkillCardManager:切换后主副卡牌：主={m_mainSkillCard.Name}||副={m_spareSkillCard.Name}");
            }
            UpdateUI();
        }

        /// <summary>
        /// 主动捡起卡牌并替换当前的备用卡牌，并在AddCard中刷新UI
        /// </summary>
        /// <param name="cardId"></param>
        public void ReplaceSpareCard(string cardId)
        {
            if (m_spareSkillCard!=null)
            {
                m_spareSkillCard.OnDestroySelf();
                m_spareSkillCard = null;
            }
            AddCard(cardId);
        }

        public void ReplaceMainCard(string cardId)
        {
            if (m_mainSkillCard!=null)
            {
                m_mainSkillCard.OnDestroySelf();
                m_mainSkillCard = ForceAddCardToMain(cardId);
            }
        }

        private SkillCardBase ForceAddCardToMain(string cardId)
        {
            if (!CardLibrary.instance.TryGetCard(cardId, out var c))
            {
                LogHelper.LogError($"SkillCardManager:不存在id为{cardId}的卡片 in CardLibrary");
                return null;
            }
            var go = Instantiate(c.gameObject, transform);
            var skillCardComp = go.GetComponent<SkillCardBase>();
            skillCardComp.OnInit();
            skillCardComp.OnEnterMainCardSlot();
            m_mainSkillCard = skillCardComp;
            LogHelper.LogInfo($"新的卡牌{cardId} 强制进入主卡牌位");
            UpdateUI();
            return m_mainSkillCard;
        }

        /// <summary>
        /// 销毁MainCard，切换至SpareCard(如果有的话)，并刷新UI;由卡牌自行调用，用于比如卡牌弃牌能力为替换成其他一张卡牌
        /// </summary>
        public void OnMainCardExhausted()
        {
            m_mainSkillCard.OnDestroySelf();
            m_mainSkillCard = null;
            if (m_spareSkillCard!=null)
            {
                m_spareSkillCard.OnSwitchIn();
                m_mainSkillCard = m_spareSkillCard;
                m_spareSkillCard = null;
            }
        }

        private bool HasMaxCards()
        {
            return m_spareSkillCard != null;
        }

        private void UpdateUI()
        {
            m_skillCardsView.UpdateView(m_mainSkillCard,m_spareSkillCard);
        }
    }
}