using System.Collections.Generic;
using GamePlay.Character.Spell;
using Joystick_Pack.Examples;
using UnityEngine;

namespace GamePlay.Character
{
    public class InteractionWithTag : MonoBehaviour
    {
        [SerializeField] private ManaController _manaController;
        [SerializeField] private GameObject _father;
        [SerializeField] private List<GameObject> _noGrowUp;
        private int _numberSlower;
        private int _numberFaster;

        

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Slower") && _numberSlower == 0)
            {
                JoystickPlayerExample.Speed /= 2;
                _numberSlower++;
            }
            if (other.gameObject.CompareTag("Faster") && _numberFaster == 0)
            {
                JoystickPlayerExample.Speed *= 2;
                _numberFaster++;
            }
            if (other.gameObject.CompareTag("GrowUp"))
            {
                _father.transform.localScale = new Vector3(2, 2, 2);
                foreach (var noGrowUp in _noGrowUp)
                {
                    noGrowUp.transform.localScale = Vector3.one / 2;
                }
            }
            if (other.gameObject.CompareTag("ManaEater"))
            {
                _manaController.ManaReduction(0.1f);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Slower") && _numberSlower == 1)
            {
                JoystickPlayerExample.Speed *= 2;
                _numberSlower--;

            }
            if (other.gameObject.CompareTag("Faster") && _numberFaster == 1)
            {
                JoystickPlayerExample.Speed /= 2;
                _numberFaster--;
            }
            if (other.gameObject.CompareTag("GrowUp"))
            {
                if (other.gameObject.CompareTag("GrowUp"))
                {
                    _father.transform.localScale = new Vector3(1, 1, 1);
                    foreach (var noGrowUp in _noGrowUp)
                    {
                        noGrowUp.transform.localScale = Vector3.one;
                    }
                }
            }
        }
       
    }
}
