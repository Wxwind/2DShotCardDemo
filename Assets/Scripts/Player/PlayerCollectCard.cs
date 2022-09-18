using System;
using SkillCardSystem;
using SkillCardSystem.CollectableCard;
using UnityEngine;

namespace Player
{
    public class PlayerCollectCard:MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            var c = other.GetComponent<CollectableCaedBase>();
            if (c!=null)
            {
                SkillCardManager.instance.AddCard(c.CardName);
            }
        }
    }
}