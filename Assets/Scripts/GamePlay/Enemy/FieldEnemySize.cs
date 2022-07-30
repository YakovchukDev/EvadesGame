using UnityEngine;

namespace GamePlay.Enemy
{
    public class FieldEnemySize : MonoBehaviour
    {
        private const float ScaleField = 26.8f;

        private void Start()
        {
            var localScale = transform.parent.localScale;
            transform.localScale = new Vector3(ScaleField / localScale.x, ScaleField / localScale.y, ScaleField / localScale.z);
        }
    }
}