using UnityEngine;

namespace GamePlay.Enemy
{
    public class FieldEnemySize : MonoBehaviour
    {
        private void Start()
        {
            if (transform.parent.localScale == new Vector3(20f, 20f, 20))
            {
                transform.localScale = new Vector3(0.4f, 0.0075f, 0.4f);
            }
            if (transform.parent.localScale == new Vector3(40, 40, 40))
            {
                transform.localScale = new Vector3(0.2f, 0.0075f, 0.2f);
            }
            if (transform.parent.localScale == new Vector3(100f, 100, 100))
            {
                transform.localScale = new Vector3(0.08f, 0.0075f, 0.08f);
            }
        }
    }
}