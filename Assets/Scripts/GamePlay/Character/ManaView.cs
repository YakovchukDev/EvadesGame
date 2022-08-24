using GamePlay.Character.Spell;
using Map;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using GamePlay.Character;

namespace GamePlay
{
    public class ManaView : MonoBehaviour
    {
        [SerializeField] private Image _fillingImage;

        private void OnEnable()
        {
            ManaController.UpdateManaView += SetManaValue;
            CharacterSpawner.HandOverManaController += FillMana;
            EducationSpawnCharacter.HandOverManaController += FillMana;
        }

        private void OnDisable()
        {
            ManaController.UpdateManaView -= SetManaValue;
            CharacterSpawner.HandOverManaController -= FillMana;
            EducationSpawnCharacter.HandOverManaController -= FillMana;

        }
        private void SetManaValue(float value)
        {
            _fillingImage.DOFillAmount(value, value / 30);
        }

        private void FillMana(ManaController manaController)
        {
            _fillingImage.fillAmount = 1;
        }
    }
}
