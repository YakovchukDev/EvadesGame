using System.Collections.Generic;
using GamePlay.Character.Spell;
using Menu.SelectionClass;
using UnityEngine;

namespace GamePlay.Character
{
    public class CharacterSpawner : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _type;

        void Start()
        {
            if (SelectionClassView.WhatPlaying == "Level")
            {
                SpawnCharacter(new Vector3(0, 1, 65));
            }
            else if (SelectionClassView.WhatPlaying == "Infinity")
            {
                SpawnCharacter(new Vector3(0, 1, 0));
            }
        }


        private void SpawnCharacter(Vector3 startPosition)
        {
            
         GameObject game = Instantiate(_type[SelectionClassView.CharacterType], startPosition, Quaternion.identity);
         game.GetComponent<ManaController>();
        }
    }
}