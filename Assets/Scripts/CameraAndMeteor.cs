using Menu.SelectionClass;
using UnityEngine;

public class CameraAndMeteor : MonoBehaviour
{
    private readonly string[] _characterObject =
    {
        "CharacterWeak(Clone)", "CharacterNecro(Clone)", "CharacterShooter(Clone)", "CharacterNeo(Clone)",
        "CharacterTank(Clone)", "CharacterNecromusComponent"
    };

    [SerializeField] private GameObject _player;
    private const float OffSetY = 15;
    private Vector3 _cameraPosition;
    [SerializeField] private GameObject _meteorPrefab;
    private GameObject _meteor;
    private float _time1;
    private float _time2;
    private bool _spawn1;


    private void Start()
    {
        _spawn1 = true;

        if (_player == null)
        {
            _player = GameObject.Find(_characterObject[SelectionClassView.CharacterType]);
        }
    }

    private void Update()
    {
        Camera();
        if (SelectionClassView.WhatPlaying == "Infinity")
        {
            Meteor();
        }
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

    private void Camera()
    {
        if (_player == null)
        {
            _player = GameObject.Find(_characterObject[SelectionClassView.CharacterType]);
        }

        var position = _player.transform.position;
        _cameraPosition.x = position.x;
        _cameraPosition.y = position.y + OffSetY;
        _cameraPosition.z = position.z;

        transform.position = _cameraPosition;
    }
}