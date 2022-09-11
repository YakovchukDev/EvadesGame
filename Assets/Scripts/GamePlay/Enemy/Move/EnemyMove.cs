using System;
using UnityEngine;

namespace GamePlay.Enemy.Move
{
    public abstract class EnemyMove : MonoBehaviour
    {
        public Quaternion Rotation { get; set; }

        protected readonly float[] PossiblesRotation1 = {-175, -85, 5, 95};
        protected readonly float[] PossiblesRotation2 = {-95, -5, 85, 175};
        protected const float TimeForFreeze = 3;
        protected float FreezeTimer;
        protected bool CanFreeze;

        protected float FasterTime;
        [SerializeField] protected float _speed;
        [SerializeField] protected GameObject _enemyBounceParticle;
        [SerializeField] private AudioSource _bounceSound;


        protected abstract void MoveSystem(float speed);

        protected void RotateController()
        {
            transform.rotation = Rotation;
        }

        protected void WallTouch(Collision other, GameObject bounceParticle)
        {
            var reflectDir = Vector3.Reflect(transform.forward, other.GetContact(0).normal);
            Rotation = Quaternion.LookRotation(reflectDir);

            _bounceSound.Play();
            if (bounceParticle != null)
            {
                foreach (var missileHit in other.contacts)
                {
                    var hitPoint = missileHit.point;
                    bounceParticle.SetActive(true);
                    bounceParticle.transform.position = hitPoint;
                }
            }
        }

        protected void FreezeMe(float allFreezeTime)
        {
            print(1);

            FreezeTimer += Time.deltaTime;
            if (FreezeTimer >= allFreezeTime)
            {
                CanFreeze = false;
            }
        }
    }
}