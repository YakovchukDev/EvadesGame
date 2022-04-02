using System.Collections;
using Menu.SelectionClass;
using UnityEngine;

namespace GamePlay.Enemy
{
    public class MeteorController : MonoBehaviour
    {
        private readonly string[] _characterObject =
        {
            "CharacterWeak(Clone)", "CharacterNecro(Clone)", "CharacterShooter(Clone)", "CharacterNeo(Clone)",
            "CharacterTank(Clone)", "CharacterNecromusComponent"
        };

        [SerializeField] private GameObject _player;
        [SerializeField] private GameObject _meteorPrefab;
        private GameObject _meteor;

        private void Start()
        {
            StartCoroutine(MeteorSpawner());
        }

        private IEnumerator MeteorSpawner()
        {
            if (_player == null)
            {
                _player = GameObject.Find(_characterObject[SelectionClassView.CharacterType]);
            }

            yield return new WaitForSeconds(3);
            _meteor = Instantiate(_meteorPrefab, new Vector3(_player.transform.position.x, 0, _player.transform.position.z),
                Quaternion.identity);
            while (true)
            {
                Destroy(_meteor, 7);
                yield return new WaitForSeconds(8);
                _meteor = Instantiate(_meteorPrefab,
                    new Vector3(_player.transform.position.x, 0, _player.transform.position.z), Quaternion.identity);
            }
        }
    }
}