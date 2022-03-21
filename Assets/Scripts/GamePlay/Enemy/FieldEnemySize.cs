using UnityEngine;

namespace GamePlay.Enemy
{
    public class FieldEnemySize : MonoBehaviour
    {
        private void Start()
        {
            if (transform.parent.localScale == new Vector3(20f, 20f, 20))
            {
                transform.localScale = new Vector3(1.34f, 1.34f, 1.34f);
            }
            if (transform.parent.localScale == new Vector3(40, 40, 40))
            {
                transform.localScale = new Vector3(0.67f, 0.67f, 0.67f);
            }
            if (transform.parent.localScale == new Vector3(100f, 100, 100))
            {
                transform.localScale = new Vector3(0.268f, 0.268f, 0.268f);
            }
        }
    }
}