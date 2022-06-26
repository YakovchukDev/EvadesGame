using UnityEngine;

namespace GamePlay.Enemy
{
    public class FieldEnemySize : MonoBehaviour
    {
        private const float _scaleField = 26.8f;

        private void Start()
        {
            transform.localScale = new Vector3(_scaleField / transform.parent.localScale.x,
                _scaleField / transform.parent.localScale.y, _scaleField / transform.parent.localScale.z);
        }
    }
}