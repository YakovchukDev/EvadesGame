using System.Collections.Generic;
using Menu.SelectionClass;
using UnityEngine;

namespace GamePlay.Character
{
    public class CharacterSpawner : SelectionClassView
    {
        [SerializeField] private List<GameObject> _type;

        void Start()
        {
            if (WhatPlaying == "Level")
            {
                SpawnCharacter(new Vector3(0, 1, 65));
            }
            else if (WhatPlaying == "Infinity")
            {
                SpawnCharacter(new Vector3(0, 1, 0));
            }
        }


        private void SpawnCharacter(Vector3 startPosition)
        {
            Instantiate(_type[CharacterType], startPosition, Quaternion.identity);
        }
    }
}