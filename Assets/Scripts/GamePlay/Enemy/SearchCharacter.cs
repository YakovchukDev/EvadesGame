using System;
using GamePlay.Enemy.Move;
using Menu.SelectionClass;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GamePlay.Enemy
{
    public class SearchCharacter : MonoBehaviour
    {
        [SerializeField] private GameObject _player;
        private Animator _hornsAnimation;
       

        private void Start()
        {
            _hornsAnimation = GetComponent<Animator>();
        }

    
    }
}