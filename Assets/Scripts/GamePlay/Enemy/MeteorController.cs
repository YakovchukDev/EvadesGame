using GamePlay.Character;
using UnityEngine;

namespace GamePlay.Enemy
{
    public class MeteorController : MonoBehaviour
    {
        [SerializeField] private GameObject _meteorPrefab;
        [SerializeField] private CharacterSpawner _characterSpawner;
        private GameObject _player;
        private GameObject _meteor;
        private float _time1;
        private float _time2;
        private bool _spawn1;

        private void Start()
        {
            _spawn1 = true;
            _player = _characterSpawner.Character;
        }

        private void FixedUpdate()
        {
            Meteor();
        }

        private void Meteor()
        {
            _time1 += Time.deltaTime;
            if (_time1 >= 3 && _spawn1)
            {
                _meteor = Instantiate(_meteorPrefab,
                    new Vector3(_player.transform.position.x, 0.6f, _player.transform.position.z),
                    Quaternion.identity);
                _spawn1 = false;
            }

            Destroy(_meteor, 7);
            if (_time1 >= 3)
            {
                _time2 += Time.deltaTime;
                if (_time2 >= 8)
                {
                    _time2 = 0;
                    _time1 = 0;
                    _spawn1 = true;
                }
            }
        }
    }
}