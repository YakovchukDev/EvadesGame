using UnityEngine;

namespace Menu
{
    public class MenuAnimatorController : MonoBehaviour
    {
        [SerializeField] private Animator _settingsAnimator;
        [SerializeField] private Animator _informationAnimator;
        [SerializeField] private Animator _selectionAnimator;
        [SerializeField] private Animator _levelsAnimator;

        

        public void Settings(int index)
        {
            _settingsAnimator.SetInteger("Settings", index);
        }

        public void Information(int index)
        {
            _informationAnimator.SetInteger("Information", index);
        }

        public void Selection(int index)
        {
            _selectionAnimator.SetInteger("Selection", index);
        }

        public void Levels(int index)
        {
            _levelsAnimator.SetInteger("Levels", index);
        }
    }
}