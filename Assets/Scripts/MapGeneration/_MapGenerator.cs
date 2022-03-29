// КОМЕНТАРІ ЯКІ ПОЗНАЧЕНІ "***", ЦЕ ДЛЯ СЕБЕ, ЩОБ НЕ ЗАБУТИ, ЩО САМЕ Я ХОТІВ ДОДАТИ, ЧИ ЗМІНИТИ
//код написан за 40 гривень, на качество були накладені санкції
using System.Collections.Generic;
using UnityEngine;

namespace MapGeneration
{
    public class _MapGenerator : MonoBehaviour
    {
        //Нужно соединить этот код с GeneralParameters
        [SerializeField] private _RoomParameters _arena;
        [SerializeField] private _SaveZoneParameters _saveZone;
        [SerializeField] private GameObject _emptyWall;
        
        [SerializeField] private GameObject _character;
        [SerializeField] private List<GameObject> _justEnemies;
        [SerializeField] private List<GameObject> _midleEnemies;
        [SerializeField] private List<GameObject> _hardEnemies;
        [SerializeField] private GameObject _indestructibleEnemy;
        
        [SerializeField] private int _difficulty;
        [SerializeField] private int[] _difficultyFromTo = new int[2];
        [SerializeField] private int _length;
        [SerializeField] private int _branchs;
        [SerializeField] private int _sizeChank;

        private _RoomParameters[,] _map;
        private _SaveZoneParameters[,] _saveZoneMap;

        private GameObject[,,] _listEntities;
        private int lastcordX = 0;
        private int lastcordY = 0;
        private GameObject lastEmptyWallStart;
        private GameObject lastEmptyWallEnd;
        
