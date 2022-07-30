using System;
using DG.Tweening;
using Education.Level.Controllers;
using GamePlay.Character;
using Joystick_Pack.Examples;
using Map.Ending;
using UnityEngine;
using UnityEngine.UI;

namespace Education.Level
{
    public class EducationSaveZone : MonoBehaviour
    {
        public bool IsSaveZone;
        public bool GiveExpirience;
        [SerializeField] private CharacterSpawner _characterSpawner;
        [SerializeField] private HandController _handController;
        [SerializeField] private bool SaveZoneForTeleport;
        [SerializeField] private bool IsFirstSaveZone;
        [SerializeField] private bool IsLastSaveZone;
        [SerializeField] private PseudoDoor _pseudoDoor;
        [SerializeField] private Button _forOffInteractable;
        [SerializeField] private GameObject _backPanel;
        [SerializeField] private Transform _teleportHandPoint;
        public static event Action<bool> OnEnter;

        private void Start()
        {
            if (IsFirstSaveZone && _pseudoDoor != null)
            {
                _pseudoDoor.UpdateDoor(DoorStatusEnum.Close);
            }
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                OnEnter?.Invoke(IsSaveZone);
                if (IsLastSaveZone && _pseudoDoor != null)
                {
                    _pseudoDoor.UpdateDoor(DoorStatusEnum.Open);
                }


                if (SaveZoneForTeleport)
                {
                    _handController.MoveHand(_teleportHandPoint.position, 0.7f);
                    _forOffInteractable.interactable = false;
                    _backPanel.SetActive(true);
                    _characterSpawner.Character.transform.DORotate(new Vector3(0, 90, 0), 0.5f);
                    _characterSpawner.Character.transform.DOMove(
                        new Vector3(27, _characterSpawner.Character.transform.position.y, 15), 0.5f);
                    JoystickPlayerExample.Speed = 0;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                OnEnter?.Invoke(false);
                if (SaveZoneForTeleport)
                {
                    _handController.Hand.SetActive(false);
                    print(1);
                }
            }
        }
    }
}