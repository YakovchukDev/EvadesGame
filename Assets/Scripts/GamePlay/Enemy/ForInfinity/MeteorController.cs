using System.Collections;
using GamePlay.Character;
using UnityEngine;

namespace GamePlay.Enemy.ForInfinity
{
    public class MeteorController : MonoBehaviour
    {
        [SerializeField] private GameObject _meteorPrefab;
        [SerializeField] private CharacterSpawner _characterSpawner;
        private GameObject _player;

        private GameObject _meteor;


        private void Start()
        {
            _player = _characterSpawner.Character;
            _meteor = Instantiate(_meteorPrefab, transform.position, Quaternion.identity);
            _meteor.SetActive(false);
            StartCoroutine(Meteor());
        }

        private IEnumerator Meteor()
        {
            yield return new WaitForSeconds(3);
            var position = _player.transform.position;
            _meteor.transform.position = new Vector3(position.x, 0.6f, position.z);
            _meteor.SetActive(true);
            yield return new WaitForSeconds(7);
            _meteor.SetActive(false);
            StartCoroutine(Meteor());
        }
    }
}