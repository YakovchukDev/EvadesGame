using System;
using System.Collections.Generic;
using GamePlay.Character;
using UnityEngine;
using Map.Coins;
using Map.Data;
using Map.Stars;
using Menu.SelectionClass;
using GamePlay.Character.Spell;
using GamePlay.Map.Coins;
using Random = UnityEngine.Random;

namespace Map
{
    public class EntitiesGenerator : MonoBehaviour
    {
        [SerializeField] private StarController _star;
        [SerializeField] private CoinControl _coin;
        [SerializeField] private List<GameObject> _characterList;
        [SerializeField] private List<Enemy> _enemyList;
        [SerializeField] private GameObject _indestructibleEnemy;
        [SerializeField] CharacterSpawner _characterSpawner;

        public GameObject SelectedCharacter { get; set; }

        private void Start()
        {
            SelectedCharacter = _characterSpawner.Character;
        }


        public void SetPlayerCoordinates(Vector3 coordinateCharacter)
        {
            SelectedCharacter.transform.position = new Vector3(coordinateCharacter.x,
                SelectedCharacter.transform.position.y, coordinateCharacter.z);
        }

        private bool SearchLastRoom(StarSide side, int column, SafeZoneParameters[,] _mapOfSafeZones)
        {
            for (int row = _mapOfSafeZones.GetLength(0) - 1; row > 0; row--)
            {
                if (_mapOfSafeZones[row, column] != null && _mapOfSafeZones[row, column].IsSaveZone)
                {
                    StarController star = Instantiate(
                        _star,  
                        _mapOfSafeZones[row, column].gameObject.transform.position, 
                        Quaternion.identity, 
                        _mapOfSafeZones[row, column].transform);
                    star.SetValueSide(side);
                    return true;
                }
            }

            return false;
        }

        private bool SearchRoomOnCentralBranch(StarSide side, SafeZoneParameters[,] _mapOfSafeZones)
        {
            List<SafeZoneParameters> listOfSafeZone = new List<SafeZoneParameters>();
            for (int row = 0; row < _mapOfSafeZones.GetLength(0); row++)
            {
                if (row != 0 && row != _mapOfSafeZones.GetLength(0) - 1 &&
                    _mapOfSafeZones[row, MapManager.LevelParameters.Branchs].IsSaveZone)
                {
                    listOfSafeZone.Add(_mapOfSafeZones[row, MapManager.LevelParameters.Branchs]);
                }
            }

            int indexOfSafeZone = 0;
            while (listOfSafeZone.Count > 0)
            {
                indexOfSafeZone = Random.Range(0, listOfSafeZone.Count);
                if (listOfSafeZone[indexOfSafeZone].gameObject.transform.Find("StarOfAchievement") == null)
                {
                    StarController star = Instantiate(_star, new Vector3
                    (
                        listOfSafeZone[indexOfSafeZone].gameObject.transform.position.x,
                        listOfSafeZone[indexOfSafeZone].gameObject.transform.position.y,
                        listOfSafeZone[indexOfSafeZone].gameObject.transform.position.z
                    ), Quaternion.identity, listOfSafeZone[indexOfSafeZone].gameObject.transform);
                    star.SetValueSide(side);
                    return true;
                }
                else
                {
                    listOfSafeZone.RemoveAt(indexOfSafeZone);
                }
            }

            return false;
        }
        
        public void GenerateAndSpawnStars(SafeZoneParameters[,] _mapOfSafeZones)
        {
            if (MapManager.LevelParameters.Branchs != 0)
            {
                if (!MapManager.MainDataCollector.Level.UpStar)
                {
                    bool isFound = false;
                    for (int column = _mapOfSafeZones.GetLength(1) - 1;
                         column > MapManager.LevelParameters.Branchs;
                         column--)
                    {
                        if (SearchLastRoom(StarSide.Up, column, _mapOfSafeZones))
                        {
                            isFound = true;
                            break;
                        }
                    }
                    if (!isFound && !SearchRoomOnCentralBranch(StarSide.Up, _mapOfSafeZones))
                    {
                        MapManager.MainDataCollector.Level.UpStar = true;
                    }
                }
                if (!MapManager.MainDataCollector.Level.DownStar)
                {
                    bool isFound = false;
                    for (int column = 0; column < MapManager.LevelParameters.Branchs; column++)
                    {
                        if (SearchLastRoom(StarSide.Down, column, _mapOfSafeZones))
                        {
                            isFound = true;
                            break;
                        }
                    }
                    if (!isFound && !SearchRoomOnCentralBranch(StarSide.Down, _mapOfSafeZones))
                    {
                        MapManager.MainDataCollector.Level.DownStar = true;
                    }
                }
            }
            else
            {
                MapManager.MainDataCollector.Level.UpStar = true;
                MapManager.MainDataCollector.Level.DownStar = true;
            }
        }

