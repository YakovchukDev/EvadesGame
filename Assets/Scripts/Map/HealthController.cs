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
            //не подключал библиотеку, потому что у нас одинаковые названия классов
            GamePlay.Character.HealthController.HealthPanelUpdate += UpdateHearts;
        }
        private void Disable()
        {
            //не подключал библиотеку, потому что у нас одинаковые названия классов
            GamePlay.Character.HealthController.HealthPanelUpdate -= UpdateHearts;
        }
        public void UpdateHearts(int hpCount)
        {
            foreach (GameObject heart in _hearts)
            {
                Animator animator = heart.gameObject.GetComponent<Animator>();
                if(hpCount == 0 && !animator.GetBool("IsEmpty"))
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
