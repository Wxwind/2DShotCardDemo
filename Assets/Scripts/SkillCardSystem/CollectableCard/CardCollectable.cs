namespace SkillCardSystem.CollectableCard
{
    public class CardCollectable : CollectableCardBase
    {
        public override void Interact()
        {
            SkillCardManager.instance.ReplaceSpareCard(m_cardName);
            Destroy(gameObject);
        }
    }
}
