namespace SkillCardSystem
{
    public interface ICard
    {
        //初始化卡牌
        void OnInit();
        //生命周期结束，销毁卡牌
        void OnDestroySelf();
        //卡牌的激活技能/通常为左键射击
        void OnActivate();
        //卡牌的弃牌技能/通常为右键弃牌触发效果
        void OnDiscord();
    }
}