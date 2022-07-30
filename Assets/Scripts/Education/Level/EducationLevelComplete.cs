using Education.Level.Controllers;
using UnityEngine;

namespace Education.Level
{
    public class EducationLevelComplete : MonoBehaviour
    {
        [SerializeField] private EducationResultController _resultController;

        public void EndOfGame()
        {
            _resultController.gameObject.SetActive(true);
           StartCoroutine( _resultController.ShowResult());
        }
    }
}