using System.Collections.Generic;
using Menu.level;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Menu
{
    public class ClassAvailability : MonoBehaviour
    {
        [SerializeField] private List<Button> _classes;
        [SerializeField] private List<TMP_Text> _maxTime;
        [SerializeField] private float[] _recordTime;
        private float _neededTime = 50;
        private int _neededPassedLevels = 5;

        private void Start()
        {
            _recordTime = new float[_maxTime.Count];
            _recordTime[0] = 200; //PlayerPrefs.GetFloat("JustTime");
            _recordTime[1] = 200; //PlayerPrefs.GetFloat("NecroTime");
            _recordTime[2] = 200; //PlayerPrefs.GetFloat("MorfeTime");
            _recordTime[3] = 200; //PlayerPrefs.GetFloat("NeoTime");
            _recordTime[4] = 200; //PlayerPrefs.GetFloat("InvulnerableTime");
            _recordTime[5] = 200; //PlayerPrefs.GetFloat("NexusTime");

            for (int i = 0; i < _maxTime.Count; i++)
            {
                _maxTime[i].text = Mathf.Round(_recordTime[i]).ToString();
            }
        }

        public void CheckClassForInfinity()
        {
            _classes[0].interactable = true;
            for (int i = 1; i < _classes.Count; i++)
            {
                if (LevelController._completeLevel >= _neededPassedLevels)
                {
                    _classes[i].interactable = true;
                    _classes[0].interactable = true;
                    for (int j = 1; j < _classes.Count; j++)
                    {
                        if (_recordTime[j - 1] >= _neededTime)
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

                _neededPassedLevels += 5;
            }

            _neededPassedLevels = 5;
        }

        public void CheckClassForLevel()
        {
            _classes[0].interactable = true;
            for (int i = 1; i < _classes.Count; i++)
            {
                if (LevelController._completeLevel >= _neededPassedLevels)
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