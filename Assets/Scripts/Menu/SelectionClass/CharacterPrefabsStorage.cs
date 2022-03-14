using System.Collections.Generic;
using UnityEngine;

namespace Menu.SelectionClass
{
    [CreateAssetMenu(fileName = "CharacterPrefabsStorage", menuName = "CharacterPrefabs/PrefabsFeatures")]
    public class CharacterPrefabsStorage : ScriptableObject
    {
        [SerializeField] private List<CharacterPrefabs> _characterPrefabs;
        public List<CharacterPrefabs> CharacterPrefabs => _characterPrefabs;
    }
}