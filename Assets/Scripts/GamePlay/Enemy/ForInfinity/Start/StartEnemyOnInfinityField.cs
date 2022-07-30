using UnityEngine;
using UnityEngine.SceneManagement;

namespace GamePlay.Enemy.ForInfinity.Start
{
    public class StartEnemyOnInfinityField : EnemyStarter
    {
        [SerializeField] private GameObject _startOff;
        [SerializeField] private GameObject _particle;

        protected override void Start()
        {
            base.Start();
            if (SceneManager.GetActiveScene().name == "InfinityGame")
            {
                _startOff.SetActive(false);
                _particle.SetActive(false);
            }
        }
        private void FixedUpdate()
        {
            MoveObj();
        }

        protected override void MoveObj()
        {
            if (Vector3.Distance(transform.position, TargetPoints[Index]) > 2)
            {
                transform.position = Vector3.MoveTowards(transform.position, TargetPoints[Index], Speed);
            }
            else
            {
                _particle.SetActive(true);
                _startOff.SetActive(true);
                OffOn(true);
                Destroy(this);
            }
        }
    }
}