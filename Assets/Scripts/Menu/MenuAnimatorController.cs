using Menu.SelectionClass;
using UnityEngine;

namespace Menu
{
    public class MenuAnimatorController : MonoBehaviour
    {
        [SerializeField] private Animator _settingsAnimator;
        [SerializeField] private Animator _informationAnimator;
        [SerializeField] private Animator _selectionAnimator;
        [SerializeField] private Animator _levelsAnimator;
        [SerializeField] private Animator _infoAnimator;
        [SerializeField] private Animator _shopAnimator;
       
        public void Settings(int index)
        {
            _settingsAnimator.SetInteger("Settings", index);
        }

        public void Information(int index)
        {
            _informationAnimator.SetInteger("Information", index);
        }

        public void Survive(int index)
        {
            _selectionAnimator.SetInteger("Selection", index);
        }

        public void Company(int index)
        {
            _levelsAnimator.SetInteger("Levels", index);
        }

        public void Info(int index)
        {
            _infoAnimator.SetInteger("Information", index);
        }
        public void Shop(int index)
        {
            _shopAnimator.SetInteger("Information", index);
        }
    }
}