using UnityEngine;

namespace Map
{
    public class LevelComplited : MonoBehaviour
    {
        public delegate void Finish(bool isEnter);
        public static event Finish OpenFinishMenu;
        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag == "Player")
            {   
                OpenFinishMenu(true);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if(other.gameObject.tag == "Player")
            {
                OpenFinishMenu(false);
            }
        }
    }
}
