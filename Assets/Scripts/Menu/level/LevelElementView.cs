using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.level
{
    public class LevelElementView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _levelNumber;
        [SerializeField] private Button _levelButton;
        [SerializeField] private List<Image> _stars;
        public TMP_Text LevelNumber => _levelNumber;
        public Button LevelButton => _levelButton;
        public List<Image> Stars => _stars;

    }
}