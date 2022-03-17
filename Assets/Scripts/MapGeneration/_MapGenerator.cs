using System.Collections.Generic;
using UnityEngine;

namespace MapGeneration
{
    public class _MapGenerator : MonoBehaviour
    {
        [SerializeField]
        private _RoomParameters _arena;
        [SerializeField]
        private _SaveZoneParameters _saveZone;

        [SerializeField]
        private _Entities _entities;
        
        [SerializeField]
        private int _difficulty;
        [SerializeField]
        private int[] _difficultyFromTo = new int[2];
        [SerializeField]
        private int _length;
        [SerializeField]
        private int _branchs;
        [SerializeField]
        private int _sizeChank;

        private _RoomParameters[,] _map;
        private _SaveZoneParameters[,] _saveZoneMap;
        
        void Start()
        {
            //defence from idiot
            if(_difficulty < _difficultyFromTo[0])
            {
                _difficulty = _difficultyFromTo[0];
            }
            else if(_difficulty > _difficultyFromTo[1])
            {
                _difficulty = _difficultyFromTo[1];
            }
            if(_sizeChank < 5)
            {
                _sizeChank = 5;
            }
            if(_branchs < 0)
            {
                _branchs = 0;
            }
            if(_length < _sizeChank)
            {
                _length = _sizeChank;
            }

            generateOfPrimaryView();
            generateOfSaveZone();
            showAllMap();
            setExitSaveZone();
            setUpExitInRoom();
        }

        private void generateOfPrimaryView()
        {
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

                        if((row > 0 && _map[row - 1, column] != null) || column - 1 == _branchs || column + 1 == _branchs)
                        {
                            if(quantityBranchs[indexBranch] < quantityBranchsInChank[indexBranch] && 
                                Random.Range(0, 2) == 1)
                            {
                                _map[row, column] = _arena;
                                quantityBranchs[indexBranch]++;
                            }
                        }
                        else if(row > 0 && column > 0 && _map[row - 1, column - 1] != null)
                        {
                            if(quantityBranchs[indexBranch] < quantityBranchsInChank[indexBranch] &&
                                Random.Range(0, 2) == 1)
                            {
                                _map[row, column] = _arena;
                                _map[row, column - 1] = _arena;
                                quantityBranchs[indexBranch]++;
                            }
                        }
                        else if(row > 0 && column < _map.GetLength(1) - 1 && _map[row - 1, column + 1] != null)
                        {
                            if(quantityBranchs[indexBranch] < quantityBranchsInChank[indexBranch] &&
                                Random.Range(0, 2) == 1)
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
                                _saveZoneMap[row, column].name = "SaveZoneHorizontal";
                               _saveZoneMap[row, column].SetHorizontalParameters(Random.Range(0, 2) == 1); 
                            }
                            else if(_map[row, column] == null && _map[row - 1, column] != null)
                            {
                                _saveZoneMap[row, column].name = "SaveZoneLeft";
                                _saveZoneMap[row, column].SetOneWay("left", Random.Range(0, 2) == 1);
                            }
                            else if(_map[row, column] != null && _map[row - 1, column] == null)
                            {
                                _saveZoneMap[row, column].name = "SaveZoneRight";
                                _saveZoneMap[row, column].SetOneWay("right", Random.Range(0, 2) == 1);
                            }
                        }
                        else if(row == _saveZoneMap.GetLength(0) - 1)
                        {
                            _saveZoneMap[row, column].name = "SaveZoneLeft";
                            _saveZoneMap[row, column].SetOneWay("left", column == _branchs ? true : Random.Range(0, 2) == 1);
                        }
                        else if (row == 0)
                        {
                            _saveZoneMap[row, column].name = "SaveZoneRight";
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
                        _map[row, column].name = $"Arena {column} | {row}";
                        _map[row, column] = Instantiate(_map[row, column], new Vector3(_arena.GetLengthX() / 2f + _saveZone.GetLengthX() / 2f + _arena.GetLengthX() * row + _saveZone.GetLengthX() * row, 0f, _arena.GetLengthZ() * column), Quaternion.identity);
                    }
                }
                for(int row = 0; row < _saveZoneMap.GetLength(0); row++)
                {
                    if(_saveZoneMap[row, column] != null)
                    {      
                        _saveZoneMap[row, column].name = $"Save Zone {column} | {row}";
                        _saveZoneMap[row, column] = Instantiate(_saveZoneMap[row, column], new Vector3(_arena.GetLengthX() * row + _saveZone.GetLengthX() * row, 0, _saveZone.GetLengthZ() * column), Quaternion.identity);
                    }
                }
            }
        }
    }
}