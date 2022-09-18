using UnityEngine;

namespace SkillCardSystem.CollectableCard
{
    public abstract class CollectableCaedBase : MonoBehaviour
    {
        [SerializeField] protected string m_cardName;

        public string CardName => m_cardName;
    }
}