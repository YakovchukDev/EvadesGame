using System.Collections.Generic;
using GamePlay.Enemy;
using GamePlay.Enemy.Move;
using UnityEngine;

namespace GamePlay.Character
{
    public class AttractionSystem : MonoBehaviour
    {
        private HashSet<GameObject> _enemyObject = new HashSet<GameObject>();
        private float time = 1;
        [SerializeField] private GameObject _horns;
        private float _rotationSpeed = 15;

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Gravitation"))
            {
                var moveScript = other.gameObject.GetComponent<MoveEnemy>();
                if (moveScript.enabled)
                {
                    _enemyObject.Add(other.gameObject);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Gravitation"))
            {
                _enemyObject.Remove(other.gameObject);

                other.transform.rotation =
                    Quaternion.Lerp(other.transform.rotation,
                        Quaternion.Euler(new Vector3(0, Random.Range(-90, 90), 0)), _rotationSpeed * Time.deltaTime);
                other.transform.GetChild(1).gameObject.SetActive(false);
            }
        }

        private void Update()
        {
            Attraction();
        }

        private void Attraction()
        {
            foreach (GameObject enemy in _enemyObject)
            {
                Vector3 direction = transform.position - enemy.transform.position;
                direction.y = 0;
                Quaternion rotation = Quaternion.LookRotation(direction);
                enemy.transform.rotation =
                    Quaternion.Lerp(enemy.transform.rotation, rotation, _rotationSpeed * Time.deltaTime);
                enemy.transform.GetChild(1).gameObject.SetActive(true);

                /*else if (_setRotation)
                {
                    //Vector3 direction = new Vector3(0, Random.Range(0, 360), 0);
                    _enemy.transform.rotation = Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0));
                    // transform.rotation = Quaternion.Lerp(transform.rotation, rotation, _rotationSpeed * Time.deltaTime);
                    _horns.SetActive(false);
                    _setRotation = false;
                }*/
            }
        }
    }
}