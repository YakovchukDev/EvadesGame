using System.Collections.Generic;
using Menu.level;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.SelectionClass
{
    public class ClassAvailability : MonoBehaviour
    {
        [SerializeField] private List<Button> _classes;
        [SerializeField] private List<TMP_Text> _maxTime;
        [SerializeField] private Button _levelButton;
        [SerializeField] private GameObject _levelInfoText;
        [SerializeField] private float[] _recordTime;
        private float _neededTime = 25;
        private int _neededPassedLevels = 5;

        public void OpenAllClass()
        {
            PlayerPrefs.SetFloat("JustTime", 200);
            PlayerPrefs.SetFloat("NecroTime", 200);
            PlayerPrefs.SetFloat("MorfeTime", 200);
            PlayerPrefs.SetFloat("NeoTime", 200);
            PlayerPrefs.SetFloat("InvulnerableTime", 200);
            PlayerPrefs.SetFloat("NexusTime", 200);
        }

        private void Start()
        {
            _recordTime = new float[_maxTime.Count];

            _recordTime[0] = PlayerPrefs.GetFloat("JustTime");
            _recordTime[1] = PlayerPrefs.GetFloat("NecroTime");
            _recordTime[2] = PlayerPrefs.GetFloat("MorfeTime");
            _recordTime[3] = PlayerPrefs.GetFloat("NeoTime");
            _recordTime[4] = PlayerPrefs.GetFloat("InvulnerableTime");
            _recordTime[5] = PlayerPrefs.GetFloat("NexusTime");

            for (int i = 0; i < _maxTime.Count; i++)
            {
                _maxTime[i].text = Mathf.Round(_recordTime[i]).ToString();
            }

            _levelButton.interactable = (_recordTime[0] >= 25);
            _levelInfoText.SetActive(_recordTime[0] < 25);
        }

        public void CheckClassForInfinity()
        {
            _classes[0].interactable = true;
            for (int i = 1; i < _classes.Count; i++)
            {
                if (LevelController.CompleteLevel >= _neededPassedLevels)
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