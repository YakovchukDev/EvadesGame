using System;
using UnityEngine;

namespace GamePlay.Map
{
    public class SpellChecker : MonoBehaviour
    {
        [SerializeField] private bool _spell1;
        [SerializeField] private bool _spell2;
        public static event Action OnRemoveSpell1;
        public static event Action OnRemoveSpell2;

        private void Start()
        {
            if (!_spell1)
            {
                OnRemoveSpell1?.Invoke();
            }
            if(!_spell2) 
            {
                OnRemoveSpell2?.Invoke();
            }
        }
    }
}
