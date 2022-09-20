using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SkillCardSystem.View
{
    public class UnitCardView : MonoBehaviour
    {
        [SerializeField] private Image cardBackground;
        [SerializeField] private TMP_Text cardName;
        [SerializeField] private TMP_Text bulletNumInfo;
        [SerializeField] private Image weaponPreview;

        public void UpdateCardView(Sprite background,string name,int bulletNum,int maxBulletNum)
        {
            cardBackground.sprite = background;
            cardName.text = name;
            bulletNumInfo.text = $"{bulletNum}/{maxBulletNum}";
        }

        public void UpdateBulletNum(int newNum,int maxNum)
        {
            bulletNumInfo.text = $"{newNum}/{maxNum}";
        }
        
    }
}
