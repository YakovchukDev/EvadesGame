using Map.Coins;
using Map.Data;
using UnityEngine;
using Map.Teleport;

namespace Map
{
    public class MapManager : MonoBehaviour
    {
        public static LevelParameters LevelParameters;//парамети, які потрібні для генерації карти
        public static MainDataCollector MainDataCollector;//зборщик, результатів на рівні
        [SerializeField] private CameraController _cameraController;//камера, яка слідкує за гравцем
        private MapGenerator _mapGenerator;
        private EntitiesGenerator _entitiesGenerator;
        private Vector2Int _lastRoomWherePlayerWas;
        private Vector2Int _safeZoneWherePlayerWasOrIs;
        private RoomParameters [,] _mapOfRooms;
        private SafeZoneParameters [,] _mapOfSafeZones;
        [SerializeField] private CoinController _coinController;

        private void OnEnable()
        {
            RoomParameters.OnEnterRoom += MoveMapArea;
            TeleportController.OnTeleport += GoToCentralRoom;
            GamePlay.Character.HealthController.OnBackToSafeZone += GoToSafeZone;
            SafeZoneParameters.OnEnterSafeZone += SetLocationOfNewSafeZone;
            TeleportControl.OnGetCentralSafeZone += GetSafeZoneParameters;
        }
        private void OnDisable()
        {
            RoomParameters.OnEnterRoom -= MoveMapArea;
            TeleportController.OnTeleport -= GoToCentralRoom;
            GamePlay.Character.HealthController.OnBackToSafeZone -= GoToSafeZone;
            SafeZoneParameters.OnEnterSafeZone -= SetLocationOfNewSafeZone;
            TeleportControl.OnGetCentralSafeZone -= GetSafeZoneParameters;
        }
        private void Start()
        {
            MainDataCollector = new MainDataCollector();
            MainDataCollector.SetCoinController(_coinController);

            _mapGenerator = GetComponent<MapGenerator>();
            _mapOfRooms = _mapGenerator.GetMapOfRooms();
            _mapOfSafeZones = _mapGenerator.GetMapOfSafeZones();

            _entitiesGenerator = GetComponent<EntitiesGenerator>();
            _entitiesGenerator.GenerateAndSpawnStars(_mapOfSafeZones);
            _entitiesGenerator.InstantiateCharacter(new Vector3(0, 2, _mapGenerator.GetLongRoom().GetLengthZ() * LevelParameters.Branchs));

            _cameraController.SetPlayer(_entitiesGenerator.GetSelectedCharacter());
            
            ShowRoomsAroundSpecifiedCoordinates(new Vector2Int (LevelParameters.Branchs, 0));
        }
        public void GoToCentralRoom()
        {
            _entitiesGenerator.SetPlayerCoordinates(new Vector3
            (
                _mapGenerator.GetLongRoom().GetLengthX() * _safeZoneWherePlayerWasOrIs.y + _mapGenerator.GetShortRoom().GetLengthX() * _safeZoneWherePlayerWasOrIs.y,
                0,
                _mapGenerator.GetLongRoom().GetLengthZ() * LevelParameters.Branchs
            ));
            ShowRoomsAroundSpecifiedCoordinates(new Vector2Int(LevelParameters.Branchs, _safeZoneWherePlayerWasOrIs.y));
        }
        public static ref MainDataCollector GetMainDataCollector() => ref MainDataCollector;
        private void SetLocationOfNewSafeZone(Vector2Int playerPosition)
        {
            _safeZoneWherePlayerWasOrIs = playerPosition;
        }
        private SafeZoneParameters GetSafeZoneParameters() => _mapOfSafeZones[_safeZoneWherePlayerWasOrIs.y, LevelParameters.Branchs];
        
        private void GoToSafeZone()
        {
            _entitiesGenerator.SetPlayerCoordinates(new Vector3
            (
                _mapGenerator.GetLongRoom().GetLengthX() * _safeZoneWherePlayerWasOrIs.y + _mapGenerator.GetShortRoom().GetLengthX() * _safeZoneWherePlayerWasOrIs.y,
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
            for(int column = startPos.x; column <= endPos.x; column++)
            {
                for(int row = startPos.y; row <= endPos.y; row++)
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
                    if(isShow)
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
    }

}
