using SkillCardSystem.SkillCard;
using UnityEngine;

namespace SkillCardSystem.View
{
    /// <summary>
    /// 卡牌HUD
    /// </summary>
    public class SkillCardsView:MonoBehaviour
    {
        [SerializeField] private UnitCardView m_mainSkillCardView;
        [SerializeField] private UnitCardView m_spareSkillCardView;

        public void UpdateView(SkillCardBase main,SkillCardBase spare)
        {
            UpdateUnitView(main,m_mainSkillCardView);
            UpdateUnitView(spare,m_spareSkillCardView);
        }

        private void UpdateUnitView(SkillCardBase card, UnitCardView cardView)
        {
            if (card is null)
            {
                cardView.gameObject.SetActive(false);
                return;
            }
            cardView.gameObject.SetActive(true);
            cardView.UpdateCardView(card.BackGround,card.Name,card.Weapon.NowBulletCount,card.Weapon.BulletCapacity);
        }
        
    }
}