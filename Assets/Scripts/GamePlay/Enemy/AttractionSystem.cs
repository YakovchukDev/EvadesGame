using GamePlay.Enemy.Move;
using UnityEngine;

namespace GamePlay.Enemy
{
    public class AttractionSystem : MonoBehaviour
    {
        [SerializeField] private MoveEnemy _moveEnemy;
        [SerializeField] private GameObject _horns;

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("GravityRadius")&& other.gameObject.layer!=11)
            {
                _moveEnemy.Rotate = true;
                Vector3 direction = other.transform.position - transform.position;
                direction.y = 0;
                Quaternion rotation = Quaternion.LookRotation(direction);
                _moveEnemy.Rotation = rotation;
                _horns.SetActive(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("GravityRadius"))
            {
                _moveEnemy.Rotate = true;
                _moveEnemy.Rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
                _horns.SetActive(false);
            }
        }
    }
}