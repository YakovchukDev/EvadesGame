using System.Collections.Generic;
using Menu.SelectionClass;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class AbilityInfo : SelectionClassView
    {
        [SerializeField] private List<Sprite> _spellSprites1;
        [SerializeField] private List<Sprite> _spellSprites2;

        [SerializeField] private TMP_Text _spellText1;
        [SerializeField] private TMP_Text _spellText2;
        [SerializeField] private Image _spellImage1;
        [SerializeField] private Image _spellImage2;
        private int _characterNumber;

        private readonly string[] _englishSkillType1 =
            {"Nothing", "Resurrection", "Reduction", "Freezing field", "Acceleration", "Resurrection"};

        private readonly string[] _englishSkillType2 =
            {"Nothing", "Nothing", "Deactivation", "Teleport", "Invulnerability", "Invulnerability field"};

        private readonly string[] _russianSkillType1 =
            {"Ничего", "Воскресение", "Уменьшение", "Замораживающее поле", "Ускорение", "Воскресение"};

        private readonly string[] _russianSkillType2 =
            {"Ничего", "Ничего", "Деактивация", "Tелепорт", "Неуязвимость", "Поле неуязвимости"};

        private readonly string[] _ukrainianSkillType1 =
            {"Нічого", "Воскресіння", "Зменшення", "Заморожуюче поле", "Прискорення", "Воскресіння"};

        private readonly string[] _ukrainianSkillType2 =
            {"Нічого", "Нічого", "Деактивація", "Tелепортація", "Невразливість", "Поле невразливості"};

        private void Start()
        {
            SetAbilities(PlayerPrefs.GetInt("SelectionNumber"));
        }

        public void SetAbilities(int characterNumber)
        {
            PlayerPrefs.SetInt("SelectionNumber", characterNumber);
            _spellImage1.sprite = _spellSprites1[characterNumber];
            _spellImage2.sprite = _spellSprites2[characterNumber];
            switch (PlayerPrefs.GetString("Language"))
            {
                case "English":
                    _spellText1.text = _englishSkillType1[characterNumber];
                    _spellText2.text = _englishSkillType2[characterNumber];
                    break;
                case "Russian":
                    _spellText1.text = _russianSkillType1[characterNumber];
                    _spellText2.text = _russianSkillType2[characterNumber];
                    break;
                case "Ukrainian":
                    _spellText1.text = _ukrainianSkillType1[characterNumber];
                    _spellText2.text = _ukrainianSkillType2[characterNumber];
                    break;
            }
        }
    }
}