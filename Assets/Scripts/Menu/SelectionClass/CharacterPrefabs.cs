using System.Collections.Generic;
using UnityEngine;
using System;

namespace Menu.SelectionClass
{
    [Serializable]
    public class CharacterPrefabs
    {
        [SerializeField] private TypeOfCharacter _typeOfCharacter;
        [SerializeField] private List<GameObject> _gameObject;

        public TypeOfCharacter TypeOfCharacter => _typeOfCharacter;
        public List<GameObject> GameObject => _gameObject;

    }
}