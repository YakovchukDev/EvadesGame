using UnityEngine;

namespace GamePlay.Enemy.ForInfinity.Start
{
    public class StartEnemyOnInfinity : EnemyStarter
    {
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
                OffOn(true);
                Destroy(this);
            }
        }
    }
}