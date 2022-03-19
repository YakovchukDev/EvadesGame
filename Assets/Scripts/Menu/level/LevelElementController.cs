using System;
using UnityEngine;

namespace Menu.level
{
    public class LevelElementController : MonoBehaviour
    {
        public static bool OnView;
        public void ButtonPlay()
        { 
            OnView = true;
        }
    }
}
