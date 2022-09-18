namespace SkillCardSystem
{
    public abstract class CardBase:ICard
    {
        public string Id;
        public string Summary;
        public virtual void OnInit()
        {
        }

        public virtual void OnDestroySelf()
        {
 
        }

        public virtual void OnActivate()
        {
     
        }

        public virtual void OnDiscord()
        {
           
        }
    }
}