namespace SkillCardSystem
{
    public interface ICard
    {
        ///初始化卡牌/生成武器，UI等
        void OnInit();
        ///生命周期结束/销毁卡牌和武器及UI等
        void OnDestroySelf();
        ///卡牌的激活技能/通常为左键射击
        void OnActivate();
        ///卡牌的弃牌技能/通常为右键弃牌触发效果
        void OnDiscord();
        ///切换至其他卡牌时触发效果/最起码要隐藏当前的武器
        void OnSwitch();
        ///切换至该卡牌时触发效果/最起码要切换至当前武器
        void OnEquip();
    }
}