using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Menu.SelectionClass;
using Map.Expirience;
using Map.Coins;
using Map.Data;
using Map.Teleport;
using GamePlay.Character.Spell;

namespace Map
{
    public sealed class MapGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject _emptyWall;
        [SerializeField] private StarController _star;
        [SerializeField] private CoinControl _coin;
        private RoomParameters[,] _roomMap;
        private SaveZoneParameters[,] _safeZoneMap;
        private GameObject _selectCharacter;
        private GameObject _emptyWallStart;
        private GameObject _emptyWallEnd;
        private Vector2Int _lastRoomPosition;
        private Vector2Int _safeZonePlayerWasOrIs;
        private Vector2Int _roomPlayerWasOrIs;
        public static event Action<ManaController> HandOverManaController;

        private void OnEnable()
        {
            RoomParameters.OnEnterRoom += MoveMapArea;
            SaveZoneParameters.OnEnterSafeZone += SetLocationOfNewSafeZone;
            GeneralParameters.LoadedGeneralParameters += Generate;
            GamePlay.Character.HealthController.OnBackToSafeZone += GoToSafeZone;
            TeleportController.OnTeleport += GoToCentralRoom;
            TeleportControl.OnGetCentralSafeZone += GetSafeZoneParameters;
        }

        private void OnDisable()
        {
            RoomParameters.OnEnterRoom -= MoveMapArea;
            SaveZoneParameters.OnEnterSafeZone -= SetLocationOfNewSafeZone;
            GeneralParameters.LoadedGeneralParameters -= Generate;
            GamePlay.Character.HealthController.OnBackToSafeZone -= GoToSafeZone;
            TeleportController.OnTeleport -= GoToCentralRoom;
            TeleportControl.OnGetCentralSafeZone -= GetSafeZoneParameters;
        }
        public void GoToCentralRoom()
        {
            _selectCharacter.transform.position = new Vector3
            (
                GeneralParameters.LongRoom.GetLengthX() * _safeZonePlayerWasOrIs.y + GeneralParameters.ShortRoom.GetLengthX() * _safeZonePlayerWasOrIs.y, 
                _selectCharacter.transform.position.y, 
                GeneralParameters.LongRoom.GetLengthZ() * GeneralParameters.LevelParameters.Branchs
            );
            ShowMapAroundPayer(new Vector2Int(GeneralParameters.LevelParameters.Branchs, _safeZonePlayerWasOrIs.y));
        }
        public void GoToSafeZone()
        {
            _selectCharacter.transform.position = new Vector3
            (
                GeneralParameters.LongRoom.GetLengthX() * _safeZonePlayerWasOrIs.y + GeneralParameters.ShortRoom.GetLengthX() * _safeZonePlayerWasOrIs.y,
                1f,
                GeneralParameters.LongRoom.GetLengthZ() * _safeZonePlayerWasOrIs.x
            );
        }
        private void Generate()
        {
            _lastRoomPosition = new Vector2Int(GeneralParameters.LevelParameters.Branchs, -1);

            GenerateOfPrimaryView();
            GenerateOfSaveZone();

            SpawnAllMap();
            ShowMapAroundPayer(new Vector2Int(GeneralParameters.LevelParameters.Branchs, 0));

            SetExitSaveZone();
            SetExitInRoom();

            _selectCharacter = Instantiate(GetSelectedCharacter(),
                new Vector3(0, 2, GeneralParameters.LongRoom.GetLengthZ() * GeneralParameters.LevelParameters.Branchs),
                Quaternion.identity);
            GenerateAndSpawnStars();

            if (_selectCharacter.GetComponent<ManaController>() != null)
            {
                HandOverManaController(_selectCharacter.GetComponent<ManaController>());
            }
        }
        private GameObject GetSelectedCharacter()
        {
            return GeneralParameters.CharacterList[SelectionClassView.CharacterType];
        }
        private void SetLocationOfNewSafeZone(Vector2Int playerPosition)
        {
            _safeZonePlayerWasOrIs = playerPosition;
        }
        private void SetLocationOfNeRoom(Vector2Int roomPosition)
        {
            _roomPlayerWasOrIs = roomPosition;
        }
        private SaveZoneParameters GetSafeZoneParameters()
        {
            return _safeZoneMap[_safeZonePlayerWasOrIs.y, GeneralParameters.LevelParameters.Branchs];
        }
        
