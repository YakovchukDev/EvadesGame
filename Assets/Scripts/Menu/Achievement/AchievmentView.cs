using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
namespace Menu.Achievement
{
    public class AchievmentView : MonoBehaviour
    {
        [SerializeField] private RectTransform _block;
        [SerializeField] private TMP_Text _head;
        [SerializeField] private TMP_Text _task;
        [SerializeField] private TMP_Text _reward;
        [SerializeField] private Image _checkMark;
        private List<Achievement> _completedAchievements;
        private bool _isShow;
        private float _timer;

        private void OnEnable()
        {
            Achievement.OnAchieveComplite += AddNewAchieveView;
        }
        private void OnDisable()
        {
            Achievement.OnAchieveComplite -= AddNewAchieveView;
        }
        private void Start()
        {
            _completedAchievements = new List<Achievement>();
            _isShow = false;
            _timer = 0f;
        }
        private void FixedUpdate()
        {
            if (_isShow)
            {
                _timer += Time.deltaTime;
                if (_timer >= 5f)
                {
                    _completedAchievements.RemoveAt(0);
                    StartCoroutine(HideBlock());
                    _timer = 0f;
                }
            }
            if(_completedAchievements.Count > 0 && !_isShow)
            {
                StartCoroutine(ShowBlock(_completedAchievements[0]));
            }
        }
        public IEnumerator ShowBlock(Achievement achievement)
        {
            _isShow = true;
            _head.text = achievement.Title.GetText();
            _task.text = achievement.Task.GetText();
            _block.DOAnchorPos(new Vector2(_block.position.x, 0), 0.5f);
            yield return new WaitForSeconds(1);
            _checkMark.DOFade(1, 0.5f);
            yield return new WaitForSeconds(1.5f);
            _checkMark.DOFade(0, 0f);
            _task.text = string.Empty;
            yield return new WaitForSeconds(0.2f);
            _reward.text = achievement.RewardStr.GetText();

        }
        public IEnumerator HideBlock()
        {
            _block.DOAnchorPos(new Vector2(_block.position.x, _block.sizeDelta.y), 0.5f);
            yield return new WaitForSeconds(0.5f);
            _head.text = string.Empty;
            _task.text = string.Empty;
            _reward.text = string.Empty;
            _checkMark.color = new Color(_checkMark.color.r, _checkMark.color.g, _checkMark.color.b, 0);
            _isShow = false;
        }
        private void AddNewAchieveView(Achievement achievement)
        {
            _completedAchievements.Add(achievement);
        }
    }
}
