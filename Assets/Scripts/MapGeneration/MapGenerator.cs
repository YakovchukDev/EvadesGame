// КОМЕНТАРІ ЯКІ ПОЗНАЧЕНІ "***", ЦЕ ДЛЯ СЕБЕ, ЩОБ НЕ ЗАБУТИ, ЩО САМЕ Я ХОТІВ ДОДАТИ, ЧИ ЗМІНИТИ
//код написан за 40 гривень, на качество були накладені санкції

using System.Collections.Generic;
using UnityEngine;
using Menu.SelectionClass;

namespace Map
{
    public class MapGenerator : MonoBehaviour
    {
        //Нужно соединить этот код с GeneralParameters
        [SerializeField] private List<GameObject> _justEnemies;
        [SerializeField] private List<GameObject> _midleEnemies;
        [SerializeField] private List<GameObject> _hardEnemies;
        [SerializeField] private GameObject _indestructibleEnemy;

        [SerializeField] private int _maxCoin;
        [SerializeField] private CoinControl _coin;

        private GameObject _character;
        private RoomParameters[,] _map;
        private SaveZoneParameters[,] _saveZoneMap;

        private GameObject[,,] _listEntities;
        private int lastcordX;
        private int lastcordY;
        private GameObject lastEmptyWallStart;
        private GameObject lastEmptyWallEnd;
        private readonly float[] _possibleSizes = {20f, 40f, 100f};
        private int _playerRoomX;
        private int _playerRoomY;

        private void OnEnable()
        {
            RoomParameters.EnterNewRoom += spawnMapAroundPayer;
            SaveZoneParameters.PlayerHere += GetWherePlayer;
            GeneralParameters.LoadedGeneralParameters += Generate;
        }

        private void OnDisable()
        {
            RoomParameters.EnterNewRoom -= spawnMapAroundPayer;
            SaveZoneParameters.PlayerHere -= GetWherePlayer;
            GeneralParameters.LoadedGeneralParameters -= Generate;
        }

        public void GoToCentralRoom()
        {
            _character.transform.position = new Vector3(_character.transform.localPosition.x,
                _character.transform.localPosition.y,
                GeneralParameters.LongRoom.GetLengthZ() * GeneralParameters.LevelParameters.Branchs);
            spawnOnPoint(GeneralParameters.LevelParameters.Branchs, _playerRoomY - 1);
        }

        private void Generate()
        {
            lastcordX = GeneralParameters.LevelParameters.Branchs;
            lastcordY = -1;

            generateOfPrimaryView();
            generateOfSaveZone();
            generationCoinList();

            _listEntities = new GameObject[_map.GetLength(0), _map.GetLength(1), 30];

            showAllMap();
            spawnOnPoint(GeneralParameters.LevelParameters.Branchs, 0);

            setExitSaveZone();
            setUpExitInRoom();

            _character = Instantiate(GeneralParameters.CharacterList[SelectionClassView.CharacterType],
                new Vector3(0, 2, GeneralParameters.LongRoom.GetLengthZ() * GeneralParameters.LevelParameters.Branchs),
                Quaternion.identity);
        }

        private void GetWherePlayer(int x, int y)
        {
            _playerRoomX = x;
            _playerRoomY = y;
        }