        private void GenerateOfPrimaryView()
        {
            _roomMap = new RoomParameters[GeneralParameters.LevelParameters.Length,
                GeneralParameters.LevelParameters.Branchs * 2 + 1];
            int[] quantityBranchsInChank = new int[GeneralParameters.LevelParameters.Branchs];
            int[] quantityBranchs = new int[GeneralParameters.LevelParameters.Branchs];

            for (int row = 0; row < _roomMap.GetLength(0); row++)
            {
                if (row % GeneralParameters.LevelParameters.SizeChank == 0)
                {
                    for (int i = 0; i < GeneralParameters.LevelParameters.Branchs; i++)
                    {
                        quantityBranchsInChank[i] = (GeneralParameters.LevelParameters.Difficulty *
                                                     ((row / GeneralParameters.LevelParameters.SizeChank) + 1)) /
                                                    (i + 1);
                        quantityBranchs[i] = 0;
                    }
                }

                for (int column = 0; column < _roomMap.GetLength(1); column++)
                {
                    if (column == GeneralParameters.LevelParameters.Branchs)
                    {
                        _roomMap[row, column] = GeneralParameters.LongRoom;
                    }
                    else
                    {
                        int indexBranch = column > GeneralParameters.LevelParameters.Branchs
                            ? column - GeneralParameters.LevelParameters.Branchs - 1
                            : GeneralParameters.LevelParameters.Branchs - column - 1;
                        // *** Налаштувати quantityBranchs зараз розподіл працює не коректно
                        if ((row > 0 && _roomMap[row - 1, column] != null) ||
                            column - 1 == GeneralParameters.LevelParameters.Branchs ||
                            column + 1 == GeneralParameters.LevelParameters.Branchs)
                        {
                            if (quantityBranchs[indexBranch] < quantityBranchsInChank[indexBranch])
                            {
                                if (UnityEngine.Random.Range(0, 2) == 1)
                                {
                                    _roomMap[row, column] = GeneralParameters.LongRoom;
                                    quantityBranchs[indexBranch]++;
                                }
                                else if (UnityEngine.Random.Range(0, 2) == 1)
                                {
                                    if (column > GeneralParameters.LevelParameters.Branchs)
                                    {
                                        _roomMap[row, column] = GeneralParameters.LongRoom;
                                        _roomMap[row, column - 1] = GeneralParameters.LongRoom;
                                        quantityBranchs[indexBranch]++;
                                    }
                                    else if (column < GeneralParameters.LevelParameters.Branchs)
                                    {
                                        _roomMap[row, column] = GeneralParameters.LongRoom;
                                        _roomMap[row, column + 1] = GeneralParameters.LongRoom;
                                        quantityBranchs[indexBranch]++;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (row > 0 && column > 0 && _roomMap[row - 1, column - 1] != null)
                            {
                                if (quantityBranchs[indexBranch] < quantityBranchsInChank[indexBranch])
                                {
                                    if (UnityEngine.Random.Range(0, 3) == 1)
                                    {
                                        _roomMap[row, column] = GeneralParameters.LongRoom;
                                        _roomMap[row, column - 1] = GeneralParameters.LongRoom;
                                        quantityBranchs[indexBranch]++;
                                    }
                                }
                            }
                            else if (row > 0 && column < _roomMap.GetLength(1) - 1 && _roomMap[row - 1, column + 1] != null)
                            {
                                if (quantityBranchs[indexBranch] < quantityBranchsInChank[indexBranch])
                                {
                                    if (UnityEngine.Random.Range(0, 3) == 1)
                                    {
                                        _roomMap[row, column] = GeneralParameters.LongRoom;
                                        _roomMap[row, column + 1] = GeneralParameters.LongRoom;
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
            _safeZoneMap = new SaveZoneParameters[GeneralParameters.LevelParameters.Length + 1,
                GeneralParameters.LevelParameters.Branchs * 2 + 1];

            _safeZoneMap[0, GeneralParameters.LevelParameters.Branchs] = GeneralParameters.ShortRoom;

            for (int row = 0; row < _roomMap.GetLength(0); row++)
            {
                for (int column = 0; column < _roomMap.GetLength(1); column++)
                {
                    if (row + 1 < _roomMap.GetLength(0) && _roomMap[row + 1, column] != null && _roomMap[row, column] != null)
                    {
                        _safeZoneMap[row + 1, column] = GeneralParameters.ShortRoom;
                    }
                    else if (_roomMap[row, column] != null && UnityEngine.Random.Range(0, 2) == 1)
                    {
                        _safeZoneMap[row + 1, column] = GeneralParameters.ShortRoom;
                    }

                    if (0 < row && _roomMap[row - 1, column] != null && _roomMap[row, column] != null)
                    {
                        _safeZoneMap[row, column] = GeneralParameters.ShortRoom;
                    }
                    else if (_roomMap[row, column] != null && UnityEngine.Random.Range(0, 2) == 1)
                    {
                        _safeZoneMap[row, column] = GeneralParameters.ShortRoom;
                    }
                }
            }

            _safeZoneMap[_safeZoneMap.GetLength(0) - 1, GeneralParameters.LevelParameters.Branchs] =
                GeneralParameters.ShortRoom;
        }
        
        private void SetExitInRoom()
        {
            for (int column = 0; column < _roomMap.GetLength(1); column++)
            {
                for (int row = 0; row < _roomMap.GetLength(0); row++)
                {
                    if (_roomMap[row, column] != null)
                    {
                        _roomMap[row, column].name = $"Arena {column} | {row}";
                        _roomMap[row, column].SetDoorParameters
                        (
                            (column + 1 < _roomMap.GetLength(1) && _roomMap[row, column + 1] == null) ||
                            column + 1 == _roomMap.GetLength(1),
                            (row + 1 < _roomMap.GetLength(0) && _roomMap[row + 1, column] == null &&
                             _safeZoneMap[row + 1, column] == null) ||
                            (row + 1 == _roomMap.GetLength(0) && _safeZoneMap[row + 1, column] == null),
                            (0 < column && _roomMap[row, column - 1] == null) || column == 0,
                            (row > 0 && _roomMap[row - 1, column] == null && _safeZoneMap[row, column] == null) ||
                            (row == 0 && _safeZoneMap[row, column] == null)
                        );
                    }
                }
            }
        }
        private void SetExitSaveZone()
        {
            for (int column = 0; column < _safeZoneMap.GetLength(1); column++)
            {
                for (int row = 0; row < _safeZoneMap.GetLength(0); row++)
                {
                    if (_safeZoneMap[row, column] != null)
                    {
                        _safeZoneMap[row, column].name = $"SafeZoneRoom {column} | {row}";
                        if (0 < row && row < _roomMap.GetLength(0))
                        {
                            if (_roomMap[row, column] != null && _roomMap[row - 1, column] != null)
                            {
                                _safeZoneMap[row, column].SetHorizontalParameters(_safeZoneMap[row - 1, column] != null && _safeZoneMap[row - 1, column].IsSaveZone ? UnityEngine.Random.Range(0, 1) == 0 : true);
                            }
                            else if (_roomMap[row, column] == null && _roomMap[row - 1, column] != null)
                            {
                                _safeZoneMap[row, column].SetOneWay((short)OpenSide.Left, UnityEngine.Random.Range(0, 1) == 0);
                            }
                            else if (_roomMap[row, column] != null && _roomMap[row - 1, column] == null)
                            {
                                _safeZoneMap[row, column].SetOneWay((short)OpenSide.Right, UnityEngine.Random.Range(0, 1) == 0);
                            }
                        }
                        else if (row == _safeZoneMap.GetLength(0) - 1)
                        {
                            _safeZoneMap[row, column].SetOneWay((short)OpenSide.Left,
                                column == GeneralParameters.LevelParameters.Branchs ? true : UnityEngine.Random.Range(0, 1) == 0);
                        }
                        else if (row == 0)
                        {
                            _safeZoneMap[row, column].SetOneWay((short)OpenSide.Right,
                                column == GeneralParameters.LevelParameters.Branchs ? true : UnityEngine.Random.Range(0, 1) == 0);
                        }

                        if (!(column == GeneralParameters.LevelParameters.Branchs && row == 0) &&
                            !(column == GeneralParameters.LevelParameters.Branchs &&
                              row == _safeZoneMap.GetLength(0) - 1))
                        {
                            _safeZoneMap[row, column].gameObject.AddComponent<ExpirienceControl>();
                        }

                        if (column != GeneralParameters.LevelParameters.Branchs && _safeZoneMap[row, column].IsSaveZone)
                        {
                            _safeZoneMap[row, column].gameObject.AddComponent<TeleportControl>();
                        }
                    }
                }
            }
            _safeZoneMap[_safeZoneMap.GetLength(0) - 1, GeneralParameters.LevelParameters.Branchs].gameObject
                .AddComponent<LevelComplited>();
        }
        
        private void SpawnAllMap()
        {
            for (int column = 0; column < _roomMap.GetLength(1); column++)
            {
                for (int row = 0; row < _roomMap.GetLength(0); row++)
                {
                    if (_roomMap[row, column] != null)
                    {
                        _roomMap[row, column] = Instantiate(_roomMap[row, column], new Vector3
                        (
                            GeneralParameters.LongRoom.GetLengthX() / 2f + GeneralParameters.ShortRoom.GetLengthX() / 2f + GeneralParameters.LongRoom.GetLengthX() * row + GeneralParameters.ShortRoom.GetLengthX() * row, 
                            0f,
                            GeneralParameters.LongRoom.GetLengthZ() * column
                        ), Quaternion.identity);
                        _roomMap[row, column].SetPosition(new Vector2Int(column, row));
                        _roomMap[row, column].gameObject.SetActive(false);
                    }
                }
            }
            for(int column = 0; column < _safeZoneMap.GetLength(1); column++)
            {
                for (int row = 0; row < _safeZoneMap.GetLength(0); row++)
                {
                    if (_safeZoneMap[row, column] != null)
                    {
                        _safeZoneMap[row, column] = Instantiate(_safeZoneMap[row, column],new Vector3
                        (
                            GeneralParameters.LongRoom.GetLengthX() * row + GeneralParameters.ShortRoom.GetLengthX() * row, 
                            0f,
                            GeneralParameters.ShortRoom.GetLengthZ() * column
                        ), Quaternion.identity);
                        _safeZoneMap[row, column].SetPosition(new Vector2Int(column, row));
                        _safeZoneMap[row, column].gameObject.SetActive(false);
                    }
                }
            }
            _emptyWallStart = Instantiate(_emptyWall, new Vector3
            (
                0 - (GeneralParameters.ShortRoom.GetLengthX() / 2f), 
                0f,
                GeneralParameters.LevelParameters.Branchs * GeneralParameters.LongRoom.GetLengthZ()
            ), Quaternion.identity);
            _emptyWallEnd = Instantiate(_emptyWall, new Vector3
            (
                0 - (GeneralParameters.ShortRoom.GetLengthX() / 2f), 
                0f,
                GeneralParameters.LevelParameters.Branchs * GeneralParameters.LongRoom.GetLengthZ()
            ), Quaternion.identity);
        }
        private void ChangePositionEmptyWall(Vector2Int position)
        {
            _emptyWallStart.transform.position = new Vector3
            (
                GeneralParameters.LongRoom.GetLengthX() * (position.y - 1 > 0 ? position.y - 1 : 0) + GeneralParameters.ShortRoom.GetLengthX() * (position.y - 1 > 0 ? position.y - 1 : 0) - GeneralParameters.ShortRoom.GetLengthX() / 2f - 0.5f,
                0f,
                GeneralParameters.LongRoom.GetLengthZ() * position.x
            );
            _emptyWallEnd.transform.position = new Vector3
            (
                GeneralParameters.LongRoom.GetLengthX() * (position.y + 2) + GeneralParameters.ShortRoom.GetLengthX() * (position.y + 2) + GeneralParameters.ShortRoom.GetLengthX() / 2f + 0.5f, 
                0f, 
                GeneralParameters.LongRoom.GetLengthZ() * position.x
            );
        }
        private void ShowMapAroundPayer(Vector2Int position)
        {
            foreach(RoomParameters room in _roomMap)
            {
                if(room != null)
                {
                    HideCoinsInRoom(room);
                    HideEnemiesInRoom(room);
                    room.gameObject.SetActive(false);
                }
            }
            foreach (SaveZoneParameters saveZone in _safeZoneMap)
            {
                if (saveZone != null)
                {
                    saveZone.gameObject.SetActive(false);
                }
            }
            for (int column = position.x - 1 > 0 ? position.x - 1 : 0; column <= (position.x + 1 < _roomMap.GetLength(1) - 1 ? position.x + 1 : _roomMap.GetLength(1) - 1); column++)
            {
                for (int row = position.y - 1 > 0 ? position.y - 1 : 0; row <= (position.y + 1 < _roomMap.GetLength(0) - 1 ? position.y + 1 : _roomMap.GetLength(0) - 1); row++)
                {
                    if (_roomMap[row, column] != null)
                    {
                        _roomMap[row, column].gameObject.SetActive(true);
                        ShowEnemiesInRoom(_roomMap[row, column]);
                        ShowCoinListInRoom(_roomMap[row, column]);
                    }
                }
                
                for (int row = position.y - 1 > 0 ? position.y - 1 : 0; row <= (position.y + 2 < _safeZoneMap.GetLength(0) - 1 ? position.y + 2 : _safeZoneMap.GetLength(0) - 1); row++)
                {
                    if (_safeZoneMap[row, column] != null)
                    {
                        _safeZoneMap[row, column].gameObject.SetActive(true);
                    }
                }
            }
            ChangePositionEmptyWall(new Vector2Int(position.x, position.y));
            _lastRoomPosition = new Vector2Int(position.x, position.y);
        }
        private void MoveMapArea(Vector2Int position)
        {
            if (_lastRoomPosition.x != position.x || _lastRoomPosition.y != position.y)
            {
                float coordinateDifferenceX = position.x - _lastRoomPosition.x;
                float coordinateDifferenceY = position.y - _lastRoomPosition.y;

                int coordStartX = 0;
                int coordStartY = 0;
                int coordEndX = 0;
                int coordEndY = 0;

                if (coordinateDifferenceX == 1)
                {
                    coordStartX = Convert.ToInt32(_lastRoomPosition.x - 1 > 0 ? _lastRoomPosition.x - 1 : 0);
                    coordStartY = Convert.ToInt32(_lastRoomPosition.y - 1 > 0 ? _lastRoomPosition.y - 1 : 0);

                    coordEndX = coordStartX + 1;
                    coordEndY = coordStartY + 3;
                }
                else if (coordinateDifferenceX == -1)
                {
                    coordStartX = Convert.ToInt32(_lastRoomPosition.x + 1 < _roomMap.GetLength(1) - 1 ? _lastRoomPosition.x + 1 : _roomMap.GetLength(1) - 1);
                    coordStartY = Convert.ToInt32(_lastRoomPosition.y - 1 > 0 ? _lastRoomPosition.y - 1 : 0);

                    coordEndX = coordStartX + 1;
                    coordEndY = coordStartY + 3;
                }
                else if (coordinateDifferenceY == 1)
                {
                    coordStartX = Convert.ToInt32(_lastRoomPosition.x - 1 > 0 ? _lastRoomPosition.x - 1 : 0);
                    coordStartY = Convert.ToInt32(_lastRoomPosition.y - 1 > 0 ? _lastRoomPosition.y - 1 : 0);

                    coordEndX = coordStartX + 3;
                    coordEndY = coordStartY + (position.y != 0 && position.y != 1 ? 1 : 0);
                }
                else if (coordinateDifferenceY == -1)
                {
                    coordStartX = Convert.ToInt32(_lastRoomPosition.x - 1 > 0 ? _lastRoomPosition.x - 1 : 0);
                    coordStartY = Convert.ToInt32(_lastRoomPosition.y + 1 < _roomMap.GetLength(0) - 1 ? _lastRoomPosition.y + 1 : _roomMap.GetLength(0) - 1);

                    coordEndX = coordStartX + 3;
                    coordEndY = coordStartY + 1;
                }

                for (int column = coordStartX; column < coordEndX; column++)
                {
                    for (int row = coordStartY; row < coordEndY; row++)
                    {
                        if (_roomMap[row, column] != null)
                        {
                            HideCoinsInRoom(_roomMap[row, column]);
                            HideEnemiesInRoom(_roomMap[row, column]);
                            _roomMap[row, column].gameObject.SetActive(false);
                        }
                    }
                    for (int row = coordStartY + (coordinateDifferenceY == -1 ? 1 : 0); row < coordEndY + (coordinateDifferenceY != 1 ? 1 : 0); row++)
                    {
                        if (_safeZoneMap[row, column] != null)
                        {
                            _safeZoneMap[row, column].gameObject.SetActive(false);
                        }
                    }
                }

                if (coordinateDifferenceX == 1 && position.x != _roomMap.GetLength(0) - 1 && (position.y != 0 && position.y != _roomMap.GetLength(1) - 1))
                {
                    coordStartX = position.x + 1 < _roomMap.GetLength(1) - 1 ? position.x + 1 : _roomMap.GetLength(1) - 1;
                    coordStartY = position.y - 1 > 0 ? position.y - 1 : 0;

                    coordEndX = coordStartX + 1;
                    coordEndY = coordStartY + 3;
                }
                else if (coordinateDifferenceX == -1 && position.x != 0 && (position.y != 0 && position.y != _roomMap.GetLength(1) - 1))
                {
                    coordStartX = position.x - 1 > 0 ? position.x - 1 : 0;
                    coordStartY = position.y - 1 > 0 ? position.y - 1 : 0;

                    coordEndX = coordStartX + 1;
                    coordEndY = coordStartY + 3;
                }
                else if (coordinateDifferenceY == 1 && position.y != _roomMap.GetLength(1) - 1 && (position.x != 0 && position.x != _roomMap.GetLength(0) - 1))
                {
                    coordStartX = position.x - 1 > 0 ? position.x - 1 : 0;
                    coordStartY = position.y + 1 < _roomMap.GetLength(0) - 1 ? position.y + 1 : _roomMap.GetLength(0) - 1;

                    coordEndX = coordStartX + 3;
                    coordEndY = coordStartY + 1;
                }
                else if (coordinateDifferenceY == -1 && position.y != 0 && (position.x != 0 && position.x != _roomMap.GetLength(0) - 1))
                {
                    coordStartX = position.x - 1 > 0 ? position.x - 1 : 0;
                    coordStartY = position.y - 1 > 0 ? position.y - 1 : 0;

                    coordEndX = coordStartX + 3;
                    coordEndY = coordStartY + 1;
                }

                for (int column = coordStartX; column < coordEndX; column++)
                {
                    for (int row = coordStartY; row < coordEndY; row++)
                    {
                        if (_roomMap[row, column] != null)
                        {
                            _roomMap[row, column].gameObject.SetActive(true);
                            ShowEnemiesInRoom(_roomMap[row, column]);
                            ShowCoinListInRoom(_roomMap[row, column]);
                        }
                    }

                    for (int row = coordStartY; row < coordEndY + 1; row++)
                    {
                        if (_safeZoneMap[row, column] != null)
                        {
                            _safeZoneMap[row, column].gameObject.SetActive(true);
                        }
                    }
                }
                ChangePositionEmptyWall(position);
                _lastRoomPosition = position;
            }
        }
        
        private void GenerateAndSpawnStars()
        {
            bool isExit = false;
            if (!GeneralParameters.MainDataCollector.Level.LeftStars)
            {
                for (int column = 0; column < _safeZoneMap.GetLength(1); column++)
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
                            ), Quaternion.identity); //17---------------------------------------------------------
                            star.SetValueSide("left");
                            isExit = true;
                            break;
                        }
                    }
                    if (isExit)
                    {
                        break;
                    }
                }
                isExit = false;
            }

            if (!GeneralParameters.MainDataCollector.Level.RightStars)
            {
                for (int column = _safeZoneMap.GetLength(1) - 1; column > 0; column--)
                {
                    for (int row = _safeZoneMap.GetLength(0) - 1; row > 0; row--)
                    {
                        if (_safeZoneMap[row, column] != null)
                        {
                            StarController star = Instantiate(_star, new Vector3
                            (
                                _safeZoneMap[row, column].gameObject.transform.position.x,
                                _safeZoneMap[row, column].gameObject.transform.position.y,
                                _safeZoneMap[row, column].gameObject.transform.position.z
                            ), Quaternion.identity);
                            star.SetValueSide("right");
                            isExit = true;
                            break;
                        }
                    }
                    if (isExit)
                    {
                        break;
                    }
                }
            }
        }

        private void GenerateAndSpawnCoinList(RoomParameters room)
        {
            //спосіб підбірки і спавна, Який описаний нижче, на мою думку простіший в розумінні(принцип KISS) і при цьому код виконує менше операцій в порівнянно з попереднім варіантом 
            int maxCoinInRoom = UnityEngine.Random.Range(0, GeneralParameters.LevelParameters.MaxCoinInRoom);
            float indent = 3f;
            room.CoinList = new List<CoinControl>(maxCoinInRoom);
            for (int i = 0; i < maxCoinInRoom; i++)
            {
                room.CoinList.Add(Instantiate(_coin,
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
        }
        private void ShowCoinListInRoom(RoomParameters room)
        {
            //якщо монети ще не згенеровані, то згенерувати їх
            // це зроблено для оптимізації, по-перше загрузка карти відбувається швидше, по-друге менше памяті займає карта, тому що є вірогідність, що гравець не побуває в кожній кімнаті, але при цьому ресурси на генерацію цих місць були би витрачені
            if (room.CoinList == null || room.CoinList.Count == 0)
            {
                GenerateAndSpawnCoinList(room);
            }
            else
            {
                foreach (CoinControl coin in room.CoinList)
                {
                    if (!coin.IsUse)
                    {
                        coin.gameObject.SetActive(true);
                    }
                }
            }
        }
        private void HideCoinsInRoom(RoomParameters room)
        {
            if (room.CoinList != null && room.CoinList.Count > 0)
            {
                foreach (CoinControl coin in room.CoinList)
                {
                    coin.gameObject.SetActive(false);
                }
            }
        }

        private void GenerateAndSpawnEmenyList(RoomParameters room)
        {
            //рахунок баллів для кімнати
            int score = GeneralParameters.LevelParameters.Difficulty * 2 + 4 + 1 / 4;
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
            foreach (Enemy enemy in GeneralParameters.EnemyList)
            {
                if (enemy.SpawnRate <= GeneralParameters.MainDataCollector.GetLevelNumber())
                {
                    if (enemy.SpawnRate == GeneralParameters.MainDataCollector.GetLevelNumber() && enemy.SpawnRate > 1)
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
            while (score > 0 && room.EnemyList.Count < GeneralParameters.LevelParameters.MaxEnemiesInRoom)
            {
                int index = UnityEngine.Random.Range(0, selectionEnemies.Count);
                if (selectionEnemies[index].ScoreRate <= score)
                {
                    //перевіряю через імена, тому що при зрівненні геймобджектів видає fаlse, припускаю, що це взязанно з тим, що префаб в процессі роботи змінює свої параметри і таким чином не співпадає
                    if (selectionEnemies[index].name == GeneralParameters.IndestructibleEnemy.name && indestructibleEnemyCounter < room.GetLengthX() / 2)
                    {
                        int indexCoordX = UnityEngine.Random.Range(0, optionCoordX.Count);
                        int indexCoordZ = UnityEngine.Random.Range(0, optionCoordZ.Count);
                        room.EnemyList.Add(Instantiate(selectionEnemies[index].EnemyObj,
                        new Vector3
                        (
                            room.gameObject.transform.position.x + optionCoordX[indexCoordX],
                            1,
                            room.gameObject.transform.position.z + optionCoordZ[indexCoordZ]
                        ), Quaternion.identity));
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
        }
        private void ShowEnemiesInRoom(RoomParameters room)
        {
            //якщо противикі ще не згенеровані, то згенерувати їх
            // це зроблено для оптимізації, по-перше загрузка карти відбувається швидше, по-друге менше памяті займає карта, тому що є вірогідність, що гравець не побуває в кожній кімнаті, але при цьому ресурси на генерацію цих місць були би витрачені
            if (room.EnemyList == null || room.EnemyList.Count == 0)
            {
                GenerateAndSpawnEmenyList(room);
            }
            else
            {
                foreach (GameObject enemy in room.EnemyList)
                {
                    enemy.SetActive(true);
                }
            }
        }
        private void HideEnemiesInRoom(RoomParameters room)
        {
            if (room.EnemyList != null && room.EnemyList.Count > 0)
            {
                foreach (GameObject enemy in room.EnemyList)
                {
                    enemy.SetActive(false);
                }
            }
        }
    }
}