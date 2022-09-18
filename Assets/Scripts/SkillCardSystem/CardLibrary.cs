using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SkillCardSystem
{
    public class CardLibrary : SerializedMonoBehaviour
    {
        public static CardLibrary instance;
        [LabelText("存放游戏所有的卡牌预制体")]
        [SerializeField] private Dictionary<string, SkillCardBase> m_cardsPreData=new Dictionary<string, SkillCardBase>();
        //private List<ICard> cardPool=new List<ICard>();

        private void Awake()
        {
            if (instance!=null)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;
        }

        public bool TryGetCard(string cardId,out SkillCardBase card)
        {
            if (m_cardsPreData.TryGetValue(cardId,out var c))
            {
                card = c;
                return true;
            }
            card = null;
            return false;
        }
    }
}
