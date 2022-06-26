using GamePlay.Character;
using GamePlay.Enemy.Move;
using Map.Data;
using Map.Expirience;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GamePlay.Enemy
{
    public class AttractionSystem : MonoBehaviour
    {
        [SerializeField] private MoveEnemy _moveEnemy;
        [SerializeField] private GameObject _horns;
        private bool _inSaveZone;

        private void Start()
        {
            _horns.SetActive(false);
        }

        private void OnEnable()
        {
            HealthController.OnBackToSafeZone += Refresh;
            SafeZoneParameters.OnEnter += OrSaveZone;

        }


        private void OnDisable()
        {
            HealthController.OnBackToSafeZone -= Refresh;
            SafeZoneParameters.OnEnter -= OrSaveZone;

        }
        private void OrSaveZone(bool orSaveZone)
        {
            _inSaveZone = orSaveZone;
        }
        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("GravityRadius") && other.gameObject.layer != 11)
            {
                if (!_inSaveZone)
                {
                    _moveEnemy.Rotate = true;
                    Vector3 direction = other.transform.position - transform.position;
                    direction.y = 0;
                    Quaternion rotation = Quaternion.LookRotation(direction);
                    _moveEnemy.Rotation = rotation;
                    _horns.SetActive(true);
                }
                else
                {
                    _horns.SetActive(false);
                }
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

        private void Refresh()
        {
            _horns.SetActive(false);
        }
    }
}