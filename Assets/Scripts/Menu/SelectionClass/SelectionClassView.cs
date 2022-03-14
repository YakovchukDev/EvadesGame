using UnityEngine.SceneManagement;
using UnityEngine;

namespace Menu.SelectionClass
{
    public class SelectionClassView : MonoBehaviour
    {
        public static string _characterType;
        public static string _whatPlaying;

        public void ChoiceTypeOfCharacter(string characterType)
        {
            _characterType = characterType;
        }

        public void Teststart()
        {
            SceneManager.LoadScene("InfinityGame");
        }

        public void SetWhatPlaying(string whatPlaying)
        {
            _whatPlaying = whatPlaying;
        }

        public void CheckWhatPlaying()
        {
            if (_whatPlaying == "Level")
            {
                SceneManager.LoadScene("MapGeneratorBeta");
            }
            else if (_whatPlaying == "Infinity")
            {
                SceneManager.LoadScene("InfinityGame");
            }
        }
    }
}