using System;
using System.Collections.Generic;
using GamePlay.Character.Spell;
using Menu.SelectionClass;
using UnityEngine;

namespace GamePlay.Character
{
    public class CharacterSpawner : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _type;
        public GameObject Character { get; private set; }
        public Vector3 StartPosition { get; set; }
        public static event Action<ManaController> HandOverManaController;
        public static event Action InitializeUpgradePanel;

        private void Awake()
        {
            if (SelectionClassView.WhatPlaying == "Infinity")
            {
                SpawnCharacter(new Vector3(0, 1, 0), Quaternion.identity);
            }

            if (SelectionClassView.WhatPlaying == "Level")
            {
                SpawnCharacter(transform.position, new Quaternion(0, 90, 0, 0));
                if (Character.GetComponent<ManaController>() != null)
                {
                    HandOverManaController?.Invoke(Character.GetComponent<ManaController>());
                }

                InitializeUpgradePanel?.Invoke();
            }
        }

        private void Start()
        {
            Character.transform.position = StartPosition;
        }

        private void SpawnCharacter(Vector3 startPosition, Quaternion quaternion)
        {
            Character = Instantiate(_type[SelectionClassView.CharacterType], startPosition, quaternion);
        }
    }
}