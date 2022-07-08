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
        private const float RotationSpeed = 100;

        [SerializeField] protected float _speed;
        [SerializeField] protected ParticleSystem _enemyBounceParticle;
        [SerializeField] private AudioSource _bounceSound;

        protected abstract void MoveSystem(float speed);

        protected void RotateController()
        {
            if (transform.rotation != Rotation)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Rotation, RotationSpeed * Time.deltaTime);
            }
        }

        protected void WallTouch(Collision other)
        {
            var reflectDir = Vector3.Reflect(transform.forward, other.GetContact(0).normal);
            Rotation = Quaternion.LookRotation(reflectDir);

            _bounceSound.Play();
            foreach (var missileHit in other.contacts)
            {
                var hitPoint = missileHit.point;
                _enemyBounceParticle.transform.localScale = transform.localScale / 30;
                Instantiate(_enemyBounceParticle, new Vector3(hitPoint.x, hitPoint.y, hitPoint.z),
                    Quaternion.identity);
            }
        }

        protected void FreezeMe(float allFreezeTime)
        {
            MoveSystem(0);
            FreezeTimer += Time.deltaTime;
            if (FreezeTimer >= allFreezeTime)
            {
                CanFreeze = false;
            }
        }
    }
}