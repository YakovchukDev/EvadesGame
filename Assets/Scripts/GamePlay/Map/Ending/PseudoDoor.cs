using System;
using UnityEngine;
using DG.Tweening;
using Education.Level;
using Map.Ending;
using Menu.SelectionClass;

public class PseudoDoor : MonoBehaviour
{
    [SerializeField] private GameObject _leftDoor;
    [SerializeField] private GameObject _rightDoor;
    private bool _isUse = false;
    private DoorStatusEnum _doorStatus;
    
    private void OnCollisionEnter(Collision collision)
    {
        if (SelectionClassView.WhatPlaying == "Level")
        {
            LevelComplited levelComplited = GetComponentInParent<LevelComplited>();
            if (collision.gameObject.CompareTag("Player") && _doorStatus == DoorStatusEnum.Open && levelComplited != null)
            {
                levelComplited.EndOfGame();
            }
        }
        else  if (SelectionClassView.WhatPlaying == "Education")
        {
            EducationLevelComplete levelComplited = GetComponentInParent<EducationLevelComplete>();
            if (collision.gameObject.CompareTag("Player") && _doorStatus == DoorStatusEnum.Open && levelComplited != null)
            {
                levelComplited.EndOfGame();
            } 
        }
        
       
    }

    public void UpdateDoor(DoorStatusEnum doorTypeEnum)
    {
        _doorStatus = doorTypeEnum;
        switch (doorTypeEnum)
        {
            case DoorStatusEnum.Close:
            {
                OpenDoor(0);
                CloseDoor(1);
                break;
            }
            case DoorStatusEnum.Open:
            {
                CloseDoor(0);
                OpenDoor(1);
                break;
            }
        }
    }

    private void OpenDoor(float duration)
    {
        gameObject.tag = "Untagged";
        _leftDoor.transform.DOLocalMoveY(0.04f, duration);
        _leftDoor.transform.DOScaleZ(0, duration);
        _rightDoor.transform.DOLocalMoveY(-0.04f, duration);
        _rightDoor.transform.DOScaleZ(0, duration);
    }

    private void CloseDoor(float duration)
    {
        gameObject.tag = "Wall";
        _leftDoor.transform.DOLocalMoveY(0.02f, duration);
        _leftDoor.transform.DOScaleZ(0.04f, duration);
        _rightDoor.transform.DOLocalMoveY(-0.02f, duration);
        _rightDoor.transform.DOScaleZ(0.04f, duration);
    }
}