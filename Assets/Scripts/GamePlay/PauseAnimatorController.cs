using UnityEngine;

namespace GamePlay
{
    public class PauseAnimatorController : MonoBehaviour
    {
        [SerializeField] private Animator _pauseSettingsAnimator;
        [SerializeField] private Animator _pauseMainAnimator;
        public void Settings(int index)
        {
            _pauseSettingsAnimator.SetInteger("SettingsPanel", index);
        }
        public void Main(int index)
        {
            _pauseMainAnimator.SetInteger("MainPanel", index);
        }
    }
}