        //Створюю геометрію рівня(кількість і позицію кімнат)
        private void generateOfPrimaryView()
        {
            // *** Транспоновати матрицю, щоб перебір даних виглядів більш коерктно 
            _map = new RoomParameters[GeneralParameters.LevelParameters.Length,
                GeneralParameters.LevelParameters.Branchs * 2 + 1];
            int[] quantityBranchsInChank = new int[GeneralParameters.LevelParameters.Branchs];
            int[] quantityBranchs = new int[GeneralParameters.LevelParameters.Branchs];

            for (int row = 0; row < _map.GetLength(0); row++)
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

                for (int column = 0; column < _map.GetLength(1); column++)
                {
                    if (column == GeneralParameters.LevelParameters.Branchs)
                    {
                        _map[row, column] = GeneralParameters.LongRoom;
                    }
                    else
                    {
                        int indexBranch = column > GeneralParameters.LevelParameters.Branchs
                            ? column - GeneralParameters.LevelParameters.Branchs - 1
                            : GeneralParameters.LevelParameters.Branchs - column - 1;
                        // *** Налаштувати quantityBranchs зараз розподіл працює не коректно
                        if ((row > 0 && _map[row - 1, column] != null) ||
                            column - 1 == GeneralParameters.LevelParameters.Branchs ||
                            column + 1 == GeneralParameters.LevelParameters.Branchs)
                        {
                            if (quantityBranchs[indexBranch] < quantityBranchsInChank[indexBranch])
                            {
                                if (Random.Range(0, 2) == 1)
                                {
                                    _map[row, column] = GeneralParameters.LongRoom;
                                    quantityBranchs[indexBranch]++;
                                }
                                else if (Random.Range(0, 2) == 1)
                                {
                                    if (column > GeneralParameters.LevelParameters.Branchs)
                                    {
                                        _map[row, column] = GeneralParameters.LongRoom;
                                        _map[row, column - 1] = GeneralParameters.LongRoom;
                                        quantityBranchs[indexBranch]++;
                                    }
                                    else if (column < GeneralParameters.LevelParameters.Branchs)
                                    {
                                        _map[row, column] = GeneralParameters.LongRoom;
                                        _map[row, column + 1] = GeneralParameters.LongRoom;
                                        quantityBranchs[indexBranch]++;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (row > 0 && column > 0 && _map[row - 1, column - 1] != null)
                            {
                                if (quantityBranchs[indexBranch] < quantityBranchsInChank[indexBranch])
                                {
                                    if (Random.Range(0, 3) == 1)
                                    {
                                        _map[row, column] = GeneralParameters.LongRoom;
                                        _map[row, column - 1] = GeneralParameters.LongRoom;
                                        quantityBranchs[indexBranch]++;
                                    }
                                }
                            }
                            else if (row > 0 && column < _map.GetLength(1) - 1 && _map[row - 1, column + 1] != null)
                            {
                                if (quantityBranchs[indexBranch] < quantityBranchsInChank[indexBranch])
                                {
                                    if (Random.Range(0, 3) == 1)
                                    {
                                        _map[row, column] = GeneralParameters.LongRoom;
                                        _map[row, column + 1] = GeneralParameters.LongRoom;
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
            _saveZoneMap = new SaveZoneParameters[GeneralParameters.LevelParameters.Length + 1,
                GeneralParameters.LevelParameters.Branchs * 2 + 1];

            _saveZoneMap[0, GeneralParameters.LevelParameters.Branchs] = GeneralParameters.ShortRoom;

            for (int row = 0; row < _map.GetLength(0); row++)
            {
                for (int column = 0; column < _map.GetLength(1); column++)
                {
                    if (row + 1 < _map.GetLength(0) && _map[row + 1, column] != null && _map[row, column] != null)
                    {
                        _saveZoneMap[row + 1, column] = GeneralParameters.ShortRoom;
                    }
                    else if (_map[row, column] != null && Random.Range(0, 2) == 1)
                    {
                        _saveZoneMap[row + 1, column] = GeneralParameters.ShortRoom;
                    }

                    if (0 < row && _map[row - 1, column] != null && _map[row, column] != null)
                    {
                        _saveZoneMap[row, column] = GeneralParameters.ShortRoom;
                    }
                    else if (_map[row, column] != null && Random.Range(0, 2) == 1)
                    {
                        _saveZoneMap[row, column] = GeneralParameters.ShortRoom;
                    }
                }
            }

            _saveZoneMap[_saveZoneMap.GetLength(0) - 1, GeneralParameters.LevelParameters.Branchs] =
                GeneralParameters.ShortRoom;
        }

        private void setUpExitInRoom()
        {
            for (int column = 0; column < _map.GetLength(1); column++)
            {
                for (int row = 0; row < _map.GetLength(0); row++)
                {
                    if (_map[row, column] != null)
                    {
                        _map[row, column].name = $"Arena {column} | {row}";
                        _map[row, column].SetDoorParameters
                        (
                            (column + 1 < _map.GetLength(1) && _map[row, column + 1] == null) ||
                            column + 1 == _map.GetLength(1),
                            (row + 1 < _map.GetLength(0) && _map[row + 1, column] == null &&
                             _saveZoneMap[row + 1, column] == null) ||
                            (row + 1 == _map.GetLength(0) && _saveZoneMap[row + 1, column] == null),
                            (0 < column && _map[row, column - 1] == null) || column == 0,
                            (row > 0 && _map[row - 1, column] == null && _saveZoneMap[row, column] == null) ||
                            (row == 0 && _saveZoneMap[row, column] == null),
                            false,
                            false
                        );
                    }
                }
            }
        }

        private void setExitSaveZone()
        {
            for (int column = 0; column < _saveZoneMap.GetLength(1); column++)
            {
                for (int row = 0; row < _saveZoneMap.GetLength(0); row++)
                {
                    if (_saveZoneMap[row, column] != null)
                    {
                        if (0 < row && row < _map.GetLength(0))
                        {
                            if (_map[row, column] != null && _map[row - 1, column] != null)
                            {
                                _saveZoneMap[row, column].name = $"SaveZoneHorizontal {column} | {row}";
                                _saveZoneMap[row, column].SetHorizontalParameters(Random.Range(0, 2) == 1);
                            }
                            else if (_map[row, column] == null && _map[row - 1, column] != null)
                            {
                                _saveZoneMap[row, column].name = $"SaveZoneLeft {column} | {row}";
                                _saveZoneMap[row, column].SetOneWay("left", Random.Range(0, 2) == 1);
                            }
                            else if (_map[row, column] != null && _map[row - 1, column] == null)
                            {
                                _saveZoneMap[row, column].name = $"SaveZoneRight {column} | {row}";
                                _saveZoneMap[row, column].SetOneWay("right", Random.Range(0, 2) == 1);
                            }
                        }
                        else if (row == _saveZoneMap.GetLength(0) - 1)
                        {
                            _saveZoneMap[row, column].name = $"SaveZoneLeft {column} | {row}";
                            _saveZoneMap[row, column].SetOneWay("left",
                                column == GeneralParameters.LevelParameters.Branchs ? true : Random.Range(0, 2) == 1);
                        }
                        else if (row == 0)
                        {
                            _saveZoneMap[row, column].name = $"SaveZoneRight {column} | {row}";
                            _saveZoneMap[row, column].SetOneWay("right",
                                column == GeneralParameters.LevelParameters.Branchs ? true : Random.Range(0, 2) == 1);
                        }

                        if (!(column == GeneralParameters.LevelParameters.Branchs && row == 0) &&
                            !(column == GeneralParameters.LevelParameters.Branchs &&
                              row == _saveZoneMap.GetLength(0) - 1) && _saveZoneMap[row, column].IsSaveZone)
                        {
                            _saveZoneMap[row, column].gameObject.AddComponent<ExpirienceControl>();
                        }

                        if (column != GeneralParameters.LevelParameters.Branchs && _saveZoneMap[row, column].IsSaveZone)
                        {
                            _saveZoneMap[row, column].gameObject.AddComponent<TeleportControl>();
                        }
                    }
                }
            }
        }

        private void showAllMap()
        {
            for (int column = 0; column < _map.GetLength(1); column++)
            {
                for (int row = 0; row < _map.GetLength(0); row++)
                {
                    if (_map[row, column] != null)
                    {
                        _map[row, column] = Instantiate(_map[row, column],
                            new Vector3(
                                GeneralParameters.LongRoom.GetLengthX() / 2f +
                                GeneralParameters.ShortRoom.GetLengthX() / 2f +
                                GeneralParameters.LongRoom.GetLengthX() * row +
                                GeneralParameters.ShortRoom.GetLengthX() * row, 0f,
                                GeneralParameters.LongRoom.GetLengthZ() * column), Quaternion.identity);
                        _map[row, column].SetCordinatRoom(column, row);
                        _map[row, column].gameObject.SetActive(false);
                    }
                }

                for (int row = 0; row < _saveZoneMap.GetLength(0); row++)
                {
                    if (_saveZoneMap[row, column] != null)
                    {
                        _saveZoneMap[row, column] = Instantiate(_saveZoneMap[row, column],
                            new Vector3(
                                GeneralParameters.LongRoom.GetLengthX() * row +
                                GeneralParameters.ShortRoom.GetLengthX() * row, 0,
                                GeneralParameters.ShortRoom.GetLengthZ() * column), Quaternion.identity);
                        _saveZoneMap[row, column].SetCoordinate(column, row);
                        _saveZoneMap[row, column].gameObject.SetActive(false);
                    }
                }
            }

            _saveZoneMap[_saveZoneMap.GetLength(0) - 1, GeneralParameters.LevelParameters.Branchs].gameObject
                .AddComponent<LevelComplited>();
        }

        private void generationCoinList()
        {
            for (int column = 0; column < _map.GetLength(1); column++)
            {
                for (int row = 0; row < _map.GetLength(0); row++)
                {
                    if (_map[row, column] != null)
                    {
                        _map[row, column].CoinList = new List<CoinControl>(_maxCoin);
                        for (int i = 0; i < Random.Range(0, _maxCoin); i++)
                        {
                            float indent = 3f;
                            _map[row, column].CoinList.Add
                            (
                                Instantiate(_coin, new Vector3
                                (
                                    Random.Range
                                    (
                                        (indent + GeneralParameters.ShortRoom.GetLengthX() / 2f +
                                         GeneralParameters.LongRoom.GetLengthX() * row +
                                         GeneralParameters.ShortRoom.GetLengthX() * row),
                                        (GeneralParameters.LongRoom.GetLengthX() - indent +
                                         GeneralParameters.ShortRoom.GetLengthX() / 2f +
                                         GeneralParameters.LongRoom.GetLengthX() * row +
                                         GeneralParameters.ShortRoom.GetLengthX() * row)
                                    ),
                                    1f,
                                    Random.Range
                                    (
                                        GeneralParameters.LongRoom.GetLengthZ() * column -
                                        (GeneralParameters.LongRoom.GetLengthZ() / 2f + indent),
                                        GeneralParameters.LongRoom.GetLengthZ() * column +
                                        (GeneralParameters.LongRoom.GetLengthZ() / 2f - indent)
                                    )
                                ), Quaternion.identity)
                            );
                        }

                        foreach (CoinControl room in _map[row, column].CoinList)
                        {
                            //room.gameObject.SetActive(false);
                        }
                    }
                }
            }
        }

        private void spawnOnPoint(int x, int y)
        {
            for (int column = 0; column < _map.GetLength(1); column++)
            {
                for (int row = 0; row < _map.GetLength(0); row++)
                {
                    if (_map[row, column] != null)
                    {
                        foreach (CoinControl room in _map[row, column].CoinList)
                        {
                            room.gameObject.SetActive(false);
                        }

                        _map[row, column].gameObject.SetActive(false);
                        for (int height = 0; height < _listEntities.GetLength(2); height++)
                        {
                            if (_listEntities[row, column, height] != null)
                            {
                                Destroy(_listEntities[row, column, height]);
                                _listEntities[row, column, height] = null;
                            }
                        }
                    }
                }

                for (int row = 0; row < _saveZoneMap.GetLength(0); row++)
                {
                    if (_saveZoneMap[row, column] != null)
                    {
                        _saveZoneMap[row, column].gameObject.SetActive(false);
                    }
                }
            }

            for (int column = x - 1 > 0 ? x - 1 : 0;
                column <= (x + 1 < _map.GetLength(1) - 1 ? x + 1 : _map.GetLength(1) - 1);
                column++)
            {
                for (int row = y - 1 > 0 ? y - 1 : 0;
                    row <= (y + 1 < _map.GetLength(0) - 1 ? y + 1 : _map.GetLength(0) - 1);
                    row++)
                {
                    if (_map[row, column] != null)
                    {
                        _map[row, column].gameObject.SetActive(true);
                        spawnEnemiesAroundPayer(row, column);

                        foreach (CoinControl room in _map[row, column].CoinList)
                        {
                            if (!room.IsUse)
                            {
                                room.gameObject.SetActive(true);
                            }
                        }
                    }
                }

                for (int row = y - 1 > 0 ? y - 1 : 0;
                    row <= (y + 2 < _saveZoneMap.GetLength(0) - 1 ? y + 2 : _saveZoneMap.GetLength(0) - 1);
                    row++)
                {
                    if (_saveZoneMap[row, column] != null)
                    {
                        _saveZoneMap[row, column].gameObject.SetActive(true);
                    }
                }
            }

            if (lastEmptyWallEnd != null && lastEmptyWallStart != null)
            {
                lastEmptyWallStart.transform.position = new Vector3(
                    GeneralParameters.LongRoom.GetLengthX() * (y - 1 > 0 ? y - 1 : 0) +
                    GeneralParameters.ShortRoom.GetLengthX() * (y - 1 > 0 ? y - 1 : 0) -
                    GeneralParameters.ShortRoom.GetLengthX() / 2f - 0.5f, 0f,
                    GeneralParameters.LongRoom.GetLengthZ() * x);
                lastEmptyWallEnd.transform.position = new Vector3(
                    GeneralParameters.LongRoom.GetLengthX() * (y + 2) +
                    GeneralParameters.ShortRoom.GetLengthX() * (y + 2) + GeneralParameters.ShortRoom.GetLengthX() / 2f +
                    0.5f, 0f, GeneralParameters.LongRoom.GetLengthZ() * x);
            }
            else
            {
                lastEmptyWallStart = Instantiate(GeneralParameters.EmptyWall,
                    new Vector3(
                        GeneralParameters.LongRoom.GetLengthX() * (y - 2 > 0 ? y - 2 : 0) +
                        GeneralParameters.ShortRoom.GetLengthX() * (y - 2 > 0 ? y - 2 : 0) -
                        GeneralParameters.ShortRoom.GetLengthX() / 2f - 0.5f, 0f,
                        GeneralParameters.LongRoom.GetLengthZ() * x), Quaternion.identity);
                lastEmptyWallEnd = Instantiate(GeneralParameters.EmptyWall,
                    new Vector3(
                        GeneralParameters.LongRoom.GetLengthX() * (y + 2) +
                        GeneralParameters.ShortRoom.GetLengthX() * (y + 2) +
                        GeneralParameters.ShortRoom.GetLengthX() / 2f + 0.5f, 0f,
                        GeneralParameters.LongRoom.GetLengthZ() * x), Quaternion.identity);
            }

            lastcordX = x;
            lastcordY = y;
        }

        private void spawnMapAroundPayer(int x, int y)
        {
            if (lastcordX != x || lastcordY != y)
            {
                int coordinateDifferenceX = x - lastcordX;
                int coordinateDifferenceY = y - lastcordY;

                int coordStartX = 0;
                int coordStartY = 0;
                int coordEndX = 0;
                int coordEndY = 0;
                if (coordinateDifferenceX == 1)
                {
                    coordStartX = lastcordX - 1 > 0 ? lastcordX - 1 : 0;
                    coordStartY = lastcordY - 1 > 0 ? lastcordY - 1 : 0;

                    coordEndX = coordStartX + 1;
                    coordEndY = coordStartY + 3;
                }
                else if (coordinateDifferenceX == -1)
                {
                    coordStartX = lastcordX + 1 < _map.GetLength(1) - 1 ? lastcordX + 1 : _map.GetLength(1) - 1;
                    coordStartY = lastcordY - 1 > 0 ? lastcordY - 1 : 0;

                    coordEndX = coordStartX + 1;
                    coordEndY = coordStartY + 3;
                }
                else if (coordinateDifferenceY == 1)
                {
                    coordStartX = lastcordX - 1 > 0 ? lastcordX - 1 : 0;
                    coordStartY = lastcordY - 1 > 0 ? lastcordY - 1 : 0;

                    coordEndX = coordStartX + 3;
                    coordEndY = coordStartY + (y != 0 && y != 1 ? 1 : 0);
                }
                else if (coordinateDifferenceY == -1)
                {
                    coordStartX = lastcordX - 1 > 0 ? lastcordX - 1 : 0;
                    coordStartY = lastcordY + 1 < _map.GetLength(0) - 1 ? lastcordY + 1 : _map.GetLength(0) - 1;

                    coordEndX = coordStartX + 3;
                    coordEndY = coordStartY + 1;
                }

                for (int column = coordStartX; column < coordEndX; column++)
                {
                    for (int row = coordStartY; row < coordEndY; row++)
                    {
                        if (_map[row, column] != null)
                        {
                            foreach (CoinControl room in _map[row, column].CoinList)
                            {
                                room.gameObject.SetActive(false);
                            }

                            _map[row, column].gameObject.SetActive(false);
                            for (int height = 0; height < _listEntities.GetLength(2); height++)
                            {
                                if (_listEntities[row, column, height] != null)
                                {
                                    Destroy(_listEntities[row, column, height]);
                                    _listEntities[row, column, height] = null;
                                }
                            }
                        }
                    }

                    for (int row = coordStartY + (coordinateDifferenceY == -1 ? 1 : 0);
                        row < coordEndY + (coordinateDifferenceY != 1 ? 1 : 0);
                        row++)
                    {
                        if (_saveZoneMap[row, column] != null)
                        {
                            _saveZoneMap[row, column].gameObject.SetActive(false);
                        }
                    }
                }

                if (coordinateDifferenceX == 1)
                {
                    coordStartX = x + 1 < _map.GetLength(1) - 1 ? x + 1 : _map.GetLength(1) - 1;
                    coordStartY = y - 1 > 0 ? y - 1 : 0;

                    coordEndX = coordStartX + 1;
                    coordEndY = coordStartY + 3;
                }
                else if (coordinateDifferenceX == -1)
                {
                    coordStartX = x - 1 > 0 ? x - 1 : 0;
                    coordStartY = y - 1 > 0 ? y - 1 : 0;

                    coordEndX = coordStartX + 1;
                    coordEndY = coordStartY + 3;
                }
                else if (coordinateDifferenceY == 1)
                {
                    coordStartX = x - 1 > 0 ? x - 1 : 0;
                    coordStartY = y + 1 < _map.GetLength(0) - 1 ? y + 1 : _map.GetLength(0) - 1;

                    coordEndX = coordStartX + 3;
                    coordEndY = coordStartY + 1;
                }
                else if (coordinateDifferenceY == -1)
                {
                    coordStartX = x - 1 > 0 ? x - 1 : 0;
                    coordStartY = y - 1 > 0 ? y - 1 : 0;

                    coordEndX = coordStartX + 3;
                    coordEndY = coordStartY + 1;
                }

                for (int column = coordStartX; column < coordEndX; column++)
                {
                    for (int row = coordStartY; row < coordEndY; row++)
                    {
                        if (_map[row, column] != null)
                        {
                            _map[row, column].gameObject.SetActive(true);
                            spawnEnemiesAroundPayer(row, column);

                            foreach (CoinControl room in _map[row, column].CoinList)
                            {
                                if (!room.IsUse)
                                {
                                    room.gameObject.SetActive(true);
                                }
                            }
                        }
                    }

                    for (int row = coordStartY; row < coordEndY + 1; row++)
                    {
                        if (_saveZoneMap[row, column] != null)
                        {
                            _saveZoneMap[row, column].gameObject.SetActive(true);
                        }
                    }
                }

                lastEmptyWallStart.transform.position = new Vector3(
                    GeneralParameters.LongRoom.GetLengthX() * (y - 1 > 0 ? y - 1 : 0) +
                    GeneralParameters.ShortRoom.GetLengthX() * (y - 1 > 0 ? y - 1 : 0) -
                    GeneralParameters.ShortRoom.GetLengthX() / 2f - 0.5f, 0f,
                    GeneralParameters.LongRoom.GetLengthZ() * x);
                lastEmptyWallEnd.transform.position = new Vector3(
                    GeneralParameters.LongRoom.GetLengthX() * (y + 2) +
                    GeneralParameters.ShortRoom.GetLengthX() * (y + 2) + GeneralParameters.ShortRoom.GetLengthX() / 2f +
                    0.5f, 0f, GeneralParameters.LongRoom.GetLengthZ() * x);

                lastcordX = x;
                lastcordY = y;
            }
        }

        private void spawnStars()
        {
            bool isExit = false;
            if (!GeneralParameters.MainDataCollector.Level.LeftStars)
            {
                for (int column = 0; column < _saveZoneMap.GetLength(1); column++)
                {
                    for (int row = _saveZoneMap.GetLength(0) - 1; row > 0; row--)
                    {
                        if (_saveZoneMap[row, column] != null && _saveZoneMap[row, column].IsSaveZone)
                        {
                            StarController star = Instantiate(GeneralParameters.Star, new Vector3
                            (
                                _saveZoneMap[row, column].gameObject.transform.position.x,
                                _saveZoneMap[row, column].gameObject.transform.position.y,
                                _saveZoneMap[row, column].gameObject.transform.position.z
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
                for (int column = _saveZoneMap.GetLength(1) - 1; column > 0; column--)
                {
                    for (int row = _saveZoneMap.GetLength(0) - 1; row > 0; row--)
                    {
                        if (_saveZoneMap[row, column] != null)
                        {
                            StarController star = Instantiate(GeneralParameters.Star, new Vector3
                            (
                                _saveZoneMap[row, column].gameObject.transform.position.x,
                                _saveZoneMap[row, column].gameObject.transform.position.y,
                                _saveZoneMap[row, column].gameObject.transform.position.z
                            ), Quaternion.identity); //17-----------------------------------------------------------
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

        private void generateListEmeny(int x, int y)
        {
            int quantityIndestructibleEnemy = 4;
            int quantityJustEnemy = 2;
            int quantityMidleEnemy = 1;
            int quantityHardEnemy = 0;

            int beginingSum = 0;
            int endingSum = 0;

            endingSum += quantityIndestructibleEnemy < _listEntities.GetLength(2)
                ? quantityIndestructibleEnemy
                : _listEntities.GetLength(2) - 1;
            for (int index = beginingSum; index < endingSum; index++)
            {
                _listEntities[x, y, index] = _indestructibleEnemy;
            }

            beginingSum += quantityIndestructibleEnemy;

            endingSum += quantityJustEnemy < _listEntities.GetLength(2)
                ? quantityJustEnemy
                : _listEntities.GetLength(2) - 1;
            for (int index = beginingSum; index < endingSum; index++)
            {
                _listEntities[x, y, index] = _justEnemies[Random.Range(0, _justEnemies.Count)];
            }

            beginingSum += quantityJustEnemy;

            endingSum += quantityMidleEnemy < _listEntities.GetLength(2)
                ? quantityMidleEnemy
                : _listEntities.GetLength(2) - 1;
            for (int index = beginingSum; index < endingSum; index++)
            {
                _listEntities[x, y, index] = _midleEnemies[Random.Range(0, _midleEnemies.Count)];
            }

            beginingSum += quantityMidleEnemy;

            endingSum += quantityHardEnemy < _listEntities.GetLength(2)
                ? quantityHardEnemy
                : _listEntities.GetLength(2) - 1;
            for (int index = beginingSum; index < endingSum; index++)
            {
                _listEntities[x, y, index] = _hardEnemies[Random.Range(0, _hardEnemies.Count)];
            }
        }

        private void spawnEnemiesAroundPayer(int row, int column)
        {
            generateListEmeny(row, column);
            for (int height = 0; height < _listEntities.GetLength(2); height++)
            {
                if (_listEntities[row, column, height] != null)
                {
                    if (_listEntities[row, column, height] == _indestructibleEnemy)
                    {
                        _listEntities[row, column, height].gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
                        float[] listCordX = new float[2] {-25.5f, 25.5f};
                        float[] listCordY = new float[2] {-5f, 5f};
                        _listEntities[row, column, height] = Instantiate(_listEntities[row, column, height], new Vector3
                        (
                            (GeneralParameters.LongRoom.GetLengthX() / 2f +
                             GeneralParameters.ShortRoom.GetLengthX() / 2f +
                             GeneralParameters.LongRoom.GetLengthX() * row +
                             GeneralParameters.ShortRoom.GetLengthX() * row) + Random.Range(listCordX[0], listCordX[1]),
                            2f,
                            (GeneralParameters.LongRoom.GetLengthZ() * column) +
                            listCordY[Random.Range(0, listCordY.GetLength(0))]
                        ), Quaternion.Euler(-180, 0, 0));
                    }
                    else
                    {
                        float indent = 3f;
                        /////////////////////  
                        var enemySize = _possibleSizes[Random.Range(0, _possibleSizes.Length)];
                        _listEntities[row, column, height].gameObject.transform.localScale =
                            new Vector3(enemySize, enemySize, enemySize);
                        /////////////////////

                        //_listEntities[row, column, height].gameObject.transform.localScale = new Vector3(100f, 100f, 100f);  SASHA
                        _listEntities[row, column, height] = Instantiate(_listEntities[row, column, height], new Vector3
                        (
                            Random.Range
                            (
                                (indent + GeneralParameters.ShortRoom.GetLengthX() / 2f +
                                 GeneralParameters.LongRoom.GetLengthX() * row +
                                 GeneralParameters.ShortRoom.GetLengthX() * row),
                                (GeneralParameters.LongRoom.GetLengthX() - indent +
                                 GeneralParameters.ShortRoom.GetLengthX() / 2f +
                                 GeneralParameters.LongRoom.GetLengthX() * row +
                                 GeneralParameters.ShortRoom.GetLengthX() * row)
                            ),
                            2f,
                            Random.Range
                            (
                                GeneralParameters.LongRoom.GetLengthZ() * column -
                                (GeneralParameters.LongRoom.GetLengthZ() / 2f + indent),
                                GeneralParameters.LongRoom.GetLengthZ() * column +
                                (GeneralParameters.LongRoom.GetLengthZ() / 2f - indent)
                            )
                        ), Quaternion.identity);
                    }
                }
            }
        }
    }
}