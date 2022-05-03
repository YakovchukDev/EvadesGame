using System.Collections.Generic;
using GamePlay.Character.Spell;
using Joystick_Pack.Examples;
using UnityEngine;

namespace GamePlay.Character
{
    public class InteractionWithTag : MonoBehaviour
    {
        [SerializeField] private ManaController _manaController;
        [SerializeField] private List<GameObject> _noGrowUp;
        [SerializeField] private GameObject _father;
        private int _numberSlower;
        private int _numberFaster;


        /*private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Coin"))
            {
                PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 1);
                _coinSpawner.CoinManipulate();
            }
        }*/

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

            if (other.gameObject.CompareTag("ManaEater") && _manaController != null)
            {
                _manaController.ManaReduction(0.5f);
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