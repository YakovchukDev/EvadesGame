using System.Collections;
using Menu.SelectionClass;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Image = UnityEngine.UI.Image;

public class EducationSelectionView : MonoBehaviour
{
    [SerializeField] private Image _progressLine;
    [SerializeField] private Animator _slime;
    [SerializeField] private TMP_Text _spellText1;
    [SerializeField] private TMP_Text _spellText2;

    private readonly string _englishSkillType1 = "Resurrection";

    private readonly string _englishSkillType2 = "Invulnerability field";

    private readonly string _russianSkillType1 = "Воскресение";

    private readonly string _russianSkillType2 = "Поле неуязвимости";

    private readonly string _ukrainianSkillType1 = "Воскресіння";

    private readonly string _ukrainianSkillType2 = "Поле невразливості";


    public void Start()
    {
        switch (PlayerPrefs.GetString("Language"))
        {
            case "English":
                _spellText1.text = _englishSkillType1;
                _spellText2.text = _englishSkillType2;
                break;
            case "Russian":
                _spellText1.text = _russianSkillType1;
                _spellText2.text = _russianSkillType2;
                break;
            case "Ukrainian":
                _spellText1.text = _ukrainianSkillType1;
                _spellText2.text = _ukrainianSkillType2;
                break;
        }
    }

    public void StartEducation()
    {
        SelectionClassView.WhatPlaying = "Education";
        StartCoroutine(AsyncLoadScene("EducationLevel"));
    }

    private IEnumerator AsyncLoadScene(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        _slime.Play(0);
        while (!operation.isDone)
        {
            _progressLine.fillAmount = operation.progress;
            yield return null;
        }
    }
}