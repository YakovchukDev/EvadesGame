using System.Collections.Generic;
using Menu.ScriptableObject.level;
using Menu.ScriptableObject.Shop;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.SelectionClass
{
    public class ClassAvailability : MonoBehaviour
    {
        [SerializeField] private List<Button> _classes;
        [SerializeField] private float[] _recordTime;
        private float _neededTime = 25;
        private int _neededPassedLevels = 5;
        private int[] _buyOpen;

        public float[] RecordTime => _recordTime;

        private void Start()
        {
            _recordTime = new float[_classes.Count];
            _recordTime[0] = 2000;//PlayerPrefs.GetFloat("WeakTime");
            _recordTime[1] = 2000;//PlayerPrefs.GetFloat("NecroTime");
            _recordTime[2] = 2000;//PlayerPrefs.GetFloat("ShooterTime");
            _recordTime[3] = 2000;//PlayerPrefs.GetFloat("NeoTime");
            _recordTime[4] = 2000;//PlayerPrefs.GetFloat("TankTime");
            _recordTime[5] = 2000;//PlayerPrefs.GetFloat("NecromusTime");
            _buyOpen = new int[_classes.Count];
            for (int i = 0; i < _classes.Count; i++)
            {
                _buyOpen[i] = PlayerPrefs.GetInt($"Open{i}");
                if (_recordTime[i] >= 25 || _buyOpen[i] == 1)
                {
                    PlayerPrefs.SetInt("CompanyOpened", 1);
                }
            }
        }

        public void CheckClassForInfinity()
        {
            _classes[0].interactable = true;
            for (int i = 1; i < _classes.Count; i++)
            {
                if (LevelController.CompleteLevel >= _neededPassedLevels || PlayerPrefs.GetInt($"Open{i}") == 1)
                {
                    _classes[i].interactable = true;
                }
                else
                {
                    _classes[i].interactable = false;
                }

                _neededPassedLevels += 5;
            }

            _neededPassedLevels = 5;
            BoughtCharacter();
        }

        public void CheckClassForLevel()
        {
            _classes[0].interactable = true;
            for (int i = 1; i < _classes.Count; i++)
            {
                if (LevelController.CompleteLevel >= _neededPassedLevels)
                {
                    _classes[i].interactable = true;
                    _classes[0].interactable = true;
                    for (int j = 1; j < _classes.Count; j++)
                    {
                        if (_recordTime[j] >= _neededTime)
                        {
                            _classes[j].interactable = true;
                        }
                        else
                        {
                            _classes[j].interactable = false;
                        }

                        _neededTime += 25;
                    }

                    _neededTime = 50;
                }
                else
                {
                    _classes[i].interactable = false;
                }

                if (PlayerPrefs.GetInt($"Open{i}") == 1)
                {
                    _classes[i].interactable = true;
                }


                _neededPassedLevels += 5;
            }

            _neededPassedLevels = 5;
            BoughtCharacter();
        }

        private void BoughtCharacter()
        {
            for (int i = 1; i < _classes.Count; i++)
            {
                if (_buyOpen[i] == 1)
                {
                    _classes[i].interactable = true;
                }
            }
        }

        public void ExitClassSelection()
        {
            foreach (var classes in _classes)
            {
                classes.interactable = false;
            }
        }
    }
}