using System;
using System.Collections.Generic;
using UnityEngine;
using Map.Expirience;
using Map.Coins;
using Map.Data;
using Map.Teleport;
using GamePlay.Character.Spell;

namespace Map
{
    public sealed class MapGenerator : MonoBehaviour
    {
        [SerializeField] private RoomParameters _longRoom;
        [SerializeField] private SafeZoneParameters _shortRoom;
        [SerializeField] private GameObject _emptyWall;
        private LevelParameters _levelParameters;
        private RoomParameters [,] _mapOfRooms;
        private SafeZoneParameters [,] _mapOfSafeZones;
        private GameObject _emptyWallStart;
        private GameObject _emptyWallEnd;
        
        public void Start()
        {
            _levelParameters = MapManager.LevelParameters;
            GenerateOfPrimaryView();
            GenerateOfSaveZone();
            InstantiatetAllMap();
            SetExitSaveZone();
            SetExitInRoom();  
        }
        public ref RoomParameters [,] GetMapOfRooms() => ref _mapOfRooms;
        public ref SafeZoneParameters [,] GetMapOfSafeZones() => ref _mapOfSafeZones;
        public RoomParameters GetLongRoom() => _longRoom;
        public SafeZoneParameters GetShortRoom() => _shortRoom;
        public void ChangePositionEmptyWall(Vector2Int position)
        {
            _emptyWallStart.transform.position = new Vector3
            (
                _longRoom.GetLengthX() * (position.y - 1 > 0 ? position.y - 1 : 0) + _shortRoom.GetLengthX() * (position.y - 1 > 0 ? position.y - 1 : 0) - _shortRoom.GetLengthX() / 2f - 0.5f,
                0f,
                _longRoom.GetLengthZ() * position.x
            );
            _emptyWallEnd.transform.position = new Vector3
            (
                _longRoom.GetLengthX() * (position.y + 2) + _shortRoom.GetLengthX() * (position.y + 2) + _shortRoom.GetLengthX() / 2f + 0.5f,
                0f,
                _longRoom.GetLengthZ() * position.x
            );
        }
        private void InstantiatetAllMap()
        {
            for (int column = 0; column < _mapOfRooms.GetLength(1); column++)
            {
                for (int row = 0; row < _mapOfRooms.GetLength(0); row++)
                {
                    if (_mapOfRooms[row, column] != null)
                    {
                        _mapOfRooms[row, column] = Instantiate(_mapOfRooms[row, column], new Vector3
                        (
                            _longRoom.GetLengthX() / 2f + _shortRoom.GetLengthX() / 2f + _longRoom.GetLengthX() * row + _shortRoom.GetLengthX() * row,
                            0f,
                            _longRoom.GetLengthZ() * column
                        ), Quaternion.identity);
                        _mapOfRooms[row, column].SetPosition(new Vector2Int(column, row));
                        _mapOfRooms[row, column].gameObject.SetActive(false);
                    }
                }
            }
            for (int column = 0; column < _mapOfSafeZones.GetLength(1); column++)
            {
                for (int row = 0; row < _mapOfSafeZones.GetLength(0); row++)
                {
                    if (_mapOfSafeZones[row, column] != null)
                    {
                        _mapOfSafeZones[row, column] = Instantiate(_mapOfSafeZones[row, column], new Vector3
                        (
                            _longRoom.GetLengthX() * row + _shortRoom.GetLengthX() * row,
                            0f,
                            _shortRoom.GetLengthZ() * column
                        ), Quaternion.identity);
                        _mapOfSafeZones[row, column].SetPosition(new Vector2Int(column, row));
                        _mapOfSafeZones[row, column].gameObject.SetActive(false);
                    }
                }
            }
            _emptyWallStart = Instantiate(_emptyWall, new Vector3
            (
                0 - (_shortRoom.GetLengthX() / 2f),
                0f,
                _levelParameters.Branchs * _longRoom.GetLengthZ()
            ), Quaternion.identity);
            _emptyWallEnd = Instantiate(_emptyWall, new Vector3
            (
                0 - (_shortRoom.GetLengthX() / 2f),
                0f,
                _levelParameters.Branchs * _longRoom.GetLengthZ()
            ), Quaternion.identity);
        }
        private void GenerateOfPrimaryView()
        {
            _mapOfRooms = new RoomParameters[_levelParameters.Length, _levelParameters.Branchs * 2 + 1];
            int[] quantityBranchsInChank = new int[_levelParameters.Branchs];
            int[] quantityBranchs = new int[_levelParameters.Branchs];

            for (int row = 0; row < _mapOfRooms.GetLength(0); row++)
            {
                if (row % _levelParameters.SizeChank == 0)
                {
                    for (int i = 0; i < _levelParameters.Branchs; i++)
                    {
                        quantityBranchsInChank[i] = (_levelParameters.Difficulty *
                                                     ((row / _levelParameters.SizeChank) + 1)) /
                                                    (i + 1);
                        quantityBranchs[i] = 0;
                    }
                }

                for (int column = 0; column < _mapOfRooms.GetLength(1); column++)
                {
                    if (column == _levelParameters.Branchs)
                    {
                        _mapOfRooms[row, column] = _longRoom;
                    }
                    else
                    {
                        int indexBranch = column > _levelParameters.Branchs
                            ? column - _levelParameters.Branchs - 1
                            : _levelParameters.Branchs - column - 1;
                        if ((row > 0 && _mapOfRooms[row - 1, column] != null) ||
                            column - 1 == _levelParameters.Branchs ||
                            column + 1 == _levelParameters.Branchs)
                        {
                            if (quantityBranchs[indexBranch] < quantityBranchsInChank[indexBranch])
                            {
                                if (UnityEngine.Random.Range(0, 2) == 1)
                                {
                                    _mapOfRooms[row, column] = _longRoom;
                                    quantityBranchs[indexBranch]++;
                                }
                                else if (UnityEngine.Random.Range(0, 2) == 1)
                                {
                                    if (column > _levelParameters.Branchs)
                                    {
                                        _mapOfRooms[row, column] = _longRoom;
                                        _mapOfRooms[row, column - 1] = _longRoom;
                                        quantityBranchs[indexBranch]++;
                                    }
                                    else if (column < _levelParameters.Branchs)
                                    {
                                        _mapOfRooms[row, column] = _longRoom;
                                        _mapOfRooms[row, column + 1] = _longRoom;
                                        quantityBranchs[indexBranch]++;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (row > 0 && column > 0 && _mapOfRooms[row - 1, column - 1] != null)
                            {
                                if (quantityBranchs[indexBranch] < quantityBranchsInChank[indexBranch])
                                {
                                    if (UnityEngine.Random.Range(0, 3) == 1)
                                    {
                                        _mapOfRooms[row, column] = _longRoom;
                                        _mapOfRooms[row, column - 1] = _longRoom;
                                        quantityBranchs[indexBranch]++;
                                    }
                                }
                            }
                            else if (row > 0 && column < _mapOfRooms.GetLength(1) - 1 && _mapOfRooms[row - 1, column + 1] != null)
                            {
                                if (quantityBranchs[indexBranch] < quantityBranchsInChank[indexBranch])
                                {
                                    if (UnityEngine.Random.Range(0, 3) == 1)
                                    {
                                        _mapOfRooms[row, column] = _longRoom;
                                        _mapOfRooms[row, column + 1] = _longRoom;
                                        quantityBranchs[indexBranch]++;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        private void GenerateOfSaveZone()
        {
            _mapOfSafeZones = new SafeZoneParameters[_levelParameters.Length + 1,
                _levelParameters.Branchs * 2 + 1];

            _mapOfSafeZones[0, _levelParameters.Branchs] = _shortRoom;

            for (int row = 0; row < _mapOfRooms.GetLength(0); row++)
            {
                for (int column = 0; column < _mapOfRooms.GetLength(1); column++)
                {
                    if (row + 1 < _mapOfRooms.GetLength(0) && _mapOfRooms[row + 1, column] != null && _mapOfRooms[row, column] != null)
                    {
                        _mapOfSafeZones[row + 1, column] = _shortRoom;
                    }
                    else if (_mapOfRooms[row, column] != null && UnityEngine.Random.Range(0, 2) == 1)
                    {
                        _mapOfSafeZones[row + 1, column] = _shortRoom;
                    }

                    if (0 < row && _mapOfRooms[row - 1, column] != null && _mapOfRooms[row, column] != null)
                    {
                        _mapOfSafeZones[row, column] = _shortRoom;
                    }
                    else if (_mapOfRooms[row, column] != null && UnityEngine.Random.Range(0, 2) == 1)
                    {
                        _mapOfSafeZones[row, column] = _shortRoom;
                    }
                }
            }

            _mapOfSafeZones[_mapOfSafeZones.GetLength(0) - 1, _levelParameters.Branchs] = _shortRoom;
        }
        private void SetExitInRoom()
        {
            for (int column = 0; column < _mapOfRooms.GetLength(1); column++)
            {
                for (int row = 0; row < _mapOfRooms.GetLength(0); row++)
                {
                    if (_mapOfRooms[row, column] != null)
                    {
                        _mapOfRooms[row, column].name = $"Arena {column} | {row}";
                        _mapOfRooms[row, column].SetDoorParameters
                        (
                            (column + 1 < _mapOfRooms.GetLength(1) && _mapOfRooms[row, column + 1] == null) ||
                            column + 1 == _mapOfRooms.GetLength(1),
                            (row + 1 < _mapOfRooms.GetLength(0) && _mapOfRooms[row + 1, column] == null &&
                             _mapOfSafeZones[row + 1, column] == null) ||
                            (row + 1 == _mapOfRooms.GetLength(0) && _mapOfSafeZones[row + 1, column] == null),
                            (0 < column && _mapOfRooms[row, column - 1] == null) || column == 0,
                            (row > 0 && _mapOfRooms[row - 1, column] == null && _mapOfSafeZones[row, column] == null) ||
                            (row == 0 && _mapOfSafeZones[row, column] == null)
                        );
                    }
                }
            }
        }
        private void SetExitSaveZone()
        {
            for (int column = 0; column < _mapOfSafeZones.GetLength(1); column++)
            {
                for (int row = 0; row < _mapOfSafeZones.GetLength(0); row++)
                {
                    if (_mapOfSafeZones[row, column] != null)
                    {
                        _mapOfSafeZones[row, column].name = $"SafeZoneRoom {column} | {row}";
                        if (0 < row && row < _mapOfRooms.GetLength(0))
                        {
                            if (_mapOfRooms[row, column] != null && _mapOfRooms[row - 1, column] != null)
                            {
                                _mapOfSafeZones[row, column].SetHorizontalParameters(_mapOfSafeZones[row - 1, column] != null && _mapOfSafeZones[row - 1, column].IsSaveZone == true ? UnityEngine.Random.Range(0, _levelParameters.Difficulty / 5) == 0 : true);
                            }
                            else if (_mapOfRooms[row, column] == null && _mapOfRooms[row - 1, column] != null)
                            {
                                _mapOfSafeZones[row, column].SetOneWay((short)OpenSide.Left, UnityEngine.Random.Range(0, 1) == 0);
                            }
                            else if (_mapOfRooms[row, column] != null && _mapOfRooms[row - 1, column] == null)
                            {
                                _mapOfSafeZones[row, column].SetOneWay((short)OpenSide.Right, UnityEngine.Random.Range(0, 1) == 0);
                            }
                        }
                        else if (row == _mapOfSafeZones.GetLength(0) - 1)
                        {
                            _mapOfSafeZones[row, column].SetOneWay((short)OpenSide.Left,
                                column == _levelParameters.Branchs ? true : UnityEngine.Random.Range(0, 1) == 0);
                        }
                        else if (row == 0)
                        {
                            _mapOfSafeZones[row, column].SetOneWay((short)OpenSide.Right,
                                column == _levelParameters.Branchs ? true : UnityEngine.Random.Range(0, 1) == 0);
                        }

                        if (!(column == _levelParameters.Branchs && row == 0) &&
                            !(column == _levelParameters.Branchs &&
                              row == _mapOfSafeZones.GetLength(0) - 1))
                        {
                            _mapOfSafeZones[row, column].gameObject.AddComponent<ExpirienceControl>();
                        }

                        if (column != _levelParameters.Branchs && _mapOfSafeZones[row, column].IsSaveZone)
                        {
                            _mapOfSafeZones[row, column].gameObject.AddComponent<TeleportControl>();
                        }
                    }
                }
            }
            _mapOfSafeZones[_mapOfSafeZones.GetLength(0) - 1, _levelParameters.Branchs].gameObject
                .AddComponent<LevelComplited>();
        }
   }
}