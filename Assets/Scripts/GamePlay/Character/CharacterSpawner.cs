using System.Collections.Generic;
using Menu.SelectionClass;
using UnityEngine;

namespace GamePlay.Character
{
    public class CharacterSpawner : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _type;

        void Start()
        {
            if (SelectionClassView._whatPlaying == "Level")
            {
                SpawnCharacter(new Vector3(0, 1, 68));
            }
            else if (SelectionClassView._whatPlaying == "Infinity")
            {
                SpawnCharacter(new Vector3(0, 1, 0));
            }
        }

        

        public void SpawnCharacter(Vector3 startPosition)
        {
            if (SelectionClassView._characterType == "Just")
            {
                Instantiate(_type[0], startPosition, Quaternion.identity);
            }
            else if (SelectionClassView._characterType == "Necro")
            {
                Instantiate(_type[1], startPosition, Quaternion.identity);

            }
            else if (SelectionClassView._characterType == "Morfe")
            {
                Instantiate(_type[2], startPosition, Quaternion.identity);

            }
            else if (SelectionClassView._characterType == "Neo")
            {
                Instantiate(_type[3], startPosition, Quaternion.identity);

            }
            else if (SelectionClassView._characterType == "Invulnerable")
            {
                Instantiate(_type[4], startPosition, Quaternion.identity);

            }
            else if (SelectionClassView._characterType == "Nexus")
            {
                Instantiate(_type[5], startPosition, Quaternion.identity);
            }
        }
    }
}