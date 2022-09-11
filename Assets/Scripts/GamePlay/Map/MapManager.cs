using System;
using System.Collections;
using GamePlay.Camera;
using GamePlay.Character;
using Map.Coins;
using Map.Data;
using Map.Stars;
using UnityEngine;
using Map.Teleport;
using Menu.SelectionClass;
using TMPro;

namespace Map
{
    public class MapManager : MonoBehaviour
    {
        public static LevelParameters LevelParameters; //парамети, які потрібні для генерації карти
        public static MainDataCollector MainDataCollector; //зборщик, результатів на рівні
        [SerializeField] private CompanyCamera _companyCamera; //камера, яка слідкує за гравцем
        [SerializeField] private CoinController _coinController;
        [SerializeField] private GameObject _teleportThereParticle;
        [SerializeField] private GameObject _teleportHereParticle;
        [SerializeField] private GameObject _particleAfterStar;
        [SerializeField] private GameObject _audioListener2;
        private MapGenerator _mapGenerator;
        private CharacterSpawner _characterSpawner;
        private EntitiesGenerator _entitiesGenerator;
        private Vector2Int _lastRoomWherePlayerWas;
        private Vector2Int _safeZoneWherePlayerWasOrIs;
        private RoomParameters[,] _mapOfRooms;
        private SafeZoneParameters[,] _mapOfSafeZones;
        [SerializeField] private TMP_Text _startText;

        private void OnEnable()
        {
            RoomParameters.OnEnterRoom += MoveMapArea;
            HealthController.OnBackToSafeZone += GoToSafeZone;
            SafeZoneParameters.OnEnterSafeZone += SetLocationOfNewSafeZone;
            TeleportControl.OnGetCentralSafeZone += GetSafeZoneParameters;
            TeleportController.OnTeleport += StartTeleportationToSaveZone;
            StarController.OnParticleAfterStar += GetStar;
            StarController.OnUpdateStarAmount += StarAmount;
        }

        private void OnDisable()
        {
            RoomParameters.OnEnterRoom -= MoveMapArea;
            HealthController.OnBackToSafeZone -= GoToSafeZone;
            SafeZoneParameters.OnEnterSafeZone -= SetLocationOfNewSafeZone;
            TeleportControl.OnGetCentralSafeZone -= GetSafeZoneParameters;
            TeleportController.OnTeleport -= StartTeleportationToSaveZone;
            StarController.OnParticleAfterStar -= GetStar;
            StarController.OnUpdateStarAmount -= StarAmount;
        }


        private void Awake()
        {
            _mapGenerator = GetComponent<MapGenerator>();
            _mapGenerator.Initialize();
            _characterSpawner = GetComponent<CharacterSpawner>();
            _characterSpawner.StartPosition =
                new Vector3(0, 1, _mapGenerator.GetLongRoom().GetLengthZ() * LevelParameters.Branchs);
        }

        private void Start()
        {
            MainDataCollector = new MainDataCollector();
            MainDataCollector.SetCoinController(_coinController);

            _entitiesGenerator = GetComponent<EntitiesGenerator>();
            _mapOfRooms = _mapGenerator.GetMapOfRooms();
            _mapOfSafeZones = _mapGenerator.GetMapOfSafeZones();
            _entitiesGenerator.GenerateAndSpawnStars(_mapOfSafeZones);
            _companyCamera.SetPlayer(_characterSpawner.Character);
            ShowRoomsAroundSpecifiedCoordinates(new Vector2Int(LevelParameters.Branchs, 0));
            StarAmount();
        }

        private void StarAmount()
        {
            int star = (MainDataCollector.Level.DownStar ? 1 : 0) +
                       (MainDataCollector.Level.UpStar ? 1 : 0) +
                       (MainDataCollector.Level.MiddleStar ? 1 : 0);
            _startText.text = $"{star}/3";
        }


        public void GoToCentralRoom()
        {
            _entitiesGenerator.SetPlayerCoordinates(new Vector3
            (
                _mapGenerator.GetLongRoom().GetLengthX() * _safeZoneWherePlayerWasOrIs.y +
                _mapGenerator.GetShortRoom().GetLengthX() * _safeZoneWherePlayerWasOrIs.y,
                0,
                _mapGenerator.GetLongRoom().GetLengthZ() * LevelParameters.Branchs
            ));
            ShowRoomsAroundSpecifiedCoordinates(new Vector2Int(LevelParameters.Branchs, _safeZoneWherePlayerWasOrIs.y));
        }

        private void StartTeleportationToSaveZone()
        {
            StartCoroutine(TeleportToCentralRoom());
        }

        private IEnumerator TeleportToCentralRoom()
        {
            GameObject character = _characterSpawner.Character;
            _teleportThereParticle.transform.position = character.transform.position;
            _teleportThereParticle.SetActive(true);
            character.SetActive(false);
            _audioListener2.SetActive(true);
            yield return new WaitForSeconds(1);
            GoToCentralRoom();
            _teleportHereParticle.transform.position = character.transform.position;
            _teleportHereParticle.SetActive(true);
            yield return new WaitForSeconds(1);
            _audioListener2.SetActive(false);
            character.SetActive(true);
        }

