using Game;
using UnityEngine;

namespace SkillCardSystem.CollectableCard
{
    public abstract class CollectableCardBase : MonoBehaviour,Iinteractable
    {
        [SerializeField] protected string m_cardName;

        public string CardName => m_cardName;
        public abstract void Interact();
    }
}