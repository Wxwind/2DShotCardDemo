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
            var c = other.GetComponent<CollectableCardBase>();
            if (c!=null)
            {
                if (SkillCardManager.instance.AddCard(c.CardName))
                {
                    Destroy(c.gameObject);
                }
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            var c = other.GetComponent<CollectableCardBase>();
            if (c!=null&&Input.GetKey(InputKeyManager.instance.interactKey))
            {
                c.Interact();
                Destroy(c.gameObject);
            }
        }
    }
}