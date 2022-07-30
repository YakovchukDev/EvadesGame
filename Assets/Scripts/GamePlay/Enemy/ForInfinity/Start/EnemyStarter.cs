using GamePlay.Enemy.ForInfinity.Spawner;
using GamePlay.Enemy.Move;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GamePlay.Enemy.ForInfinity.Start
{
    public abstract class EnemyStarter: MonoBehaviour
    {
        protected readonly Vector3[] TargetPoints = new Vector3[4];
        protected const float Speed = 0.05f;
        protected int Index;
        private MoveJust _moveJustScript;
        private MoveStopper _moveStopperScript;
        protected virtual void Start()
        {
            if (SceneManager.GetActiveScene().name == "InfinityGame")
            {
                Index = InfinityEnemySpawner.Index;

                var position = transform.position;
                TargetPoints[0] = new Vector3(15, position.y, position.z);
                TargetPoints[1] = new Vector3(-15, position.y, position.z);
                TargetPoints[2] = new Vector3(position.x, position.y, 15);
                TargetPoints[3] = new Vector3(position.x, position.y, -15);

                _moveJustScript = gameObject.GetComponent<MoveJust>();
                _moveStopperScript = gameObject.GetComponent<MoveStopper>();

                OffOn(false);
            }
            else
            {
                Destroy(this);
            }
        }
        protected abstract void MoveObj();

        protected void OffOn(bool turn)
        {
            if (_moveJustScript != null)
            {
                _moveJustScript.enabled = turn;
            }

            if (_moveStopperScript != null)
            {
                _moveStopperScript.enabled = turn;
            }
        }
    }
}