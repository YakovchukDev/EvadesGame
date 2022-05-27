using System;
using System.Collections.Generic;
using UnityEngine;
using Map.Coins;
using Map.Data;
using Menu.SelectionClass;
using GamePlay.Character.Spell;

namespace Map
{
    public class EntitiesGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject _emptyWall;
        [SerializeField] private StarController _star;
        [SerializeField] private CoinControl _coin;
        [SerializeField] private List<GameObject> _characterList;
        [SerializeField] private List<Enemy> _enemyList;
        [SerializeField] private GameObject _indestructibleEnemy;
        private GameObject _selectedCharacter;
        public static event Action<ManaController> HandOverManaController;

        public void InstantiateCharacter(Vector3 coordinateCharacter)
        {
            _selectedCharacter = Instantiate(_characterList[SelectionClassView.CharacterType], coordinateCharacter, Quaternion.identity);
            _selectedCharacter.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
            if (_selectedCharacter.GetComponent<ManaController>() != null)
            {
                HandOverManaController(_selectedCharacter.GetComponent<ManaController>());
            }
        }
        public GameObject GetSelectedCharacter() => _selectedCharacter;
        public void SetPlayerCoordinates(Vector3 coordinateCharacter)
        {
            _selectedCharacter.transform.position = new Vector3 ( coordinateCharacter.x, _selectedCharacter.transform.position.y, coordinateCharacter.z);
        }
        private bool LookLastRoom(StarSide side, int column, SafeZoneParameters [,] _safeZoneMap)
        {
            for (int row = _safeZoneMap.GetLength(0) - 1; row > 0; row--)
            {
                if (_safeZoneMap[row, column] != null && _safeZoneMap[row, column].IsSaveZone)
                {
                    StarController star = Instantiate(_star, new Vector3
                    (
                        _safeZoneMap[row, column].gameObject.transform.position.x,
                        _safeZoneMap[row, column].gameObject.transform.position.y,
                        _safeZoneMap[row, column].gameObject.transform.position.z
                    ), Quaternion.identity);
                    star.SetValueSide(side);
                    return true;
                }
            }
            return false;
        }
        public void GenerateAndSpawnStars(SafeZoneParameters [,] _safeZoneMap)
        {
            if (MapManager.LevelParameters.Branchs != 0)
            {
                if (!MapManager.MainDataCollector.Level.UpStars)
                {
                    for(int column = 0; column < _safeZoneMap.GetLength(1); column++)
                    {
                        if(LookLastRoom(StarSide.Up, column, _safeZoneMap))
                        {
                            break;
                        }
                    }
                }
                if (!MapManager.MainDataCollector.Level.DownStars)
                {
                    for(int column = _safeZoneMap.GetLength(1) - 1; column > 0; column--)
                    {
                        if(LookLastRoom(StarSide.Down, column, _safeZoneMap))
                        {
                            break;
                        }
                    }
                }
            }
            else
            {
                MapManager.MainDataCollector.Level.UpStars = true;
                MapManager.MainDataCollector.Level.DownStars = true;
            }
        }
        public List<CoinControl> GenerateAndSpawnCoinList(RoomParameters room)
        {
            int maxCoinInRoom = UnityEngine.Random.Range(0, MapManager.LevelParameters.MaxCoinInRoom);
            float indent = 3f;
            room.CoinList = new List<CoinControl>(maxCoinInRoom);
            for (int i = 0; i < maxCoinInRoom; i++)
            {
                room.CoinList.Add(Instantiate(_coin, new Vector3
                (
                    UnityEngine.Random.Range
                    (
                        room.gameObject.transform.position.x - room.GetLengthX() / 2 + indent,
                        room.gameObject.transform.position.x + room.GetLengthX() / 2 - indent
                    ),
                    1,
                    UnityEngine.Random.Range
                    (
                        room.gameObject.transform.position.z - room.GetLengthZ() / 2 + indent,
                        room.gameObject.transform.position.z + room.GetLengthZ() / 2 - indent
                    )
                ), Quaternion.identity));
                room.CoinList[room.CoinList.Count - 1].SetQuantityAddCoins(1);
            }
            return room.CoinList;
        }
        public List<GameObject> GenerateAndSpawnEmenyList(RoomParameters room)
        {
            //рахунок баллів для кімнати
            int score = MapManager.LevelParameters.Difficulty * 2 + 4 + 1 / 4;
            //відступ від края
            int indent = 2;
            //параметри спавна indestructibleEnemy
            int indestructibleEnemyCounter = 0;
            List<float> optionCoordX = new List<float>();
            for (int i = Convert.ToInt32(room.GetLengthX() / 2 - room.GetLengthX()); i < room.GetLengthX() / 2; i++)
            {
                if (i % 2 == 0)
                {
                    optionCoordX.Add(i);
                }
            }
            List<float> optionCoordZ = new List<float>()
            {
                room.GetLengthZ() / 2 - indent,
                -1 * room.GetLengthZ() / 2 + indent
            };
            //робимо виборку з підходящих ворогів
            List<Enemy> selectionEnemies = new List<Enemy>();
            foreach (Enemy enemy in _enemyList)
            {
                if (enemy.SpawnRate <= MapManager.MainDataCollector.GetLevelNumber())
                {
                    if (enemy.SpawnRate == MapManager.MainDataCollector.GetLevelNumber() && enemy.SpawnRate > 1)
                    {
                        if (room.GetPosition().x % 2 == 0)
                        {
                            selectionEnemies.Add(enemy);
                        }
                    }
                    else
                    {
                        selectionEnemies.Add(enemy);
                    }
                }
            }
            //генерація списка ворогів і водночас їх спавн
            while (score > 0 && room.EnemyList.Count < MapManager.LevelParameters.MaxEnemiesInRoom)
            {
                int index = UnityEngine.Random.Range(0, selectionEnemies.Count);
                if (selectionEnemies[index].ScoreRate <= score)
                {
                    //перевіряю через імена, тому що при зрівненні геймобджектів видає fаlse, припускаю, що це взязанно з тим, що префаб в процессі роботи змінює свої параметри і таким чином не співпадає
                    if (selectionEnemies[index].name == _indestructibleEnemy.name && indestructibleEnemyCounter < room.GetLengthX() / 2)
                    {
                        int indexCoordX = UnityEngine.Random.Range(0, optionCoordX.Count);
                        int indexCoordZ = UnityEngine.Random.Range(0, optionCoordZ.Count);
                        room.EnemyList.Add(Instantiate(selectionEnemies[index].EnemyObj, new Vector3
                        (
                            room.gameObject.transform.position.x + optionCoordX[indexCoordX],
                            1,
                            room.gameObject.transform.position.z + optionCoordZ[indexCoordZ]
                        ), Quaternion.identity, room.transform));
                        room.EnemyList[room.EnemyList.Count - 1].transform.Rotate(0, indexCoordZ == 0 ? 90 : -90, 180);
                        optionCoordX.RemoveAt(indexCoordX);
                        indestructibleEnemyCounter++;
                    }
                    else
                    {
                        selectionEnemies[index].EnemyObj.gameObject.transform.rotation = Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f);
                        int scaleSize = UnityEngine.Random.Range(30, 80);
                        selectionEnemies[index].EnemyObj.gameObject.transform.localScale = new Vector3(scaleSize, scaleSize, scaleSize);
                        room.EnemyList.Add(Instantiate(selectionEnemies[index].EnemyObj,
                        new Vector3
                        (
                            UnityEngine.Random.Range
                            (
                                room.gameObject.transform.position.x - room.GetLengthX() / 2 + indent,
                                room.gameObject.transform.position.x + room.GetLengthX() / 2 - indent
                            ),
                            1,
                            UnityEngine.Random.Range
                            (
                                room.gameObject.transform.position.z - room.GetLengthZ() / 2 + indent,
                                room.gameObject.transform.position.z + room.GetLengthZ() / 2 - indent
                            )
                        ), Quaternion.identity));
                    }

                    score -= selectionEnemies[index].ScoreRate;
                }
                else
                {
                    selectionEnemies.RemoveAt(index);
                }
            }
            return room.EnemyList;
        }
    }
}
