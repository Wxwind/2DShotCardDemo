using UnityEngine;

namespace SkillCardSystem.CollectableCard
{
    public class RevolverCollectable : CollectableCardBase
    {
        public override void Interact()
        {
            SkillCardManager.instance.ReplaceSpareCard(m_cardName);
            Destroy(gameObject);
        }
    }
}