        public static ref MainDataCollector GetMainDataCollector() => ref MainDataCollector;

        private void SetLocationOfNewSafeZone(Vector2Int playerPosition)
        {
            _safeZoneWherePlayerWasOrIs = playerPosition;
        }

        private SafeZoneParameters GetSafeZoneParameters() =>
            _mapOfSafeZones[_safeZoneWherePlayerWasOrIs.y, LevelParameters.Branchs];

        private void GoToSafeZone()
        {
            _entitiesGenerator.SetPlayerCoordinates(new Vector3
            (
                _mapGenerator.GetLongRoom().GetLengthX() * _safeZoneWherePlayerWasOrIs.y +
                _mapGenerator.GetShortRoom().GetLengthX() * _safeZoneWherePlayerWasOrIs.y,
                1f,
                _mapGenerator.GetLongRoom().GetLengthZ() * _safeZoneWherePlayerWasOrIs.x
            ));
            ShowRoomsAroundSpecifiedCoordinates(_safeZoneWherePlayerWasOrIs);
        }

        private void ShowRoomsAroundSpecifiedCoordinates(Vector2Int position)
        {
            SelectAllMap(false);
            Vector2Int startPos = SearchPosition(position, -1);
            Vector2Int endPos = SearchPosition(position, 1);
            SelectMapSpecifiedCoordinates(true, startPos, endPos);
            _mapGenerator.ChangePositionEmptyWall(position);
            _lastRoomWherePlayerWas = position;
        }

        private void MoveMapArea(Vector2Int position)
        {
            if (_lastRoomWherePlayerWas.x != position.x || _lastRoomWherePlayerWas.y != position.y)
            {
                Vector2Int startPos = SearchPosition(_lastRoomWherePlayerWas, -1);
                Vector2Int endPos = SearchPosition(_lastRoomWherePlayerWas, 1);
                SelectMapSpecifiedCoordinates(false, startPos, endPos);

                startPos = SearchPosition(position, -1);
                endPos = SearchPosition(position, 1);
                SelectMapSpecifiedCoordinates(true, startPos, endPos);

                _mapGenerator.ChangePositionEmptyWall(position);
                _lastRoomWherePlayerWas = position;
            }
        }

        private Vector2Int SearchPosition(Vector2Int position, int step)
        {
            Vector2Int newPosition = new Vector2Int(position.x + step, position.y + step);
            if (newPosition.x < 0)
                newPosition.x = 0;
            else if (newPosition.x > _mapOfRooms.GetLength(1) - 1)
                newPosition.x = _mapOfRooms.GetLength(1) - 1;
            if (newPosition.y < 0)
                newPosition.y = 0;
            else if (newPosition.y > _mapOfRooms.GetLength(0) - 1)
                newPosition.y = _mapOfRooms.GetLength(0) - 1;
            return newPosition;
        }

        private void SelectMapSpecifiedCoordinates(bool isShow, Vector2Int startPos, Vector2Int endPos)
        {
            for (int column = startPos.x;
                column <= endPos.x;
                column++)
            {
                for (int row = startPos.y; row <= endPos.y; row++)
                {
                    if (_mapOfRooms[row, column] != null)
                    {
                        if (isShow)
                        {
                            CheckIfEnteredRoom(_mapOfRooms[row, column]);
                        }

                        _mapOfRooms[row, column].gameObject.SetActive(isShow);
                    }
                }

                for (int row = startPos.y; row <= endPos.y + 1; row++)
                {
                    if (_mapOfSafeZones[row, column] != null)
                    {
                        _mapOfSafeZones[row, column].gameObject.SetActive(isShow);
                    }
                }
            }
        }

        private void SelectAllMap(bool isShow)
        {
            foreach (RoomParameters room in _mapOfRooms)
            {
                if (room != null)
                {
                    if (isShow)
                    {
                        CheckIfEnteredRoom(_mapOfRooms[room.GetPosition().y, room.GetPosition().x]);
                    }

                    room.gameObject.SetActive(isShow);
                }
            }

            foreach (SafeZoneParameters saveZone in _mapOfSafeZones)
            {
                if (saveZone != null)
                {
                    saveZone.gameObject.SetActive(isShow);
                }
            }
        }

        private void CheckIfEnteredRoom(RoomParameters room)
        {
            if (room.IsFirstEntrance)
            {
                _entitiesGenerator.GenerateAndSpawnEmenyList(room);
                _entitiesGenerator.GenerateAndSpawnCoinList(room);
                room.IsFirstEntrance = false;
            }
        }

        private void GetStar(Vector3 starPosition)
        {
            _particleAfterStar.transform.position = starPosition;
            _particleAfterStar.SetActive(true);
        }
    }
}