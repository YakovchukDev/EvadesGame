using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

namespace Map.Health
{
    public class HealthController : MonoBehaviour
    { 
        [SerializeField] private List<GameObject> _hearts;

        private void OnEnable()
        {
            GamePlay.Character.HealthController.HealthPanelUpdate += UpdateHearts;
            GamePlay.Character.Spell.RespawnSpell.UpdateHearts += UpdateHearts;
        }
        private void Disable()
        {
            GamePlay.Character.HealthController.HealthPanelUpdate -= UpdateHearts;
            GamePlay.Character.Spell.RespawnSpell.UpdateHearts -= UpdateHearts;
        }
        public void UpdateHearts(int hpCount)
        {
            foreach (GameObject heart in _hearts)
            {
                if (heart != null)
                {
                    Animator animator = heart.gameObject.GetComponent<Animator>();
                    if (hpCount > 0 && animator.GetBool("IsEmpty"))
                    {
                        animator.Play("Resurrection");
                        animator.SetBool("IsEmpty", false);
                    }
                    else if (hpCount == 0 && !animator.GetBool("IsEmpty"))
                    {
                        animator.Play("DestroyHeart");
                        animator.SetBool("IsEmpty", true);
                    }
                    else
                    {
                        hpCount--;
                    }
                }
            }
        }
    }
}
