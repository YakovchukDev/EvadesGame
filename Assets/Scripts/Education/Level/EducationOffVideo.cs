using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

namespace Education.Level
{
    public class EducationOffVideo : MonoBehaviour
    {
        [SerializeField] private GameObject _video;
        [SerializeField] private GameObject _textAfterVideo;
        [SerializeField] private float _videoLenght;
        private void Start()
        {
            StartCoroutine(OffVideo());
        }

        private IEnumerator OffVideo()
        {
            yield return new WaitForSeconds(_videoLenght);
            _video.SetActive(false);
            _textAfterVideo.SetActive(true);
            _textAfterVideo.GetComponent<TMP_Text>().DOColor(new Color(1, 1, 1, 0), 1);
        }

    
    }
}