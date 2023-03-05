using System;
using UnityEngine;

namespace AwakenPool.Gameplay
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(SphereCollider))]
    public class Ball : MonoBehaviour
    {
        [field: SerializeField] public int PointsForHit { get; private set; } = 10;

        public Rigidbody Rigidbody { get; private set; }

        public event Action<Ball> OnBallDestroyed;

        bool isPlayableBall;

        public void Initialize(bool isPlayableBall)
        {
            this.isPlayableBall = isPlayableBall;
        }

        void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
        }

        void OnCollisionEnter(Collision collision)
        {
            if (isPlayableBall)
                return;

            if (!collision.gameObject.CompareTag(Statics.HitableTag))
                return;

            OnBallDestroyed?.Invoke(this);
            Destroy(gameObject);
        }
    }
}