        private void Start()
        {
            _listEntities = new GameObject[_length, _branchs * 2 + 1, 30];
            lastcordX = _branchs;
            lastcordY = -1;

            generateOfPrimaryView();
            generateOfSaveZone();

            showAllMap();
            startSpawn(_branchs, 0);

            setExitSaveZone();
            setUpExitInRoom();

            //Instantiate(_character, new Vector3(0, 2, _arena.GetLengthZ() * _branchs), Quaternion.identity);
        }
        private void OnEnable()
        {
            _RoomParameters.EnterNewRoom += spawnMapAroundPayer;
        }
        private void OnDisable()
        {
            _RoomParameters.EnterNewRoom -= spawnMapAroundPayer;
        }
        //Створюю геометрію рівня(кількість і позицію кімнат)
        private void generateOfPrimaryView()
        {
            // *** Транспоновати матрицю, щоб перебір даних виглядів більш коерктно 
            _map = new _RoomParameters[_length, _branchs * 2 + 1];
            int [] quantityBranchsInChank = new int[_branchs];
            int [] quantityBranchs = new int[_branchs];

            for(int row = 0; row < _map.GetLength(0); row++)
            {
                if(row % _sizeChank == 0)
                {
                    for(int i = 0; i < _branchs; i++)
                    {
                        quantityBranchsInChank[i] = (_difficulty * ((row /_sizeChank) + 1)) / (i + 1);
                        quantityBranchs[i] = 0;
                    }
                }
                for(int column = 0; column < _map.GetLength(1); column++)
                {
                    if(column == _branchs)
                    {
                        _map[row, column] = _arena;
                    }
                    else
                    {
                        int indexBranch = column > _branchs ? column - _branchs - 1 : _branchs - column - 1;
                        // *** Налаштувати quantityBranchs зараз розподіл працює не коректно
                        if((row > 0 && _map[row - 1, column] != null) || column - 1 == _branchs || column + 1 == _branchs)
                        {
                            if(quantityBranchs[indexBranch] < quantityBranchsInChank[indexBranch])
                            {
                                if(Random.Range(0, 2) == 1)
                                {
                                    _map[row, column] = _arena;
                                    quantityBranchs[indexBranch]++;
                                }
                                else if(Random.Range(0, 2) == 1)
                                {
                                    if(column > _branchs)
                                    {
                                        _map[row, column] = _arena;
                                        _map[row, column - 1] = _arena;
                                        quantityBranchs[indexBranch]++;
                                    }
                                    else if(column < _branchs)
                                    {
                                        _map[row, column] = _arena;
                                        _map[row, column + 1] = _arena;
                                        quantityBranchs[indexBranch]++;
                                    }
                                }
                            }
                        }
                        else 
                        {
                            if(row > 0 && column > 0 && _map[row - 1, column - 1] != null)
                            {
                                if(quantityBranchs[indexBranch] < quantityBranchsInChank[indexBranch])
                                {
                                    if(Random.Range(0, 3) == 1)
                                    {
                                        _map[row, column] = _arena;
                                        _map[row, column - 1] = _arena;
                                        quantityBranchs[indexBranch]++;
                                    }
                                }
                            }
                            else if(row > 0 && column < _map.GetLength(1) - 1 && _map[row - 1, column + 1] != null)
                            {
                                if(quantityBranchs[indexBranch] < quantityBranchsInChank[indexBranch])
                                {
                                    if(Random.Range(0, 3) == 1)
                                    {
                                        _map[row, column] = _arena;
                                        _map[row, column + 1] = _arena;
                                        quantityBranchs[indexBranch]++;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        private void generateOfSaveZone()
        {
            _saveZoneMap = new _SaveZoneParameters[_length + 1, _branchs * 2 + 1];
            
            _saveZoneMap[0, _branchs] = _saveZone;

            for(int row = 0; row < _map.GetLength(0); row++)
            {
                for(int column = 0; column < _map.GetLength(1); column++)
                {
                    if(row + 1 < _map.GetLength(0) && _map[row + 1, column] != null && _map[row, column] != null)
                    {
                        _saveZoneMap[row + 1, column] = _saveZone;
                    }
                    else if(_map[row, column] != null && Random.Range(0, 2) == 1)
                    {
                        _saveZoneMap[row + 1, column] = _saveZone;
                    }

                    if(0 < row && _map[row - 1, column] != null && _map[row, column] != null)
                    {
                        _saveZoneMap[row, column] = _saveZone;
                    }
                    else if(_map[row, column] != null && Random.Range(0, 2) == 1)
                    {
                        _saveZoneMap[row, column] = _saveZone;
                    }
                }
            }
            _saveZoneMap[_saveZoneMap.GetLength(0) - 1, _branchs] = _saveZone;
        }
        
        private void setUpExitInRoom()
        {
            for(int column = 0; column < _map.GetLength(1); column++)
            {
                for(int row = 0; row < _map.GetLength(0); row++)
                {
                    if(_map[row, column] != null)
                    {
                        _map[row, column].name = $"Arena {column} | {row}";
                        _map[row, column].SetDoorParameters
                        (
                            (column + 1 < _map.GetLength(1) && _map[row, column + 1] == null) || column + 1 == _map.GetLength(1),
                            (row + 1 < _map.GetLength(0) && _map[row + 1, column] == null && _saveZoneMap[row + 1, column] == null) || (row + 1 == _map.GetLength(0) && _saveZoneMap[row + 1, column] == null),
                            (0 <column && _map[row, column - 1] == null) || column == 0,
                            (row > 0 && _map[row - 1, column] == null && _saveZoneMap[row, column] == null) || (row == 0 && _saveZoneMap[row, column] == null),
                            false,
                            false
                        );
                    }
                }
            }
        }
        private void setExitSaveZone()
        {
            for(int column = 0; column < _saveZoneMap.GetLength(1); column++)
            {
                for(int row = 0; row < _saveZoneMap.GetLength(0); row++)
                {
                    if(_saveZoneMap[row, column] != null)
                    {
                        if(0 < row && row < _map.GetLength(0))
                        {
                            if(_map[row, column] != null && _map[row - 1, column] != null)
                            {
                                _saveZoneMap[row, column].name = $"SaveZoneHorizontal {column} | {row}";
                               _saveZoneMap[row, column].SetHorizontalParameters(Random.Range(0, 2) == 1); 
                            }
                            else if(_map[row, column] == null && _map[row - 1, column] != null)
                            {
                                _saveZoneMap[row, column].name = $"SaveZoneLeft {column} | {row}";
                                _saveZoneMap[row, column].SetOneWay("left", Random.Range(0, 2) == 1);
                            }
                            else if(_map[row, column] != null && _map[row - 1, column] == null)
                            {
                                _saveZoneMap[row, column].name = $"SaveZoneRight {column} | {row}";
                                _saveZoneMap[row, column].SetOneWay("right", Random.Range(0, 2) == 1);
                            }
                        }
                        else if(row == _saveZoneMap.GetLength(0) - 1)
                        {
                            _saveZoneMap[row, column].name = $"SaveZoneLeft {column} | {row}";
                            _saveZoneMap[row, column].SetOneWay("left", column == _branchs ? true : Random.Range(0, 2) == 1);
                        }
                        else if (row == 0)
                        {
                            _saveZoneMap[row, column].name = $"SaveZoneRight {column} | {row}";
                            _saveZoneMap[row, column].SetOneWay("right", column == _branchs ? true : Random.Range(0, 2) == 1);
                        }
                    }
                }
            }
        }
        private void showAllMap()
        {
            for(int column = 0; column < _map.GetLength(1); column++)
            {
                for(int row = 0; row < _map.GetLength(0); row++)
                {
                    if(_map[row, column] != null)
                    {
                        _map[row, column] = Instantiate(_map[row, column], new Vector3(_arena.GetLengthX() / 2f + _saveZone.GetLengthX() / 2f + _arena.GetLengthX() * row + _saveZone.GetLengthX() * row, 0f, _arena.GetLengthZ() * column), Quaternion.identity);
                        _map[row, column].SetCordinatRoom(column, row);
                        _map[row, column].gameObject.SetActive(false);
                    }
                }
                for(int row = 0; row < _saveZoneMap.GetLength(0); row++)
                {
                    if(_saveZoneMap[row, column] != null)
                    {
                        _saveZoneMap[row, column] = Instantiate(_saveZoneMap[row, column], new Vector3(_arena.GetLengthX() * row + _saveZone.GetLengthX() * row, 0, _saveZone.GetLengthZ() * column), Quaternion.identity);
                        _saveZoneMap[row, column].gameObject.SetActive(false);
                    }
                }
            }
        }
        private void startSpawn(int x, int y)
        {
            for(int column = x - 1 > 0 ? x - 1 : 0; column <= (x + 1 < _map.GetLength(1) - 1 ? x + 1 : _map.GetLength(1) - 1); column++)
            {
                for(int row = y - 1 > 0 ? y - 1 : 0; row <= (y + 1 < _map.GetLength(0) - 1 ? y + 1 : _map.GetLength(0) - 1); row++)
                {
                    if(_map[row, column] != null)
                    {
                        _map[row, column].gameObject.SetActive(true);
                        spawnEnemiesAroundPayer(row, column);
                    }
                }
                for(int row = y - 1 > 0 ? y - 1 : 0; row <= (y + 2 < _saveZoneMap.GetLength(0) - 1 ? y + 2 : _saveZoneMap.GetLength(0) - 1); row++)
                {
                    if(_saveZoneMap[row, column] != null)
                    {
                        _saveZoneMap[row, column].gameObject.SetActive(true);
                    }
                }
            }
            
            lastEmptyWallStart = Instantiate(_emptyWall, new Vector3(_arena.GetLengthX() * (y - 2 > 0 ? y - 2 : 0) + _saveZone.GetLengthX() * (y - 2 > 0 ? y - 2 : 0) - _saveZone.GetLengthX() / 2f - 0.5f, 0f, _arena.GetLengthZ() * x), Quaternion.identity);
            lastEmptyWallEnd = Instantiate(_emptyWall, new Vector3(_arena.GetLengthX() * (y + 2) + _saveZone.GetLengthX() * (y + 2) + _saveZone.GetLengthX() / 2f + 0.5f, 0f, _arena.GetLengthZ() * x), Quaternion.identity);

            lastcordX = x;
            lastcordY = y;
        }
        private void spawnMapAroundPayer(int x, int y)
        {
            if(lastcordX != x || lastcordY != y)
            {  
                int coordinateDifferenceX = x - lastcordX;
                int coordinateDifferenceY = y - lastcordY;

                int coordStartX = 0;
                int coordStartY = 0;
                int coordEndX = 0;
                int coordEndY = 0;
                if(coordinateDifferenceX == 1)
                {
                    coordStartX = lastcordX - 1 > 0 ? lastcordX - 1 : 0;
                    coordStartY = lastcordY - 1 > 0 ? lastcordY - 1 : 0;
                    
                    coordEndX = coordStartX + 1;
                    coordEndY = coordStartY + 3;
                }
                else if(coordinateDifferenceX == -1)
                {
                    coordStartX = lastcordX + 1 < _map.GetLength(1) - 1 ? lastcordX + 1 : _map.GetLength(1) - 1;
                    coordStartY = lastcordY - 1 > 0 ? lastcordY - 1 : 0;
                    
                    coordEndX = coordStartX + 1;
                    coordEndY = coordStartY + 3;
                }
                else if(coordinateDifferenceY == 1)
                {
                    coordStartX = lastcordX - 1 > 0 ? lastcordX - 1 : 0;
                    coordStartY = lastcordY - 1 > 0 ? lastcordY - 1 : 0;
                    
                    coordEndX = coordStartX + 3;
                    coordEndY = coordStartY + (y != 0 && y != 1 ? 1 : 0);
                }
                else if(coordinateDifferenceY == -1)
                {
                    coordStartX = lastcordX - 1 > 0 ? lastcordX - 1 : 0;
                    coordStartY = lastcordY + 1 < _map.GetLength(0) - 1 ? lastcordY + 1 : _map.GetLength(0) - 1;
                    
                    coordEndX = coordStartX + 3;
                    coordEndY = coordStartY + 1;
                }
                for(int column = coordStartX; column < coordEndX; column++)
                {
                    for(int row = coordStartY; row < coordEndY; row++)
                    {
                        if(_map[row, column] != null)
                        {
                            _map[row, column].gameObject.SetActive(false);
                            for(int height = 0; height < _listEntities.GetLength(2); height++)
                            {
                                if(_listEntities[row, column, height] != null)
                                {
                                    Destroy(_listEntities[row, column, height]);
                                    _listEntities[row, column, height] = null;
                                }
                            }
                        }
                    }
                    for(int row = coordStartY + (coordinateDifferenceY == -1 ? 1 : 0); row < coordEndY + (coordinateDifferenceY != 1 ? 1 : 0); row++)
                    {
                        if(_saveZoneMap[row, column] != null)
                        {
                            _saveZoneMap[row, column].gameObject.SetActive(false);
                        }
                    }
                }

                if(coordinateDifferenceX == 1)
                {
                    coordStartX = x + 1 < _map.GetLength(1) - 1 ? x + 1 : _map.GetLength(1) - 1;
                    coordStartY = y - 1 > 0 ? y - 1 : 0;
                    
                    coordEndX = coordStartX + 1;
                    coordEndY = coordStartY + 3;
                }
                else if(coordinateDifferenceX == -1)
                {
                    coordStartX = x - 1 > 0 ? x - 1 : 0;
                    coordStartY = y - 1 > 0 ? y - 1 : 0;
                    
                    coordEndX = coordStartX + 1;
                    coordEndY = coordStartY + 3;
                }
                else if(coordinateDifferenceY == 1)
                {
                    coordStartX = x - 1 > 0 ? x - 1 : 0;
                    coordStartY = y + 1 < _map.GetLength(0) - 1 ? y + 1 : _map.GetLength(0) - 1;
                    
                    coordEndX = coordStartX + 3;
                    coordEndY = coordStartY + 1;
                }
                else if(coordinateDifferenceY == -1)
                {
                    coordStartX = x - 1 > 0 ? x - 1 : 0;
                    coordStartY = y - 1 > 0 ? y - 1 : 0;
                    
                    coordEndX = coordStartX + 3;
                    coordEndY = coordStartY + (_listEntities[x, y, 0] == null && y != 0 ? 1 : 0);
                }
                
                for(int column = coordStartX; column < coordEndX; column++)
                {
                    for(int row = coordStartY; row < coordEndY; row++)
                    {
                        if(_map[row, column] != null)
                        {
                            _map[row, column].gameObject.SetActive(true);
                            spawnEnemiesAroundPayer(row, column);
                        }
                    }
                    for(int row = coordStartY; row < coordEndY + 1; row++)
                    {
                        if(_saveZoneMap[row, column] != null)
                        {
                            _saveZoneMap[row, column].gameObject.SetActive(true);
                        }
                    }
                }
                
                lastEmptyWallStart.transform.position = new Vector3(_arena.GetLengthX() * (y - 1 > 0 ? y - 1 : 0) + _saveZone.GetLengthX() * (y - 1 > 0 ? y - 1 : 0) - _saveZone.GetLengthX() / 2f - 0.5f, 0f, _arena.GetLengthZ() * x);
                lastEmptyWallEnd.transform.position = new Vector3(_arena.GetLengthX() * (y + 2) + _saveZone.GetLengthX() * (y + 2) + _saveZone.GetLengthX() / 2f + 0.5f, 0f, _arena.GetLengthZ() * x);

                lastcordX = x;
                lastcordY = y;
            }
        }

        private void generateListEmeny(int x, int y)
        {
            int quantityIndestructibleEnemy = 4;
            int quantityJustEnemy = 2;
            int quantityMidleEnemy = 1;
            int quantityHardEnemy = 0;

            int beginingSum = 0;
            int endingSum = 0;

            endingSum += quantityIndestructibleEnemy < _listEntities.GetLength(2) ? quantityIndestructibleEnemy : _listEntities.GetLength(2) - 1;
            for(int index = beginingSum; index < endingSum; index++)
            {
                _listEntities[x, y, index] = _indestructibleEnemy;
            }
            beginingSum += quantityIndestructibleEnemy;

            endingSum += quantityJustEnemy < _listEntities.GetLength(2) ? quantityJustEnemy : _listEntities.GetLength(2) - 1;
            for(int index = beginingSum; index < endingSum; index++)
            {
                _listEntities[x, y, index] = _justEnemies[Random.Range(0, _justEnemies.Count)];
            }
            beginingSum += quantityJustEnemy;

            endingSum += quantityMidleEnemy < _listEntities.GetLength(2) ? quantityMidleEnemy : _listEntities.GetLength(2) - 1;
            for(int index = beginingSum; index < endingSum; index++)
            {
                _listEntities[x, y, index] = _midleEnemies[Random.Range(0, _midleEnemies.Count)];
            }
            beginingSum += quantityMidleEnemy;

            endingSum += quantityHardEnemy < _listEntities.GetLength(2) ? quantityHardEnemy : _listEntities.GetLength(2) - 1;
            for(int index = beginingSum; index < endingSum; index++)
            {
                _listEntities[x, y, index] = _hardEnemies[Random.Range(0, _hardEnemies.Count)];
            }
        }
        private void spawnEnemiesAroundPayer(int row, int column)
        {
            generateListEmeny(row, column);
            for(int height = 0; height < _listEntities.GetLength(2); height++)
            {
                if(_listEntities[row, column, height] != null)
                {
                    if(_listEntities[row, column, height] == _indestructibleEnemy)
                    {
                        _listEntities[row, column, height].gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
                        float [] listCordX = new float[2] { -25.5f, 25.5f };
                        float [] listCordY = new float[2] { -5f, 5f };
                        _listEntities[row, column, height] = Instantiate(_listEntities[row, column, height], new Vector3
                        (
                            (_arena.GetLengthX() / 2f + _saveZone.GetLengthX() / 2f + _arena.GetLengthX() * row + _saveZone.GetLengthX() * row) + Random.Range(listCordX[0], listCordX[1]), 
                            2f, 
                            (_arena.GetLengthZ() * column) + listCordY[Random.Range(0, listCordY.GetLength(0))]
                        ), Quaternion.identity);
                    }
                    else
                    {
                        float indent = 3f;
                        _listEntities[row, column, height].gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
                        _listEntities[row, column, height] = Instantiate(_listEntities[row, column, height], new Vector3
                        (
                            Random.Range
                            (
                                (indent + _saveZone.GetLengthX() / 2f + _arena.GetLengthX() * row + _saveZone.GetLengthX() * row), 
                                (_arena.GetLengthX() - indent + _saveZone.GetLengthX() / 2f + _arena.GetLengthX() * row + _saveZone.GetLengthX() * row)
                            ), 
                            2f, 
                            Random.Range
                            (    
                                _arena.GetLengthZ() * column - (_arena.GetLengthZ() / 2f + indent),
                                _arena.GetLengthZ() * column + (_arena.GetLengthZ() / 2f - indent)
                            )
                        ), Quaternion.identity);
                    }
                } 
            }
        }
    }
}