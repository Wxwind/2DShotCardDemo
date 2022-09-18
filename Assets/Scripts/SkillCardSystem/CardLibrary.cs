using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SkillCardSystem
{
    public class CardLibrary : SerializedMonoBehaviour
    {
        [SerializeField] private Dictionary<string, ICard> m_cardsData=new Dictionary<string, ICard>();
        private List<ICard> cardPool=new List<ICard>();

        public void GetCards()
        {
        
        }
    }
}
