using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Sirenix.OdinInspector;
using UnityEngine;

public class CardLibrary : SerializedMonoBehaviour
{
    [SerializeField] private Dictionary<string, ICard> m_cardsData=new Dictionary<string, ICard>();
    private List<ICard> cardPool=new List<ICard>();

    public void GetCards()
    {
        
    }
}
