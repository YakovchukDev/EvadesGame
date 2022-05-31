using UnityEngine;

namespace Menu.ScriptableObject.level
{
    public class DownGroundForCompany : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransformContent;
        [SerializeField] private int _moveSpeed;

        private RectTransform _rectTransform;
        private int _numberPlusLenght;


        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
            
            if (_moveSpeed == 0)
            {
                _moveSpeed = 1;
            }
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            _rectTransform.anchoredPosition = new Vector2(
                (_rectTransformContent.anchoredPosition.x + _numberPlusLenght) / _moveSpeed,
                _rectTransform.anchoredPosition.y);
            if (_rectTransform.anchoredPosition.x <= -1910)
            {
                _numberPlusLenght += 1920;
            }
            else if (_rectTransform.anchoredPosition.x >= 1910)
            {
                _numberPlusLenght -= 1920;
            }
        }
    }
}