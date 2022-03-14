using System;
using UnityEngine;

namespace Menu.level
{
    public class LevelElementController : MonoBehaviour
    {
        private LevelMenuView _levelMenuView;
        private LevelElementView _levelElementView;
        private LevelController _levelController;
        public static bool _onView;
        public void ButtonPlay()
        { 
            _onView = true;
        }
    }
}