        public void GenerateAndSpawnCoinList(RoomParameters room)
        {
            int maxCoinInRoom = Random.Range(0, MapManager.LevelParameters.MaxCoinInRoom);
            float indent = 3f;
            for (int i = 0; i < maxCoinInRoom; i++)
            {
                CoinControl coin = Instantiate(_coin, new Vector3
                (
                    Random.Range
                    (
                        room.gameObject.transform.position.x - room.GetLengthX() / 2 + indent,
                        room.gameObject.transform.position.x + room.GetLengthX() / 2 - indent
                    ),
                    1,
                    Random.Range
                    (
                        room.gameObject.transform.position.z - room.GetLengthZ() / 2 + indent,
                        room.gameObject.transform.position.z + room.GetLengthZ() / 2 - indent
                    )
                ), Quaternion.identity, room.transform);
                coin.SetQuantityAddCoins(1);
            }
        }

        public void GenerateAndSpawnEmenyList(RoomParameters room)
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
            for (int enemyCount = 0; score > 0 && enemyCount < MapManager.LevelParameters.MaxEnemiesInRoom;)
            {
                int index = Random.Range(0, selectionEnemies.Count);
                if (selectionEnemies[index].ScoreRate <= score)
                {
                    //перевіряю через імена, тому що при зрівненні геймобджектів видає fаlse, припускаю, що це взязанно з тим, що префаб в процессі роботи змінює свої параметри і таким чином не співпадає
                    if (selectionEnemies[index].name == _indestructibleEnemy.name &&
                        indestructibleEnemyCounter < room.GetLengthX() / 2)
                    {
                        int indexCoordX = Random.Range(0, optionCoordX.Count);
                        int indexCoordZ = Random.Range(0, optionCoordZ.Count);
                        GameObject enemy = Instantiate(selectionEnemies[index].EnemyObj, new Vector3
                        (
                            room.gameObject.transform.position.x + optionCoordX[indexCoordX],
                            1,
                            room.gameObject.transform.position.z + optionCoordZ[indexCoordZ]
                        ), Quaternion.identity, room.transform);
                        enemyCount++;
                        enemy.transform.Rotate(0, indexCoordZ == 0 ? 90 : -90, 180);
                        optionCoordX.RemoveAt(indexCoordX);
                        indestructibleEnemyCounter++;
                    }
                    else
                    {
                        selectionEnemies[index].EnemyObj.gameObject.transform.rotation =
                            Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
                        int scaleSize = Random.Range(30, 80);
                        selectionEnemies[index].EnemyObj.gameObject.transform.localScale =
                            new Vector3(scaleSize, scaleSize, scaleSize);
                        Instantiate(selectionEnemies[index].EnemyObj,
                            new Vector3
                            (
                                Random.Range
                                (
                                    room.gameObject.transform.position.x - room.GetLengthX() / 2 + indent,
                                    room.gameObject.transform.position.x + room.GetLengthX() / 2 - indent
                                ),
                                1,
                                Random.Range
                                (
                                    room.gameObject.transform.position.z - room.GetLengthZ() / 2 + indent,
                                    room.gameObject.transform.position.z + room.GetLengthZ() / 2 - indent
                                )
                            ), Quaternion.Euler(0, Random.Range(30, 330), 0), room.transform);
                        enemyCount++;
                    }

                    score -= selectionEnemies[index].ScoreRate;
                }
                else
                {
                    selectionEnemies.RemoveAt(index);
                }
            }
        }
    }
}