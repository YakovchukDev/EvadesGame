using System;
using System.Collections;
using Menu.SelectionClass;
using UnityEngine;

public class MeteorController : SelectionClassView
{
    private readonly string[] _characterObject =
    {
        "CharacterJust(Clone)", "CharacterNecro(Clone)", "CharacterMorfe(Clone)", "CharacterNeo(Clone)",
        "CharacterMagmax(Clone)", "CharacterNexusComponent"
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
            _player = GameObject.Find(_characterObject[CharacterType]);
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