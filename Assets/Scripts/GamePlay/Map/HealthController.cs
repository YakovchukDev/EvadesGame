using System.Collections.Generic;
using Ads;
using UnityEngine;
using UnityEngine.UI;

namespace Map.Health
{
    public class HealthController : MonoBehaviour
    {
        [SerializeField] private List<Image> _hearts;

        private void OnEnable()
        {
            GamePlay.Character.HealthController.HealthPanelUpdate += UpdateHearts;
            GamePlay.Character.Spell.RespawnSpell.UpdateHearts += UpdateHearts;
            RewardedAdsButton.HealthPanelUpdate += UpdateHearts;
        }

        private void OnDisable()
        {
            GamePlay.Character.HealthController.HealthPanelUpdate -= UpdateHearts;
            GamePlay.Character.Spell.RespawnSpell.UpdateHearts -= UpdateHearts;
            RewardedAdsButton.HealthPanelUpdate -= UpdateHearts;
        }

        private void UpdateHearts(int hpCount)
        {
            foreach (Image heart in _hearts)
            {
                if (heart != null)
                {
                    if (hpCount > 0 && heart.fillAmount == 0)
                    {
                        heart.GetComponent<Animator>().Play("Resurrection");
                    }
                    else if (hpCount <= 0 && heart.fillAmount == 1)
                    {
                        heart.GetComponent<Animator>().Play("DestroyHeart");
                    }

                    hpCount--;
                }
            }
        }
    }
}